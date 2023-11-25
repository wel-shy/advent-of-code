import lib

def parse_to_calories(lines):
    calories = [0]

    for line in lines:
        if line == "\n":
            calories.append(0)
            continue

        calories[len(calories) -1] = calories[len(calories) -1] + int(line)

    calories.sort(reverse=True)
    
    return calories

def part_1(lines):
    calories = parse_to_calories(lines)
    
    return calories[0]

def part_2(lines):
    calories = parse_to_calories(lines)

    return sum(calories[0:3])

def __main__():
    lines = lib.read_file("./test.txt")
    answer_p1 = part_1(lines)
    answer_p2 = part_2(lines)

    print(answer_p1)
    print(answer_p2)

__main__()