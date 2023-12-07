import lib
import copy
import functools
import math

INPUT_FILE = "./inputs/day_5_test.txt"

def get_seeds(lines):
  seeds = lines[0].split(":")[1].strip().split(" ")
  seeds = list(map(lambda s: int(s), seeds))
  return seeds

def get_seeds_from_range(lines):
  seeds = lines[0].split(":")[1].strip().split(" ")
  seeds = list(map(lambda s: int(s), seeds))
  
  pairs = []
  for i in range(0, len(seeds), 2):
    pairs.append((seeds[i], seeds[i + 1]))
    
  return list(map(lambda p: (p[0], p[0] + p[1] + 1), pairs))
  
def get_default_map():
  return {'ranges': [], 'id': None}

def get_maps(lines):
  maps = []
  
  current_map = get_default_map()
  for index, line in enumerate(lines[2:]):
    if ":" in line:
      current_map['id'] = line.split(":")[0].strip()
    elif len(line) < 1:
      maps.append(current_map)
      current_map = get_default_map()
    else:
      ranges = list(map(lambda x: int(x), line.split(" ")))
      
      current_map["ranges"].append({
        'destination': ranges[0],
        'start': ranges[1],
        'spread': ranges[2]
      })
      
      if index == len(lines) - 3:
        maps.append(current_map)
  
  return maps

def process_seed(seed, maps):
  current = copy.copy(seed)
  steps = []
  for m in maps:
    did_map = False
    for r in m['ranges']:
      if did_map:
        continue
      
      start_map = r['start']
      end_map = r['start'] + r['spread'] - 1
      start_idx = current - start_map
      
      if current < start_map or current > end_map:
        continue
      
      mapping = r['destination'] + start_idx
      steps.append((m['id'], mapping))
      current = mapping
      did_map = True
      
  return current

def get_min_location(seeds, maps):
  ans = []
  
  for seed in seeds:
    ans.append(process_seed(seed, maps))

  return min(ans)

def part_1(lines):
  seeds = get_seeds(lines)
  maps = get_maps(lines)
  
  return get_min_location(seeds, maps)

def get_mid(low, high):
  return ((low + high) // 2)

def sample_search(low, high, maps, lowest = float('inf')):
  if high < low or high < 0 or low < 0 or high == low:
    return lowest
  
  mid = get_mid(low, high)
  
  group_size = math.floor((high - low) / 5)
  
  first = process_seed(get_mid(low, mid), maps)
  second = process_seed(get_mid(mid, high), maps)
  
  print(low, high, lowest, first, second, mid)
  
  if first < second:
    print('lower')
    return sample_search(low, mid, maps, first)
  if second < first:
    print('higher')
    return sample_search(mid + 1, high, maps, second)
  
  return lowest

def part_2(lines):
  seeds_with_ranges = get_seeds_from_range(lines)
  maps = get_maps(lines)
  lowest = float('inf')
  # first = seeds_with_ranges[0]
  
  # ans = sample_search(first[0], first[1], maps)
  for seed, range in seeds_with_ranges:
    ans = sample_search(seed, range - 1, maps)
    # print(seed, range, ans)
    if ans < lowest:
      lowest = ans
  
  return lowest
  
  # for seed, range in seeds_with_ranges:
  #   lowest_in_range = float('inf')
    
  #   print(seed, range)
  #   current_seed = seed
  #   while current_seed < range:
  #     ans = process_seed(current_seed, maps)
  #     if ans < lowest:
  #       print('lowest', ans)
  #       lowest = ans
  #     current_seed += 1
    
  #   return lowest

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')
  print(f'Part 2: {part_2(lines)}')