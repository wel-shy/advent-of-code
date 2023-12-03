import lib
import functools

INPUT_FILE = "./inputs/day_1_input.txt"

NUMBER_MAP = {
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
    l = ''

    for num in NUMBER_MAP.keys():
        l = line.replace(num, NUMBER_MAP[num])

    return l
    
def get_digits(line):
    digits = functools.reduce(lambda acc, cur: acc + [int(cur)] if cur.isnumeric() else acc + [], line, [])
    return get_line_value(digits)

def get_line_value(digits):
    return int(f'{digits[0]}{digits[-1]}')

def part_1(lines):
   x = list(map(lambda line: get_digits(line), lines))
   return functools.reduce(lambda acc, cur: acc + cur, x, 0)

def part_2(lines):
   x = map(lambda l: map_text_to_digit(l), lines)
   x = map(lambda line: get_digits(line), x)
   return functools.reduce(lambda acc, cur: acc + cur, list(x), 0)

if __name__ == '__main__':
    lines = lib.read_file(INPUT_FILE)

    print(f"Part 1: {part_1(lines)}")
    print(f"Part 2: {part_2(lines)}")