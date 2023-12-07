import lib
import functools
from enum import Enum

INPUT_FILE = './inputs/day_7_input.txt'

cards = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A']

class Hand(Enum):
  FIVE_OF_A_KIND = 7
  FOUR_OF_A_KIND = 6
  FULL_HOUSE = 5
  THREE_OF_A_KIND = 4
  TWO_PAIRS = 3
  PAIR = 2
  HIGH_CARD = 1
  
def compare_cards(a, b):
  if a == b:
    return 0
  
  for i in range(0, len(cards)):
    a_index = cards.index(a[i])
    b_index = cards.index(b[i])
    if a_index == b_index:
      continue
    if a_index > b_index:
      return 1
    elif a_index < b_index:
      return -1
    
def is_five_of_a_kind(hand):
  return len(set(hand)) == 1

def is_four_of_a_kind(hand):
  return len(set(hand)) == 2

def is_three_of_a_kind(hand):
  return len(set(hand)) == 3

def is_full_house(hand):
  groups = functools.reduce(group_by_card, hand, {})
  return len(groups.values()) and max(groups.values()) == 3 and min(groups.values()) == 2

def group_by_card(acc, card):
  if card in acc:
    acc[card] += 1
  else:
    acc[card] = 1
    
  return acc

def is_two_pairs(hand):
  return len(set(hand)) == 3 and max(functools.reduce(group_by_card, hand, {}).values()) == 2

def is_pair(hand):
  return len(set(hand)) == 4

def is_high_card(hand):
  return len(set(hand)) == 5

def get_play_type(hand):
  if is_five_of_a_kind(hand):
    return Hand.FIVE_OF_A_KIND
  elif is_full_house(hand):
    return Hand.FULL_HOUSE
  elif is_four_of_a_kind(hand):
    return Hand.FOUR_OF_A_KIND
  elif is_two_pairs(hand):
    return Hand.TWO_PAIRS
  elif is_three_of_a_kind(hand):
    return Hand.THREE_OF_A_KIND
  elif is_pair(hand):
    return Hand.PAIR
  else:
    return Hand.HIGH_CARD

class Play:
  def __init__(self, line):
    split = line.split(" ")
    
    self.cards = split[0]
    self.bid = int(split[1])
    self.hand = get_play_type(self.cards)
    
  def set_rank(self, rank):
    self.rank = rank
    
  def __str__(self):
    return f'Play: {self.cards} - {self.bid} - {self.hand} - {self.rank}'
  
  def __eq__(self, other):
    return self.hand.value == other.hand.value and self.cards == other.cards
  
  def __lt__(self, other):
    if self.hand.value < other.hand.value:
      return True
    elif self.hand.value > other.hand.value:
      return False
    else:
      return compare_cards(self.cards, other.cards) < 1

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  plays = list(map(lambda line: Play(line), lines))
  sorted_plays = sorted(plays)
    
  for i in range(0, len(sorted_plays)):
    sorted_plays[i].set_rank(i + 1)
    print(sorted_plays[i])
    
  ans = functools.reduce(lambda acc, play: acc + (play.rank * play.bid), sorted_plays, 0)
  print(ans)