import lib
import copy

INPUT_FILE = "./inputs/day_5_test.txt"

def get_seeds(lines):
  seeds = lines[0].split(":")[1].strip().split(" ")
  seeds = list(map(lambda s: int(s), seeds))
  return seeds

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

def part_1(lines):
  seeds = get_seeds(lines)
  maps = get_maps(lines)
  ans = []
  
  for seed in seeds:
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
          
    ans.append(current)  
    
  return min(ans)

if __name__ == '__main__':
  lines = lib.read_file(INPUT_FILE)
  lines = list(map(lambda l: l.replace("\n", ""), lines))
  
  print(f'Part 1: {part_1(lines)}')