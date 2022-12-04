import re
import numpy as np
import numpy.typing as npt
from dataclasses import dataclass
from typing import Callable
import operator
import numba
FINGER_MCPS = (1 , 5, 9 , 13, 17)
FINGERTIPS = (4 , 8 , 12 , 16 , 20)

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


def subscript_list():
    ...

def make_landmark(results, hand_index : int , landmark_num : int):
    return results.multi_hand_landmarks[hand_index].landmark[landmark_num]

def words(string  : str):
    return [word for word in string.split() if word]

def exclude_from_list(base_list : list, indices : list[int] | None = None):
    for index in indices:
        base_list.remove(base_list[index])
    return base_list 


find_numbers = re.compile("\d+")

def pose_dsl(hand_landmarks,command : str,cache_fn = True) -> None | bool:

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

    
def get_hand(results, hand_label : str):
    res = [_ for hand,_ in zip(results.multi_handedness, results.multi_hand_landmarks) if hand.classification[0].label == hand_label]
    return res[0] if res else None


def hand_raised(results, hand_label : str):
    """
    detects if a specific hand is being raised if any hand is raised.
    hand_label can be string indicating the direction (e.g "left", "LEFT", "Left", etc...)
    """
    return bool(get_hand(results, hand_label))



def pose_detected(results, hand_num : int ,pose_command : Callable[[], bool]):
    curr_hand_index = results.multi_handedness[hand_num].classification[0].index
    return hand_raised(results, curr_hand_index) and pose_command()

# depends on get_hand
def make_arr(req_hand, image_x , image_y):
    middle_finger_tip = req_hand.landmark[12]
    middle_finger_x, middle_finger_y = np.array([middle_finger_tip.x * image_x,middle_finger_tip.y * image_y])
    reference_x, reference_y = np.array([image_x // 2 , image_y // 2])
    return middle_finger_x, middle_finger_y, reference_x , reference_y

@numba.njit
def get_hand_orientation(req_tup : tuple[float,float,float,float]):
    middle_finger_x, middle_finger_y, reference_x , reference_y  = req_tup
    x_dir = ( "Left" if middle_finger_x < reference_x else "Right" )
    if middle_finger_y !=  reference_y:
        return ( "Top" if middle_finger_y < reference_y else "Bottom" )+ " " + x_dir
    elif middle_finger_y == reference_y and middle_finger_x == reference_x:
        return "Not moving"
    else:
        return x_dir
