fn main() {
  let min: u32 = 136760;
  let max: u32 = 595730;
  let mut passwords: Vec<u32> = vec![];

  for password in min..max {
    if is_increasing(password) && contains_double(password) {
      passwords.push(password.clone());
    }
  }

  println!("{}", passwords.len());
}

fn is_increasing(password: u32) -> bool {
  let str_pass: String = password.to_string();
  let mut previous: u32 = 0;
  let mut increasing: bool = true;

  for c in str_pass.chars() {
    let curr = c.to_digit(10).unwrap();
    if curr < previous {
      increasing = false;
      break;
    }

    previous = curr;
  }

  increasing
}

fn contains_double(password: u32) -> bool {
  let str_pass: String = password.to_string();
  let mut previous: u32 = 0;
  let mut double: bool = false;
  let mut repeat_count = 1;

  for c in str_pass.chars() {
    let curr = c.to_digit(10).unwrap();
    if curr == previous {
      repeat_count = repeat_count + 1;
    } else {
      if repeat_count == 2 {
        break;
      }
      repeat_count = 1;
    }
    previous = curr;
  }
  repeat_count == 2
}
