import os
import pathlib

sprites_path = os.path.expanduser("~/Documents/GitHub/BAU-Engineering-Day/Unity/Engineering Day Project/Assets/Sprites")

for file in pathlib.Path(sprites_path).glob("**/*.meta"):
    os.remove(file)