import lib
import functools

INPUT_FILE = "./inputs/day_4_input.txt"
lookup = {}

def get_score(score, card):
  card_score = card[0]
  if card_score == 0:
    return score
    
  return (2 ** (card_score - 1)) + score

def remove_empty_strings(arr):
  return list(filter(lambda x: x != "", arr))

def parse_line(line):
  split_line = line.split("|")
  winning_numbers = split_line[0].split(": ")[1].strip().split(" ")
  numbers = split_line[1].strip().split(" ")
  
  card_id = int(line.split(": ")[0].strip().replace("Card", "").strip())
  
  return (set(remove_empty_strings(winning_numbers)), set(remove_empty_strings(numbers)), card_id)

def get_next_cards(cards, card, lines):
  amount = card[0]
  start_idx = card[1]
  next_cards = lines[start_idx:start_idx + amount]
  
  print(f"Next cards: {len(next_cards)}")
  
  return cards + next_cards

def part_2(lines):
  total_cards = len(lines)
  
  cards_to_process = lines
  while len(cards_to_process) > 0:
    cards = list(map(parse_line, cards_to_process))
    cards = list(map(lambda card: (card[0].intersection(card[1]), card[2]), cards))
    cards = list(map(lambda card: (len(card[0]), card[1]), cards))
    
    cards = functools.reduce(lambda acc, card: get_next_cards(acc, card, lines), cards, [])
    cards_to_process = cards
    total_cards += len(cards)
    
    print(f"Total cards: {total_cards}")
    
  return total_cards

def get_card_id(line):
  return int(line.split(": ")[0].strip().replace("Card", "").strip())

def part_2_recursive(lines):
  total_cards = len(lines)
  
  for line in lines:
    card_id = get_card_id(line)    
    child_cards = get_total_child_cards(line, lines)
    print(card_id, f"Child cards: {child_cards}")
      
    total_cards += child_cards
  return total_cards

def get_card_winning_numbers(card):
  c = parse_line(card)
  card_id = get_card_id(card)
  if card_id in lookup:
    return lookup[card_id]
  
  winning_numbers = len(c[0].intersection(c[1]))
  return winning_numbers

def get_total_child_cards(card, lines):
  next_cards = []
  card_id = get_card_id(card)
  winning_numbers = get_card_winning_numbers(card)
  
  if not card_id in lookup:
    lookup[card_id] = winning_numbers
  
  next_cards = lines[card_id:card_id + winning_numbers]
  total = 0
  for card in next_cards:
    total = total + 1 + get_total_child_cards(card, lines)
  
  return total
  

def part_1(lines):
  cards = list(map(parse_line, lines))
  cards = list(map(lambda card: (card[0].intersection(card[1]), card[2]), cards))
  cards = list(map(lambda card: (len(card[0]), card[1]), cards))
  
  return functools.reduce(get_score, cards, 0)

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')   
  print(f'Part 2: {part_2_recursive(lines)}')