import lib
import functools

INPUT_FILE = './inputs/day_6_input.txt'

def parse_to_ints(line):
  nums = list(filter(lambda x: x != "", line.split(" ")))
  return list(map(lambda x: int(x), nums))

def parse_races(lines):
  times = lines[0].split(":")[1].strip()
  times = parse_to_ints(times)
  
  distances = lines[1].split(":")[1].strip()
  distances = parse_to_ints(distances)
  
  races = []
  for i in range(0, len(times)):
    races.append((times[i], distances[i]))
    
  return races

def get_winning_options(race):
  max_time, record = race
  charging_times = range(1, max_time)
  
  viable_options = 0
  for charging_time in charging_times:    
    if charging_time * (max_time - charging_time) > record:
      viable_options += 1
      
  return viable_options

def part_1(lines):
  races = parse_races(lines)
  options = []
  for race in races:
    options = options + [get_winning_options(race)]
  
  return functools.reduce(lambda x, y: x * y, options)

def read_number_from_line(line):
  return int(functools.reduce(lambda x, y: x + y if y.isnumeric() else x, line, ''))

def part_2(lines):
  time = read_number_from_line(lines[0])
  record = read_number_from_line(lines[1])
  
  return get_winning_options((time, record))

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')
  print(f'Part 2: {part_2(lines)}')