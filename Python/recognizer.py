from functools import partial
import cv2
import mediapipe as mp
import keyboard as kb
from mediapipe_primitives import FINGER_MCPS, adjacent_coord, landmark_rectifier
cap = cv2.VideoCapture(0)
cv2.namedWindow("TestWindow")
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

last_dir = None
# the first joint in each finger , see the hand_landmarks image in the project directory.
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
# the higher the object , the less its y value is. I know it doesn't make sense but that's how it is.


def write_simplex(image, text: str, position: tuple[int, int]):
    cv2.putText(image, text,  position, cv2.FONT_HERSHEY_SIMPLEX,
                1, (0, 255, 255), 2, cv2.LINE_4)


write_top_left = partial(write_simplex, position=(50, 50))


def palm(hand_landmark):
    return all(hand_landmark.landmark[num + 3].y < hand_landmark.landmark[num].y for num in FINGER_MCPS[1:]) and hand_landmark.landmark[4].x > hand_landmark.landmark[5].x

def fist(hand_landmarks):
    return all(hand_landmarks.landmark[num + 3].y > hand_landmarks.landmark[num].y for num in FINGER_MCPS[1:])


with mp_hands.Hands(min_detection_confidence=0.9, min_tracking_confidence=0.9) as hands:
    # makes the dialog exit when the x button is clicked
    while cv2.getWindowProperty("TestWindow", cv2.WND_PROP_VISIBLE) >= 1:
        _, frame = cap.read()
        image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        image: cv2.Mat = cv2.flip(image, 1)
        image.flags.writeable = False
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        results = hands.process(image)
        image_width, image_height, _ = image.shape
        landmarks = results.multi_hand_landmarks
        movement_region = [image_width // 8, image_height * 5 // 8]
        write_simplex(image, "x", [movement_region[0],  movement_region[1]])
        if landmarks:
            rectified_sides = landmark_rectifier(landmarks, image_width, image_height)
            for num, hand in enumerate(landmarks):
                mp_drawing.draw_landmarks(
                    image, hand, mp_hands.HAND_CONNECTIONS)
                if rectified_sides[0]:
                    wrist = rectified_sides[0].landmark[0]
                    x, y = wrist.x * image_width, wrist.y * image_height
                    x_dir = "Left" if x < movement_region[0] else "Right"
                    y_dir = "Down" if y > (movement_region[1] + 125) else "Up"
                    adj_cord_x, adj_cord_y = adjacent_coord(x, movement_region[0], "x"),adjacent_coord(y, movement_region[1] + 125, "y", close_y=25)
                    match (adj_cord_x , adj_cord_y):

                        case (True, True):
                            write_top_left(image, "Not moving")
                            if last_dir:
                                kb.release(last_dir)

                        case (True, False):
                            write_top_left(image, y_dir)
                            kb.send(y_dir)

                        case (False, True):
                            write_top_left(image, x_dir)
                            last_dir = x_dir
                            kb.send(x_dir, do_release=False)

                        case (False, False):
                            write_top_left(image, x_dir + " " + y_dir)
                            last_dir = x_dir
                            kb.send(x_dir, do_release=False)
                            kb.send(y_dir)
                if rectified_sides[1]:
                    # add controls for punching, throwing, etc.. here.
                    # such that each motion is assigned a hand gesture.
                    if fist(rectified_sides[1]):
                        kb.send("f")
        else :
            if last_dir:
                kb.release(last_dir)
        cv2.imshow("TestWindow", image)
        # waitKey returns a binary number and so we bitmask (bitwise and) it with 255 (0xFF) and check if it is 117 (decimal representation of 'q')
        # see https://stackoverflow.com/questions/53357877/usage-of-ordq-and-0xff for more details.
        if cv2.waitKey(10) & 255 == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()
