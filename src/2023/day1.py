import lib
import functools

INPUT_FILE = "./inputs/day_1_input.txt"

NEW_MAP = {
  "one": "o1e",
  "two": "t2o",
  "three": "t3e",
  "four": "f4r",
  "five": "f5e",
  "six": "s6x",
  "seven": "s7n",
  "eight": "e8t",
  "nine": "n9e",
}

def map_text_to_digit(line):
    l = line

    for num in NEW_MAP.keys():
        l = l.replace(num, NEW_MAP[num])
    return l
    
def get_digits(line):
    digits = []
    for char in line:
        if char.isnumeric():
            digits.append(int(char))

    return get_line_value(digits)

def get_line_value(digits):
    return int(f'{digits[0]}{digits[-1]}')

def part_1(lines):
   x = list(map(lambda line: get_digits(line), lines))
   x = functools.reduce(lambda acc, cur: acc + cur, x, 0)
   return x

def part_2(lines):
   x = list(map(lambda l: map_text_to_digit(l), lines))
   x = list(map(lambda line: get_digits(line), x))
   x = functools.reduce(lambda acc, cur: acc + cur, x, 0)
   return x

def __main__():
    lines = lib.read_file(INPUT_FILE)

    print(f"Part 1: {part_1(lines)}")
    print(f"Part 2: {part_2(lines)}")

__main__()