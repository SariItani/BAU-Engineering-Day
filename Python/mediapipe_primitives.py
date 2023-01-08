import numba

# get every 5th frame to ensure that the sensitivity constants can be used
FINGER_MCPS = (1 , 5, 9 , 13, 17)
FINGERTIPS = (4 , 8 , 12 , 16 , 20)
AVERAGE_WRIST_MEAN_DIFF_X = 42
AVERAGE_WRIST_MEAN_DIFF_Y = 162 
CLOSE_X, CLOSE_Y = 20 , 20

def make_landmark(results, hand_index : int , landmark_num : int):
    return results.multi_hand_landmarks[hand_index].landmark[landmark_num]

def landmark_rectifier(multi_hand_landmarks, img_width : int, img_height : int):
    # [left, right] [0,right] [left, 0 ] []
    if len(multi_hand_landmarks) == 2:
        return [multi_hand_landmarks[0], multi_hand_landmarks[1]]
    else:
        wrist_obj = multi_hand_landmarks[0].landmark[0] 
        return [multi_hand_landmarks[0], 0] if as_mean(wrist_obj.x, wrist_obj.y , img_width, img_height)[0] < img_width // 2 else [0 , multi_hand_landmarks[0]]
        
def get_correct_side(rectified_list : list, side : str):
    side = side.capitalize()
    left, right = rectified_list
    if left and side == "Left":
        return left
    elif right and side == "Right":
        return right

    
def hand_raised(rectified_results, hand_label : str) -> bool:
    """
    detects if a specific hand is being raised if any hand is raised.
    hand_label can be string indicating the direction (e.g "left", "LEFT", "Left", etc...)
    """
    index = 0 # assume hand to be left
    if hand_label.capitalize() == "Right":
        index = 1
    return rectified_results[index] != 0


@numba.njit
def as_mean(hand_obj_x : float , hand_obj_y : float, image_width : int  , image_height: int) -> tuple[float,float] | float :
    """
    Transform the wrist landmark into a point approximately equal to mean of either hand, defined by certain constants.
    """
    x_fake_mean = hand_obj_x * image_width + AVERAGE_WRIST_MEAN_DIFF_X
    y_fake_mean = hand_obj_y * image_height + AVERAGE_WRIST_MEAN_DIFF_Y 
    return (x_fake_mean, y_fake_mean)

def adjacent_coord(coord_1 : float , coord_2 : float , axis = "x", close_x = CLOSE_X, close_y = CLOSE_Y):
    return  abs(coord_1 - coord_2) <=close_x if axis == "x" else abs(coord_1 - coord_2) <=close_y

def adjacent_pts(vec1 : tuple[float,float], vec2 : tuple[float,float]):
    return adjacent_coord(vec1[0],vec2[0], "x") and adjacent_coord(vec1[0],vec2[0], "y")

# depends on get_hand
def make_tup(req_hand_obj, image_x : int , image_y : int):
    x_mean , y_mean = as_mean(req_hand_obj.landmark[0].x, req_hand_obj.landmark[0].y, image_x , image_y) 
    return  x_mean , y_mean , image_x / 2, image_y / 2

