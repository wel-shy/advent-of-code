use std::convert::TryInto;
use std::fs;

const INPUT: &str = "./input.txt";
const PREAMBLE_LEN: usize = 25;

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let lines: Vec<&str> = contents.split("\n").collect::<Vec<&str>>();
  let input: Vec<i64> = lines
    .into_iter()
    .map(|l| l.parse::<i64>().unwrap())
    .collect();

  let mut err_num: i64 = -1;
  let mut err_num_idx: usize = 0;
  for idx in PREAMBLE_LEN..input.len() {
    let preamble = &input[(idx - PREAMBLE_LEN)..idx];
    if !is_sum(&preamble, input[idx]) {
      err_num = input[idx];
      err_num_idx = idx;
      break;
    }
  }

  println!("ERR NUM: {:#?}", err_num);

  let err_input = &input[0..err_num_idx - 1];

  let mut series_idx: usize = 0;
  let mut series_len: usize = 1;
  let mut found: bool = false;
  let mut s: &[i64] = &mut [];
  while !found && series_len + series_idx < (input.len() as i64).try_into().unwrap() {
    let series = &err_input[series_idx..(series_idx + series_len)];
    let sum: i64 = sum_series(series);
    if sum < err_num {
      series_len += 1;
    }
    if sum > err_num {
      series_idx += 1;
      series_len = 1;
    }
    if sum == err_num {
      found = true;
      s = series;
    }
  }

  let min: i64 = *s.iter().min().unwrap();
  let max: i64 = *s.iter().max().unwrap();

  println!("{}", min + max);
}

fn is_sum(preamble: &[i64], x: i64) -> bool {
  for i in 0..preamble.len() {
    for j in 0..preamble.len() {
      if i != j && preamble[i] + preamble[j] == x {
        return true;
      }
    }
  }

  false
}

fn sum_series(series: &[i64]) -> i64 {
  series.iter().sum()
}
