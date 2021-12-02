use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

fn main() {
  let mut count: u32 = 0;
  if let Ok(lines) = read_lines("./input.txt") {
    for line in lines {
      if let Ok(pass) = line {
        let mut min: u32 = 0;
        let mut max: u32 = 0;
        let mut key: char = '\0';

        let components: Vec<&str> = pass.split(": ").collect();
        get_requirements(components[0], &mut min, &mut max, &mut key);

        if verify_password(min, max, key, components[1]) {
          count += 1;
        }
      }
    }
  }

  println!("{}", count);
}

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where
  P: AsRef<Path>,
{
  let file = File::open(filename)?;
  Ok(io::BufReader::new(file).lines())
}

fn get_requirements(input: &str, min: &mut u32, max: &mut u32, key: &mut char) {
  let components: Vec<&str> = input.split(" ").collect();

  get_limits(components[0], min, max);
  *key = components[1].as_bytes()[0] as char;
}

fn get_limits(input: &str, min: &mut u32, max: &mut u32) {
  let components: Vec<&str> = input.split("-").collect();

  *min = components[0].parse::<u32>().unwrap();
  *max = components[1].parse::<u32>().unwrap();
}

fn verify_password(min: u32, max: u32, key: char, password: &str) -> bool {
  let pass = password.as_bytes();
  let in_first_pos: bool = pass[(min - 1) as usize] as char == key;
  let in_second_pos: bool = pass[(max - 1) as usize] as char == key;

  (in_first_pos || in_second_pos) && !(in_first_pos && in_second_pos)
}
