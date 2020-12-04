import os
from pathlib import Path
cwd =Path.cwd()


SEED = 0000
GAME_PATH = str(cwd.parent.joinpath("game", "release","game"))
CONF_PATH = str(cwd.joinpath("gameConfig.txt"))
NUMBER_OF_REPETIONS = 300