import cv2
import mediapipe as mp
from mediapipe_primitives import FINGER_MCPS, get_hand, get_hand_orientation, make_m2_vec
cap = cv2.VideoCapture(0)
cv2.namedWindow("TestWindow",cv2.WINDOW_KEEPRATIO)
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

# the first joint in each finger , see the hand_landmarks image in the project directory.
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
    return image,results

# the higher the object , the less its y value is. I know it doesn't make sense but that's how it is.

def palm(hand_landmarks):
    return all(hand_landmarks[num + 3].y < hand_landmarks[num].y for num in FINGER_MCPS)

# def fist(hand_landmarks):
#     return all(hand_landmarks[num + 3].y > hand_landmarks[num.y] for num in FINGER_MCPS[1:])

def hands_recognized(results):
    return [hand.classification[0].index for hand in results.multi_handedness]



with mp_hands.Hands(min_detection_confidence=0.8, min_tracking_confidence=0.7, max_num_hands=2) as hands:
    # makes the dialog exit when the x button is clicked
    while cv2.getWindowProperty("TestWindow", cv2.WND_PROP_VISIBLE) >= 1:
        image, results = generate_landmarks(hands)
        image_x, image_y , _ = image.shape
        landmarks = results.multi_hand_landmarks
        if landmarks:
            m2_vec = make_m2_vec(results, "Left", image_x, image_y)
            for num, hand in enumerate(landmarks):
                # Render hands on screen
                mp_drawing.draw_landmarks(image, hand, mp_hands.HAND_CONNECTIONS)
                print("The middle finger is at the (real) coordinates :", m2_vec)
                # if (m2_vec != None).all():
                #     print(f"The left hand is going {get_hand_orientation(m2_vec)}")
        cv2.imshow("TestWindow",image)
        # waitKey returns a binary number and so we bitmask (bitwise and) it with 255 (0xFF) and check if it is 117 (decimal representation of 'q')
        # see https://stackoverflow.com/questions/53357877/usage-of-ordq-and-0xff for more details.
        if cv2.waitKey(10) & 255 == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()