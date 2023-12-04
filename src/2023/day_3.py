import lib
import re
import functools
import copy

INPUT_FILE = "./inputs/day_3_input.txt"
special_character = r"[^\d|\.]"

def is_special_char(row, col):
  return len(re.findall(special_character, lines[row][col])) > 0

def get_adjacent_specials(start_col, end_col, num_row):
  adjacent_specials = []
  is_adjacent = False
  col_range = range(start_col - 1, end_col + 2)
  rows = range(num_row - 1, num_row + 2)
  num = int(lines[num_row][start_col:end_col + 1])
  
  for row in rows:
    if row < 0 or row >= len(lines):
      continue
    
    for i in col_range:
      if i < 0 or i >= len(lines[row]):
        continue
      
      if is_special_char(row, i):
        adjacent_specials.append((row, i, num, lines[row][i]))
      
  if start_col != 0:
    if is_special_char(num_row, start_col - 1):
      adjacent_specials.append((num_row, start_col - 1, num, lines[num_row][start_col - 1]))
  if end_col != len(lines[num_row]) - 1:
    if is_special_char(num_row, end_col + 1):
      adjacent_specials.append((num_row, end_col + 1, num, lines[num_row][end_col + 1]))
      
  return adjacent_specials

def get_number_coordinates(lines):
  number_coordinates = []
  for row, line in enumerate(lines):
    start = -1
    end = - 1
    for col, char in enumerate(line):
      if char.isdigit() and start == -1:
        start = col
      if char.isdigit() and start != -1:
        end = col
      if not char.isdigit():
        if start != -1 and end != -1:
          number_coordinates.append((start, end, row))
        end = -1
        start = -1
      if col == len(line) - 1:
        if start != -1 and end != -1:
          number_coordinates.append((start, end, row))
        end = -1
        start = -1
        
  return number_coordinates

def reduce_to_coord_map(acc, cur):
  cords = f'{cur[0]}:{cur[1]}'
  
  if cords in acc:
    acc[cords] = acc[cords] + [cur[2]]
  else:
    acc[cords] = [cur[2]]
    
  return acc

def filter_records(adjacent_specials):
  ans = copy.copy(adjacent_specials)
  for key, value in adjacent_specials.items():
    if len(value) < 2:
      del ans[key]
    else:
      ans[key] = functools.reduce(lambda a, b: a * b, value)
  
  return ans  

def part_1(lines):    
  num_coordinates = get_number_coordinates(lines)
  valid_coordinates = list(filter(lambda c: len(get_adjacent_specials(c[0], c[1], c[2])) > 0, num_coordinates))
  nums = list(map(lambda c: int(lines[c[2]][c[0]:c[1] + 1]), valid_coordinates))
  
  return functools.reduce(lambda a, b: a + b, nums)

def part_2(lines):
  num_coordinates = get_number_coordinates(lines)

  adjacent_specials = list(map(lambda c: get_adjacent_specials(c[0], c[1], c[2]), num_coordinates))
  adjacent_specials = list(filter(lambda c: len(c) > 0, adjacent_specials))
  adjacent_specials = functools.reduce(lambda a, b: a + b, adjacent_specials)
  adjacent_specials = list(filter(lambda c: c[3] == '*', adjacent_specials))
  adjacent_specials = list(set(adjacent_specials))
  adjacent_specials = functools.reduce(lambda acc, cur: reduce_to_coord_map(acc, cur), adjacent_specials, {})
  
  return functools.reduce(lambda a, b: a + b, filter_records(adjacent_specials).values())
  
if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')   
  print(f'Part 2: {part_2(lines)}')         
