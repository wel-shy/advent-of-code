use std::fs;

const INPUT: &str = "./test.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let mut input: Vec<u32> = contents
    .split("\n")
    .into_iter()
    .map(|s| s.parse::<u32>().unwrap())
    .collect::<Vec<u32>>();
  input.sort();

  // 1 for the device
  let mut difference_dist: Vec<u32> = vec![0, 0, 1];
  let mut curr_jolt: u32 = 0;

  for i in 0..input.len() {
    if input[i] - curr_jolt <= 3 {
      update_dist(&mut difference_dist, &curr_jolt, &input[i]);
      curr_jolt = input[i];
    }
  }

  println!("part 1: {}", difference_dist[0] * difference_dist[2]);

  let max = input.iter().max().unwrap();

  let mut adapters: Vec<u64> = input.iter().map(|x| *x as u64).collect::<Vec<u64>>();
  let mut initial_adapters: Vec<u64> = vec![0, (max + 3) as u64];
  adapters.append(&mut initial_adapters);
  adapters.sort();
  println!("part 2: {}", solve(&adapters));
}

fn update_dist(dist: &mut Vec<u32>, source: &u32, adapter: &u32) {
  let diff = (adapter - source - 1) as usize;
  dist[diff] = dist[diff] + 1;
}

fn get_combos(idx: usize, diff: u8, adaptors: &Vec<u64>, count_array: &mut Vec<u64>) -> u64 {
  let diff_idx: i32 = idx as i32 - diff as i32;
  let adp: i64 = adaptors[idx] as i64;
  if diff_idx < 0 {
    return 0;
  }

  if adaptors[diff_idx as usize] as i64 >= adp - 3 {
    return count_array[diff_idx as usize];
  } else {
    return 0;
  }
}

fn solve(adapters: &Vec<u64>) -> u64 {
  let mut count_array: Vec<u64> = vec![1];
  for i in 1..adapters.len() {
    let mut sum: u64 = 0;
    for j in 1..=3 {
      sum += get_combos(i, j, adapters, &mut count_array)
    }

    count_array.push(sum);
  }

  count_array[count_array.len() - 1]
}
