from dataclasses import dataclass
from typing import Callable, Literal
import cv2
import mediapipe as mp
cap = cv2.VideoCapture(0)
cv2.namedWindow("TestWindow",cv2.WINDOW_KEEPRATIO)
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

@dataclass
class Vector3:
    x : float
    y : float
    z : float
    

def generate_landmarks(hands):
    ret, frame = cap.read()
    image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    image : cv2.Mat = cv2.flip(image, 1)
    image.flags.writeable = False
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    results = hands.process(image)
    yield image,results

def make_vector(results,hand : int,landmark_num : int):
    thing = results.multi_hand_landmarks[hand].landmark[landmark_num]
    return Vector3(thing.x, thing.y, thing.z)

def handednesss(results) -> Literal["Left", "Right", "Both"]:
    return "Both" if len(results.multi_handedness) > 1 else results.multi_handedness[0].classification[0].label

with mp_hands.Hands(min_detection_confidence=0.7, min_tracking_confidence=0.5, max_num_hands=2) as hands:
    # makes the dialog exit when the x button is clicked
    while cv2.getWindowProperty("TestWindow", cv2.WND_PROP_VISIBLE) >= 1:
        image, results = tuple(generate_landmarks(hands))[0]
        # Render hands on screen
        landmarks = results.multi_hand_landmarks
        if landmarks:
            for num, hand in enumerate(landmarks):
                mp_drawing.draw_landmarks(image, hand, mp_hands.HAND_CONNECTIONS)
                make_vector(results, num, 0)
        cv2.imshow("TestWindow",image)
        # waitKey returns a binary number and so we bitmask (bitwise and) it with 255 (0xFF) and check if it is 117 (decimal representation of 'q')
        # see https://stackoverflow.com/questions/53357877/usage-of-ordq-and-0xff for more details.
        if cv2.waitKey(10) & 255 == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()