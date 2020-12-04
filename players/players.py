import random
import settings

class Player:
  name = str()

  @staticmethod
  def behavor(price: int, rent: int, coins: int) -> str:
    pass


class Impulsive(Player):
  name = "Impulsive"

  @staticmethod
  def behavor(price, rent, coins):
    return "y"


class Demanding(Player):
  name = "Demanding"

  @staticmethod
  def behavor(price, rent, coins):
    if rent > 50:
      return "y"
    else:
      return "n"


class Cautious(Player):
  name = "Cautious"

  @staticmethod
  def behavor(price, rent, coins):
    if coins - price > 80:
      return "y"
    else:
      return "n"


class Random(Player):
  name = "Random"
  random.seed(int(settings.SEED))

  @staticmethod
  def behavor(price, rent, coins):
    return "yes" if random.randint(0, 1) else "no"
