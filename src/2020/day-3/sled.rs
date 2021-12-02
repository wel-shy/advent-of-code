use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

const TREE: char = '#';
const INPUT_PATH: &str = "./input.txt";

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where
  P: AsRef<Path>,
{
  let file = File::open(filename)?;
  Ok(io::BufReader::new(file).lines())
}

fn get_tree_count(map: &Vec<Vec<char>>, down_vector: usize, horizontal_vector: u32) -> u32 {
  let max_rows = map.len();
  let max_row_length = map[0].len() as u32;
  let mut row: usize = down_vector;
  let mut idx: u32 = horizontal_vector;
  let mut count: u32 = 0;

  while row < max_rows {
    if map[row][idx as usize] == TREE {
      count += 1;
    }

    row += down_vector;
    idx += horizontal_vector;
    if idx >= max_row_length {
      idx = idx as u32 % max_row_length;
    }
  }

  count
}

fn main() {
  let mut map: Vec<Vec<char>> = vec![];

  if let Ok(lines) = read_lines(INPUT_PATH) {
    for line in lines {
      if let Ok(l) = line {
        map.push(l.chars().collect());
      }
    }
  }

  println!(
    "SUM: {}",
    get_tree_count(&map, 1usize, 1u32)
      * get_tree_count(&map, 1usize, 3u32)
      * get_tree_count(&map, 1usize, 5u32)
      * get_tree_count(&map, 1usize, 7u32)
      * get_tree_count(&map, 2usize, 1u32)
  );
}
