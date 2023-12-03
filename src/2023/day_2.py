import lib
import functools

INPUT_FILE = "./inputs/day_2_input.txt"

MAX_RED = 12
MAX_BLUE = 14
MAX_GREEN = 13

def read_games(lines):
  games = []
  
  for line in lines:
    _id = line.split(":")[0].split(" ")[1]
    raw_sets = line.replace("\n", "").split(":")[1]
    sets = raw_sets.split("; ")
    
    game = {'red': [], 'blue': [], 'green': [], 'id': int(line.split(":")[0].split(" ")[1])}
    for s in sets:
      items = s.split(",")
      for item in items:
        r = item.strip().split(" ")
        value = int(r[0])
        game[r[1]] = game[r[1]] + [value]
    
    games.append(game)
    
  return games

def filter_games(games):
  return list(filter(lambda g: not (max(g['red']) > MAX_RED or max(g['blue']) > MAX_BLUE or max(g['green']) > MAX_GREEN), games))

def map_game_to_power(game):
  return max(game['red']) * max(game['blue']) * max(game['green'])

def part_1(games):
  f = filter_games(games)
  ids = list(map(lambda g: g['id'], f))
  return functools.reduce(lambda acc, cur: acc + cur, ids, 0)

def part_2(games):
  powers = list(map(lambda g: map_game_to_power(g), games))

  return functools.reduce(lambda acc, cur: acc + cur, powers, 0)
    
if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  games = read_games(lines)
  
  print(f"Part 1: {part_1(games)}")
  print(f"Part 2: {part_2(games)}")