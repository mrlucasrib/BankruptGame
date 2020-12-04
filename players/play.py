import sys
import subprocess
import players as pl
import logging as lg

lg.basicConfig(filename="log.txt", level=lg.DEBUG)


def init_game(game_path, conf_path, seed):
    lg.info(f"Seed: {seed}")
    p = subprocess.Popen([game_path, conf_path, seed],
                         text=True, stdin=subprocess.PIPE, stdout=subprocess.PIPE,
                         stderr=subprocess.STDOUT, bufsize=10000)
    p.stdout.readline()
    p.stdout.readline()
    return p


def init_players(p, players):
    p.stdin.write("4\n") # if more players, change this number
    p.stdin.flush()
    lg.debug(p.stdout.readline())

    for player in players:
        p.stdin.write(f"{player.name}\n")
        p.stdin.flush()




def play_game(p, players):
    person = pl.Player
    while True:
        phrase = p.stdout.readline()
        lg.debug(phrase)
        phrase_splited = phrase.replace("'", " ").split()
        word = phrase_splited[0]
        if word == "It":
            for player in players:
                if player.name == phrase_splited[3]:
                    person = player
        elif word == "Do":
            answer = person.behavor(
                int(phrase_splited[7]), int(phrase_splited[11]), int(phrase_splited[14]))
            p.stdin.write(f"{answer}\n")
            p.stdin.flush()
            lg.debug(answer)
        elif word == "Winner:":
            return (phrase_splited[1], phrase_splited[3])
            break

def execute(game_path, conf_path, seed):
  p = init_game(game_path, conf_path, seed)
  players = {pl.Impulsive, pl.Demanding, pl.Cautious, pl.Random}
  init_players(p, players)
  winner, rounds = play_game(p, players)
  lg.info(f"Winner: {winner}, Rounds: {rounds}")
  p.terminate()
  return winner, rounds
