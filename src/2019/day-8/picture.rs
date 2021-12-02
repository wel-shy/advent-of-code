use std::fs;

const ROWS: u32 = 6;
const MAX_ROW_LENGTH: u32 = 25;
const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let row_idx: u32 = 0;
  let col_idx: u32 = 0;

  let mut picture: Vec<Vec<char>> = vec![];

  for c in contents.chars() {
    picture[row_idx]
  }
}
