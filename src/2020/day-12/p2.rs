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

  // 10 east, north 1.
  let mut n_way: i32 = 1;
  let mut e_way: i32 = 10;

  for i in inst.iter() {
    let (dir, dis) = i;

    println!("n: {}, e: {}", n_s, e_w);
    match *dir {
      "F" => {
        e_w += e_way * dis;
        n_s += n_way * dis;
      }
      "N" => n_way += dis,
      "E" => e_way += dis,
      "S" => n_way -= dis,
      "W" => e_way -= dis,
      "R" => match dis {
        90 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ne;
          n_way = ce * -1;
        }
        180 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ce * -1;
          n_way = ne * -1;
        }
        270 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ne * -1;
          n_way = ce;
        }
        _ => {}
      },
      "L" => match dis {
        90 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ne * -1;
          n_way = ce;
        }
        180 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ce * -1;
          n_way = ne * -1;
        }
        270 => {
          let ce = e_way;
          let ne = n_way;

          e_way = ne;
          n_way = ce * -1;
        }
        _ => {}
      },
      _ => {}
    }
  }

  (e_w, n_s)
}
