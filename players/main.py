import play
import settings
import random
import operator
random.seed(123456)
result = list()
seeds = list()
number_of_repetions = settings.NUMBER_OF_REPETIONS

print("Running...")
for i in range(number_of_repetions):
    while True:
        settings.SEED = random.randint(88888, 99999999)
        if settings.SEED not in seeds:
            seeds.append(settings.SEED)
            break
    result.append(play.execute(settings.GAME_PATH,
                               settings.CONF_PATH,
                               str(settings.SEED)))

rounds_time_out = 0
total_rounds = 0
percent_victort = {'Impulsive': 0, 'Demanding': 0, 'Cautious': 0, 'Random': 0}
behavor_winner = ""

with open("result.csv", 'w') as f:
    for r in result:
        f.write(f"{r[0]},{r[1]}\n")
        if int(r[1]) >= 1000:
            rounds_time_out += 1
        else:
            total_rounds += int(r[1])
            percent_victort[r[0]] += 1

print(f"Time-out rounds: {rounds_time_out}\nAverage duration of a match: {total_rounds/number_of_repetions}",
      "Percent of victort:\n")
for k, v in percent_victort.items():
    print(f"{k}: {(v/number_of_repetions)*100}")

print(
    f"Behavor Winner {max(percent_victort.items(), key=operator.itemgetter(1))[0]}")
