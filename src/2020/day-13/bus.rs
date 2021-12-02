use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let input: Vec<&str> = contents.split("\n").collect();
  let r_busses = input[1].to_string();
  let bus_ids: Vec<u32> = r_busses
    .split(",")
    .collect::<Vec<&str>>()
    .into_iter()
    .filter(|i| *i != "x")
    .map(|i| i.parse::<u32>().unwrap())
    .collect();

  let arrival_time: u32 = input[0].parse::<u32>().unwrap();

  // Part 1
  // let (id, time) = get_next_departure(&bus_ids, arrival_time);
  // println!("Part 1: {}", id * (time - arrival_time));

  let id_off = get_ids_and_offsets(&r_busses);
  println!("ids-off: {:#?}", get_sync_timestamp(&id_off));
}

fn get_next_departure(ids: &Vec<u32>, arrival_time: u32) -> (u32, u32) {
  let mut next_time: u32 = u32::MAX;
  let mut next_id: u32 = 0;

  for id in ids.iter() {
    let mut base: u32 = arrival_time / id;
    if base % id != 0 {
      base += 1;
    }

    let next_depart = id * base;
    if next_depart < next_time {
      next_time = next_depart;
      next_id = *id;
    }
  }

  (next_id, next_time)
}

fn get_ids_and_offsets(line: &str) -> Vec<(u32, u32)> {
  let mut ids_and_offsets: Vec<(u32, u32)> = vec![];

  for (i, id) in line.split(",").collect::<Vec<&str>>().iter().enumerate() {
    if *id != "x" {
      ids_and_offsets.push((id.parse::<u32>().unwrap(), i as u32));
    }
  }

  ids_and_offsets
}

fn get_sync_timestamp(input: &Vec<(u32, u32)>) -> u64 {
  let (init, _) = input[0];
  let mut t: u64 = init as u64 + 100000000000000;
  //                             1202161486
  loop {
    for i in 0..input.len() {
      let (id, offset) = input[i];
      let m = (t + offset as u64) % id as u64 == 0;
      if !m {
        break;
      }

      if i == input.len() - 1 {
        return t + offset as u64;
      }
    }
    t += init as u64;
    // println!("t = {}", t)
  }
}
