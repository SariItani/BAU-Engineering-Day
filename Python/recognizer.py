import cv2
import mediapipe as mp
import numpy as np
cap = cv2.VideoCapture(0)
cv2.namedWindow("TestWindow",cv2.WINDOW_KEEPRATIO)
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils
# makes the dialog exit when the x button is clicked
with mp_hands.Hands(min_detection_confidence=0.7, min_tracking_confidence=0.5, max_num_hands=2) as hands:
    while cv2.getWindowProperty("TestWindow", cv2.WND_PROP_VISIBLE) >= 1:
        ret, frame = cap.read()
        image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        image : cv2.Mat = cv2.flip(image, 1)
        results = hands.process(image)
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        # Render hands on screen
        if results.multi_hand_landmarks:
            for num, hand in enumerate(results.multi_hand_landmarks):
                mp_drawing.draw_landmarks(image, hand, mp_hands.HAND_CONNECTIONS)
        cv2.imshow("TestWindow",image)
        # waitKey returns a binary number and so we bitmask (bitwise and) it with 255 (0xFF) and check if it is 117 (decimal representation of 'q')
        # see https://stackoverflow.com/questions/53357877/usage-of-ordq-and-0xff for more details.
        if cv2.waitKey(10) & 255 == ord('q'):
            break

cap.release()
cv2.destroyAllWindows()