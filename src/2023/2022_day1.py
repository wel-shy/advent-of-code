import lib
import functools

INPUT_FILE = "./inputs/day_1_test.txt"

def process_calorie(calories, current):
    if current == "\n":
        return calories + [0]
    else:
        return calories[:-1] + [calories[len(calories) - 1] + int(current)]

def parse_to_calories(lines):
    calories = functools.reduce(lambda acc, cur: process_calorie(acc, cur), lines, [0])
    
    return sorted(calories, reverse=True)

def part_1(lines):
    return parse_to_calories(lines)[0]
    
def part_2(lines):
    return sum(parse_to_calories(lines)[0:3])

def __main__():
    lines = lib.read_file(INPUT_FILE)

    print(f"Part 1: {part_1(lines)}")
    print(f"Part 2: {part_2(lines)}")

__main__()