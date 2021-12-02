use std::collections::HashMap;
use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let input: Vec<&str> = contents.split("\n\n").collect();
  let mut instructions: Vec<(&str, HashMap<u32, &str>)> = vec![];
  let mut memory: HashMap<u32, &str> = HashMap::new();

  for line in input.iter() {
    let lines: Vec<&str> = line.split("\n").collect();
    let mask: &str = lines[0].split(" = ").collect::<Vec<&str>>()[1];
    let mut map: HashMap<u32, &str> = HashMap::new();

    for i in 1..lines.len() {
      let l: Vec<&str> = lines[i].split(" = ").collect();
      map.insert(
        l[0].parse::<u32>().unwrap(),
        &format!("{:036b}", l[1].parse::<u32>().unwrap()),
      );
    }

    instructions.push((mask, map));
  }

  // println!("{:#?}", instructions);
  apply_mask(&instructions[0], &mut memory);
}

fn apply_mask(
  instructions: &(&str, HashMap<u32, &str>),
  memory: &mut HashMap<u32, &str>,
) -> String {
  let (mask, inst) = instructions;
  let init_val: String = format!("{:036b}", 0);

  for (key, value) in inst.iter() {
    println!("{} / {}", key, value);

    let val_addr: &str;
    if memory.contains_key(key) {
      val_addr = memory.get(key).unwrap();
    } else {
      val_addr = &init_val;
    }

    let new_val: String = write_val(value, mask, val_addr);
    new_val
    // println!("{:#?}", new_val);
  }

  // println!("{}", format!("{:036b}", l[0].parse::<u32>().unwrap()));
}

fn write_val(val: &str, mask: &str, curr: &str) -> String {
  let v_chars: Vec<char> = val.chars().collect();
  let m_chars: Vec<char> = mask.chars().collect();
  let mut c_chars: Vec<char> = curr.chars().collect();

  for i in 0..v_chars.len() {
    match m_chars[i] {
      'X' => c_chars[i] = v_chars[i],
      '0' => c_chars[i] = '0',
      '1' => c_chars[i] = '1',
      _ => {}
    }
  }

  c_chars.into_iter().collect::<String>()
}
