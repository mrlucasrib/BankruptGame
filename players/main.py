import play
import settings
import random

random.seed(123456)
result = list()

for i in range(300):
  settings.seed = str(random.randint(88888,99999999))
  result.append(play.execute("/home/lucas/Downloads/ProjectsGit/BankruptGame/game/bin/Release/net5.0/linux-x64/publish/game",
             "/home/lucas/Documentos/gameConfig.txt",
             settings.seed))
  print(i)
with open("result.csv", 'w') as f:
  for r in result:
    f.write(f"{r[0]},{r[1]}\n")
  
