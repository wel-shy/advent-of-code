use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let lines: Vec<&str> = contents.split("\n").collect::<Vec<&str>>();

  for i in 0..lines.len() {
    let mut prog: Vec<&str> = lines.clone();
    let opt_to_change: String;

    if lines[i].contains("jmp") {
      opt_to_change = lines[i].replace("jmp", "nop");
    } else if lines[i].contains("nop") {
      opt_to_change = lines[i].replace("nop", "jmp");
    } else {
      continue;
    }

    prog[i] = &opt_to_change;
    let (res, completed) = run_program(&prog);

    if completed {
      println!("{}, {}", res, completed);
      break;
    }
  }
}

fn parse_line<'a>(line: &'a str) -> (&'a str, i32) {
  let split: Vec<&str> = line.split(" ").collect();

  (split[0], split[1].parse::<i32>().unwrap())
}

fn contains_idx(arr: &Vec<i32>, idx: i32) -> bool {
  for i in 0..arr.len() {
    if arr[i] == idx {
      return true;
    }
  }

  false
}

fn run_program(program: &Vec<&str>) -> (i32, bool) {
  let mut visited: Vec<i32> = vec![];
  let mut acc: i32 = 0;
  let mut i: i32 = 0;

  while i >= 0 && (i as usize) < program.len() {
    if contains_idx(&visited, i) {
      return (acc, false);
    }

    let (inst, val) = parse_line(program[i as usize]);
    visited.push(i);
    match inst {
      "acc" => {
        acc = acc + val;
        i = i + 1;
      }
      "jmp" => i = i + val,
      "nop" | _ => i = i + 1,
    }
  }

  (acc, true)
}
