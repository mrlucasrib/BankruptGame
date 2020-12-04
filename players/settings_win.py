import os
from pathlib import Path
cwd =Path.cwd()


SEED = 123456
GAME_PATH = str(cwd.parent.joinpath("game", "releaseWin","game.exe"))
CONF_PATH = str(cwd.joinpath("gameConfig.txt"))
# Configure loop here
NUMBER_OF_REPETIONS = 5 