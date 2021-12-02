use std::collections::HashMap;
use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let temp: Vec<&str> = contents.split("\n\n").collect::<Vec<&str>>();
  let passports: Vec<HashMap<&str, &str>> = temp.iter().map(|pass| to_passport_map(pass)).collect();
  let valid_count: u32 = passports
    .iter()
    .map(|p| is_valid_passport(p) as u32)
    .sum::<u32>();

  println!("Valid count: {:#?}", valid_count);
}

fn to_passport_map(p: &str) -> HashMap<&str, &str> {
  let pairs: Vec<&str> = p.split(|c| c == ' ' || c == '\n').collect();
  let mut map: HashMap<&str, &str> = HashMap::new();

  for pair in pairs.iter() {
    let k: &str = pair.split(":").collect::<Vec<&str>>()[0];
    let v: &str = pair.split(":").collect::<Vec<&str>>()[1];
    map.insert(k, v);
  }

  map
}

fn is_valid_passport(map: &HashMap<&str, &str>) -> bool {
  let expected_keys: Vec<&str> = vec!["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
  let valid_hex_chars: Vec<char> = vec![
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
  ];
  let valid_pid_chars: Vec<char> = vec!['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

  for k in expected_keys.iter() {
    if !map.contains_key(k) {
      return false;
    }

    let is_valid: bool;
    let v: &str = map.get(k).unwrap();
    match *k {
      "byr" => is_valid = is_valid_year(v, 2002, 1920),
      "iyr" => is_valid = is_valid_year(v, 2020, 2010),
      "eyr" => is_valid = is_valid_year(v, 2030, 2020),
      "hgt" => is_valid = is_valid_height(v),
      "hcl" => is_valid = is_valid_string(v, 7, Some('#'), &valid_hex_chars),
      "ecl" => is_valid = is_valid_eye_color(v),
      "pid" => is_valid = is_valid_string(v, 9, None, &valid_pid_chars),
      _ => is_valid = false,
    }

    if !is_valid {
      return false;
    }
  }

  true
}

fn is_valid_year(year: &str, max: i32, min: i32) -> bool {
  let n: i32 = year.parse::<i32>().unwrap();

  n <= max && n >= min
}

fn is_valid_height(height: &str) -> bool {
  let i_idx = height.find("in");
  let c_idx = height.find("cm");

  let idx: usize;
  if i_idx.is_some() {
    idx = i_idx.unwrap()
  } else if c_idx.is_some() {
    idx = c_idx.unwrap();
  } else {
    return false;
  }

  let val: u32 = height[..idx].parse::<u32>().unwrap();
  let unit: &str = &height[idx..];

  match unit {
    "cm" => return val >= 150 && val <= 193,
    "in" => return val >= 59 && val <= 76,
    _ => return false,
  }
}

/**
 * Too lazy to setup the project properly to use the redux cargo package.
*/
fn is_valid_string(s: &str, len: usize, first_char: Option<char>, valid_chars: &Vec<char>) -> bool {
  if s.len() != len {
    return false;
  }

  let mut s_to_check: &str = s;
  if first_char.is_some() {
    if s.chars().nth(0).unwrap() != first_char.unwrap() {
      return false;
    }

    s_to_check = &s[1..];
  }

  let mut has_invalid_char: bool = false;
  for c in s_to_check.chars() {
    if !valid_chars.contains(&c) {
      has_invalid_char = true;
      break;
    }
  }

  !has_invalid_char
}

fn is_valid_eye_color(color: &str) -> bool {
  let colors: Vec<&str> = vec!["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];
  colors.contains(&color)
}
