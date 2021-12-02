use std::fs;

const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let input: Vec<&str> = contents.split("\n").collect();
  let instructions: Vec<(&str, i32)> = input.into_iter().map(|x| line_to_inst(x)).collect();

  let (east, north) = get_vectors(&instructions);
  let manhattan_dist: u32 = (east.abs() + north.abs()) as u32;
  println!("Manhattan Distance: {}", manhattan_dist);
}

fn line_to_inst(line: &str) -> (&str, i32) {
  let d: &str = &line[..1];
  let dist: i32 = line[1..].parse::<i32>().unwrap();

  (d, dist)
}

fn get_vectors(inst: &Vec<(&str, i32)>) -> (i32, i32) {
  let mut e_w: i32 = 0;
  let mut n_s: i32 = 0;
  let mut bearing: i32 = 90;

  for i in inst.iter() {
    let (dir, dis) = i;

    println!("B: {}", bearing);
    match *dir {
      "F" => match bearing {
        0 => n_s += dis,
        90 => e_w += dis,
        180 => n_s -= dis,
        270 => e_w -= dis,
        _ => {}
      },
      _ => {
        update_vectors(&mut e_w, &mut n_s, &mut bearing, i);
      }
    }
  }

  (e_w, n_s)
}

fn update_vectors(e: &mut i32, n: &mut i32, b: &mut i32, inst: &(&str, i32)) {
  let (dir, dis) = inst;
  match *dir {
    "N" => *n += dis,
    "E" => *e += dis,
    "S" => *n -= dis,
    "W" => *e -= dis,
    "L" => {
      *b = {
        let mut x = (*b - dis) % 360;
        if x < 0 {
          x = 360 - x.abs();
        }

        x
      }
    }
    "R" => *b = (*b + dis) % 360,
    _ => {}
  }
}
