use std::collections::HashSet;
use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let temp: Vec<&str> = contents.split("\n\n").collect::<Vec<&str>>();

  // part 1
  // let ans: Vec<HashSet<char>> = temp
  //   .into_iter()
  //   .map(|t| line_to_set(&t.replace("\n", "")))
  //   .collect();

  // part 2
  let ans: Vec<HashSet<char>> = temp.into_iter().map(|t| line_to_intersection(t)).collect();
  let count: usize = ans.into_iter().map(|s| s.len()).sum::<usize>();
  println!("{:#?}", count);
}

fn line_to_set(line: &str) -> HashSet<char> {
  let chars: Vec<char> = line.chars().collect();
  chars.iter().cloned().collect()
}

fn line_to_intersection(line: &str) -> HashSet<char> {
  let persons: Vec<&str> = line.split("\n").collect();
  let persons_hash: Vec<HashSet<char>> = persons
    .into_iter()
    .map(|p| p.chars().collect::<Vec<char>>().iter().cloned().collect())
    .collect();

  let mut inter: HashSet<char> = persons_hash[0].clone();

  for i in 1..persons_hash.len() {
    inter = inter.intersection(&persons_hash[i]).copied().collect();
  }

  inter
}
