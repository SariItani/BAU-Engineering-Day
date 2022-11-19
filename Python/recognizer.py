from functools import partial
from typing import Callable, Literal
import cv2
import mediapipe as mp
import pyautogui as pyg
cap = cv2.VideoCapture(0)
cv2.namedWindow("TestWindow",cv2.WINDOW_KEEPRATIO)
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

class Vector3:
    # optimize class to take less memory
    __slots__ = ["x", "y", "z"]
    x : float
    y : float
    z : float

    def __init__(self, results, hand : int, landmark_num : int) -> None:
        thing = results.multi_hand_landmarks[hand].landmark[landmark_num]
        self.x, self.y , self.z  = thing.x, thing.y , thing.z

        
        

class Finger:
    __slots__ = ["mcp_num","finger_num", "fingertip_vec", "finger_mcp_vec"]
    mcp_num : int
    finger_num : int
    fingertip_vec : Vector3
    finger_mcp_vec : Vector3

    def __init__(self, results, hand : int, finger_mcp_val : int) -> None:
        self.mcp_num = finger_mcp_val
        self.finger_mcp_vec, self.fingertip_vec = (Vector3(results, hand , finger_mcp_val),
                                                   Vector3(results, hand, finger_mcp_val + 3))
# the first joint in each finger , see the hand_landmarks image in the project directory.
FINGER_MCPS = (1 , 5, 9 , 13, 17)
SENSITIVITY_X , SENSITIVITY_Y , SENSITIVITY_Z = (0.0,) * 3
"""
In reality only two gestures need to be recognized,
the fist gesture and the open palm gesture.

However, I need to detect the difference between the current movement vector and the second which determines the direction
associated with each gesture.

Example:

closed hand : move left hand with closed fist left and right to control where the player is headed (closed fist left -> left arrow key, closed fist right -> right arrow key)
strong attack : move right hand with closed first inwards ( positive z difference since it heads towards the camera)
light attack : move right hand with an open hand and show the palm to indicate an attack towards the +ve z axis.

"""


def generate_landmarks(hands):
    _ , frame = cap.read()
    image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    image : cv2.Mat = cv2.flip(image, 1)
    image.flags.writeable = False
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    results = hands.process(image)
    yield image,results

def handednesss(results) -> Literal["Left", "Right", "Both"]:
    return "Both" if len(results.multi_handedness) > 1 else results.multi_handedness[0].classification[0].label

def check_fingers_raised(fingers :list[Finger],to_check :list[Finger]| None,exclude=True):
    """
    to_check : list of finger(s) to check and their associated vector, can be None to check if all fingers are raised.
    hand : the current hand being checked
    exclude : Check whether all the fingers except the ones specified are raised except the ones specified if pick is false (default).
    Check whether all the fingers specified are raised otherwise.
    """
    def raised_in_list(finger_list :list[Finger]):
        # the higher the object , the less its y value is. I know it doesn't make sense but that's how it is.
        return all(f.fingertip_vec.y < f.finger_mcp_vec.y for f in finger_list)
    if not to_check:
        # need a list of all the fingers
        return raised_in_list(fingers)
    elif exclude:
        return raised_in_list([f for f in fingers if f not in to_check])
    else:
        return raised_in_list(to_check)


def on_gestures(*gesture_callback_pairs :tuple[Callable[[], bool] , int ,Callable[[], None]], input_hand : int):
    """
    If a certain function corresponding to a gesture returns true, execute the callback.
    """
    for gesture, hand , callback in gesture_callback_pairs:
        if gesture() and hand == input_hand:
            callback()
        

with mp_hands.Hands(min_detection_confidence=0.8, min_tracking_confidence=0.7, max_num_hands=2) as hands:
    # makes the dialog exit when the x button is clicked
    while cv2.getWindowProperty("TestWindow", cv2.WND_PROP_VISIBLE) >= 1:
        image, results = tuple(generate_landmarks(hands))[0]
        fingers = [Finger(results,val) for val in FINGER_MCPS]
        palm = partial(check_fingers_raised, fingers, None, exclude=True)
        fist = partial(check_fingers_raised, fingers, fingers[0])
        press_j = partial(pyg.press, 'j')
        press_k = partial(pyg.press, 'k')
        gesture_pairs = ((palm, 1 , press_j),
                         (fist, 1 , press_k))
        landmarks = results.multi_hand_landmarks
        if landmarks:
            for num, hand in enumerate(landmarks):
                # Render hands on screen
                mp_drawing.draw_landmarks(image, hand, mp_hands.HAND_CONNECTIONS)
                on_gestures(*gesture_pairs, input_hand=num)
                
        cv2.imshow("TestWindow",image)
        # waitKey returns a binary number and so we bitmask (bitwise and) it with 255 (0xFF) and check if it is 117 (decimal representation of 'q')
        # see https://stackoverflow.com/questions/53357877/usage-of-ordq-and-0xff for more details.
        if cv2.waitKey(10) & 255 == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()