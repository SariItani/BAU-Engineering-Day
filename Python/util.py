from tomlkit import parse
import pyautogui as pyg
"""
Associate each movement with a key and hand
Possible Implementation:
Make a tiny TOML text file like so
[Left Hand]
move_left = left
move_right = right
move_up = up 
move_down = down
[Right Hand]
attack = a
super_attack = s
item = d
"""


with open("../keys.toml") as keys:
    controls = parse(keys.read())