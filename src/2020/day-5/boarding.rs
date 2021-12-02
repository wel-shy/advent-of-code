use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let lines: Vec<&str> = contents.split("\n").collect();
  let mut ids: Vec<u32> = lines.into_iter().map(|s| find_seat_id(s)).collect();
  ids.sort();

  for i in 0..ids.len() {
    if i != 0 && ids[i] - 1 != ids[i - 1] {
      println!("{:#?}", ids[i] - 1);
      break;
    }
  }
}

fn parse_from_binary(input: &str) -> u32 {
  let bin_str = input
    .replace("F", "0")
    .replace("B", "1")
    .replace("L", "0")
    .replace("R", "1");

  u32::from_str_radix(&bin_str, 2).unwrap()
}

fn find_seat_id(input: &str) -> u32 {
  parse_from_binary(&input[..7]) * 8 + parse_from_binary(&input[7..])
}

/**
 * Implementation used to get the answer, realised it was a binary
 * encoding afterwards.
 */
// fn search(input: &str, upper_limit: u32) -> u32 {
//   let mut l: u32 = 0;
//   let mut r: u32 = upper_limit;
//   let mut idx: usize = 0;
//   let chars: Vec<char> = input.chars().collect();

//   while idx < chars.len() && l != r {
//     match chars[idx] {
//       'B' | 'R' => l = ((r + l) / 2) + 1,
//       'F' | 'L' => r = (r + l) / 2,
//       _ => println!("Illegal insturction"),
//     }

//     idx += 1;
//   }

//   l
// }
