import lib
import functools

def reduce_calories(calories, current):
    if current == "\n":
        return calories + [0]
    else:
        return calories[:-1] + [calories[len(calories) - 1] + int(current)]

def parse_to_calories(lines):
    calories = functools.reduce(lambda acc, cur: reduce_calories(acc, cur), lines, [0])
    
    return sorted(calories, reverse=True)

def part_1(lines):
    return parse_to_calories(lines)[0]
    
def part_2(lines):
    return sum(parse_to_calories(lines)[0:3])

def __main__():
    lines = lib.read_file("./test.txt")
    answer_p1 = part_1(lines)
    answer_p2 = part_2(lines)

    print(answer_p1)
    print(answer_p2)

__main__()