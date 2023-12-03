import lib
import re
import functools

INPUT_FILE = "./inputs/day_3_input.txt"
special_character = r"[^\d|\.]"

# def get_special_char_map(lines):
#   special_characters = []
#   for row, line in enumerate(lines):
#     for col, char in enumerate(line):
#       is_special = len(re.findall(special_character, char)) > 0
#       if is_special:
#         special_characters.append((row, col))
        
#   return special_characters

def is_special_char(row, col):
  return len(re.findall(special_character, lines[row][col])) > 0

def is_special_char_adjacent(start_col, end_col, num_row):
  is_adjacent = False
  col_range = range(start_col - 1, end_col + 2)
  rows = range(num_row - 1, num_row + 2)
  
  for row in rows:
    if row < 0 or row >= len(lines):
      continue
    
    for i in col_range:
      if i < 0 or i >= len(lines[row]):
        continue
      
      if is_special_char(row, i):
        return True
      
  if start_col != 0:
    if is_special_char(num_row, start_col - 1):
      return True
  if end_col != len(lines[num_row]) - 1:
    if is_special_char(num_row, end_col + 1):
      return True
      
  return False

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

def map_coordinates_to_number(coordinate):
  return int(lines[coordinate[2]][coordinate[0]:coordinate[1] + 1])
  

def part_1(lines):    
  num_coordinates = get_number_coordinates(lines)
  valid_coordinates = list(filter(lambda c: is_special_char_adjacent(c[0], c[1], c[2]), num_coordinates))
  nums = list(map(lambda c: map_coordinates_to_number(c), valid_coordinates))
  
  return functools.reduce(lambda a, b: a + b, nums)

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')            
