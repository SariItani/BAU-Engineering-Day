import re
import numpy as np
from dataclasses import dataclass
from typing import Callable
import operator
import numba

# get every 5th frame to ensure that the sensitivity constants can be used
FINGER_MCPS = (1 , 5, 9 , 13, 17)
FINGERTIPS = (4 , 8 , 12 , 16 , 20)
AVERAGE_WRIST_MEAN_DIFF_X = 42
AVERAGE_WRIST_MEAN_DIFF_Y = 162 
CLOSE_X, CLOSE_Y = 20 , 20
@dataclass
class Token:
    _type : str
    value : str

@dataclass
class Operation:
    loperand : Token
    op : Token
    roperand : Token
    qualifier : Token


def make_landmark(results, hand_index : int , landmark_num : int):
    return results.multi_hand_landmarks[hand_index].landmark[landmark_num]

def words(string  : str):
    return [word for word in string.split() if word]

def exclude_from_list(base_list : list, indices : list[int] | None = None):
    for index in indices:
        base_list.remove(base_list[index])
    return base_list 


find_numbers = re.compile("\d+")

def pose_dsl(hand_landmarks,command : str,cache_fn = True):

    def recognize_str(fragment : str):
        
        # language is case insensitive
        match fragment.upper():

            # by default, "ALL" is the qualifier for the operation
            case ("ANY" | "ALL") as qualifier:
                return Token("QUALIFIER", qualifier)
           
            case ("MCP.X" | "MCP.Y" | "MCP.Z"  | "TIP.X"  | "TIP.Y"  | "TIP.Z") as finger_group:
                return Token("OPERAND", finger_group)

            case (">" | "<" |"<=" | ">=" | "==" |"!=") as operation :
                return Token("OPERATION", operation)
            
            case ("ONLY"| "EXCEPT") as domain:
                return Token("DOMAIN", domain)
            case _ :
                if find_numbers.search(fragment):
                    return Token("SELECTOR", fragment)
                return None
    

    def get_tup_as_list(dsl_token : Token, converse=False):
        if converse:
            return  list( FINGERTIPS ) if dsl_token.value == "MCP" else list(  FINGER_MCPS )
        return  list( FINGER_MCPS ) if dsl_token.value == "MCP" else  list( FINGERTIPS
  )   
    # insert the ALL qualifier by default
    tokens : tuple[Token] = tuple(filter(None, (recognize_str(word) for word in words(command))))
    operation = Operation(tokens[1], tokens[2], tokens[3],qualifier=tokens[0])
    qualifier = operation.qualifier
    domain, range_selector = tokens[-2], tokens[-1]
    reference_list , opposite_list = get_tup_as_list(qualifier), get_tup_as_list(qualifier,converse=True)
    if len(tokens) < 3:
        raise SyntaxError("Malformed expression, make sure the command is in the form : All or Any (MCP or TIP).(y or x or z) (> , < , >= , <= , != , == , etc...) (MCP or TIP).(y or x or z)")
    # remove unrecognized words
    range_str = "[0:]"
    if domain != operation.roperand and range_selector != operation.op:
        # stop is optional
        start,*stop = range_selector.value.split(",")
        stop = list(map(int, stop))
        # if domain == "ONLY":
        #     range_str = f"[{int(start) - 1} : {(stop) - 1}]"
        #     ...
        # else:
        #     reference_list = exclude_from_list(reference_list,map(lambda x : x-1,[int(start), *stop]))
            

        
    def inner(hand_landmarks) -> bool:
        # hand_landmarks[num].any_coordinate operation hand_landmarks[relation with num].any_coordinate for num in domain
        # in other words, if I know the tuple             # hand_landmarks[num].any_coordinate operation hand_landmarks[relation with num].any_coordinate for num in domainthat I got in the left operand, I can get the values with respect to that tuple
        operators = {
            ">" : operator.gt,
            "<" : operator.lt,
            "<=" : operator.le,
            ">=" : operator.ge,
            "==" : operator.eq,
            "!=" : operator.ne
        }
        start,stop = map(int,range_str.removeprefix("[").removesuffix("]").split(":"))
        if qualifier == "ALL":
            return all(operators[operation](hand_landmarks[num1],hand_landmarks[num2]) for num1,num2 in zip(reference_list, opposite_list))
        else:
            return any(operators[operation](hand_landmarks[num1],hand_landmarks[num2]) for num1,num2 in zip(reference_list, opposite_list))
    return inner if cache_fn else inner()


def hand_mean(hand_obj,axis: str, axis_multiplier : int) -> float :
    return np.mean(np.array([ getattr(hand_obj.landmark[num], axis) * axis_multiplier for num in range(21) ]))
    
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


def pose_detected(rectified_results, hand_num : int ,pose_command : Callable[[], bool]):
    ...

def adjacent_coord(coord_1 : float , coord_2 : float , axis = "x", close_x = CLOSE_X, close_y = CLOSE_Y):
    return  abs(coord_1 - coord_2) <=close_x if axis == "x" else abs(coord_1 - coord_2) <=close_y

def adjacent_pts(vec1 : tuple[float,float], vec2 : tuple[float,float]):
    return adjacent_coord(vec1[0],vec2[0], "x") and adjacent_coord(vec1[0],vec2[0], "y")

# depends on get_hand
def make_tup(req_hand_obj, image_x : int , image_y : int):
    x_mean , y_mean = as_mean(req_hand_obj.landmark[0].x, req_hand_obj.landmark[0].y, image_x , image_y) 
    return  x_mean , y_mean , image_x / 2, image_y / 2

