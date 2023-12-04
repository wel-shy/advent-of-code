import lib
import functools

INPUT_FILE = "./inputs/day_4_input.txt"

def get_score(score, num_winning):
  if num_winning == 0:
    return score
    
  return score + (2 ** (num_winning - 1))

def remove_empty_strings(arr):
  return list(filter(lambda x: x != "", arr))

def parse_line(line):
  split_line = line.split("|")
  winning_numbers = split_line[0].split(": ")[1].strip().split(" ")
  numbers = split_line[1].strip().split(" ")
  
  return (set(remove_empty_strings(winning_numbers)), set(remove_empty_strings(numbers)))

def part_1(lines):
  cards = list(map(parse_line, lines))
  winning_numbers = list(map(lambda card: card[0].intersection(card[1]), cards))
  num_winning_numbers = list(map(lambda winning_numbers: len(winning_numbers), winning_numbers))
  
  return functools.reduce(get_score, num_winning_numbers, 0)

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')   
  # print(f'Part 2: {part_2(lines)}')