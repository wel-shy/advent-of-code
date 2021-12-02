use std::fs;

const SEAT: char = 'L';
const PERSON: char = '#';
const FLOOR: char = '.';
const WALL: char = '_';
const INPUT: &str = "./input.txt";

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let mut map: Vec<Vec<char>> = contents
    .split("\n")
    .into_iter()
    .map(|s| s.chars().collect())
    .collect::<Vec<Vec<char>>>();

  let mut changes: u16 = 1;
  while changes != 0 {
    changes = update_map(&mut map);
  }

  println!("{:#?}", count_occupied(&map));
}

fn count_occupied(map: &Vec<Vec<char>>) -> u16 {
  let mut count: u16 = 0;
  for i in 0..map.len() {
    for j in 0..map[i].len() {
      if map[i][j] == PERSON {
        count += 1;
      }
    }
  }
  count
}

fn get_seat(map: &Vec<Vec<char>>, direction: (i8, i8), col_idx: i8, row_idx: i8) -> char {
  fn is_out_of_bounds(map: &Vec<Vec<char>>, col_idx: i8, row_idx: i8) -> bool {
    col_idx < 0
      || col_idx > map.len() as i8 - 1
      || row_idx < 0
      || row_idx > map[col_idx as usize].len() as i8 - 1
  }

  let mut c = col_idx.clone();
  let mut r = row_idx.clone();

  let (cvec, rvec) = direction;

  c += cvec;
  r += rvec;

  if is_out_of_bounds(&map, c, r) {
    return WALL;
  }

  let mut next: char = map[c as usize][r as usize];
  let mut has_next: bool = true;
  while has_next {
    if next != FLOOR {
      return next;
    }

    c += cvec;
    r += rvec;

    if is_out_of_bounds(&map, c, r) {
      has_next = false;
      continue;
    }
    next = map[c as usize][r as usize];
  }

  WALL
}

fn update_map(map: &mut Vec<Vec<char>>) -> u16 {
  let state = map.clone();
  let mut changes: u16 = 0;

  for i in 0..state.len() {
    for j in 0..state[i].len() {
      let left = get_seat(&state, (0, -1), i as i8, j as i8);
      let right = get_seat(&state, (0, 1), i as i8, j as i8);
      let tl: char = get_seat(&state, (1, -1), i as i8, j as i8);
      let tr: char = get_seat(&state, (1, 1), i as i8, j as i8);
      let bottom: char = get_seat(&state, (-1, 0), i as i8, j as i8);
      let top: char = get_seat(&state, (1, 0), i as i8, j as i8);
      let bl: char = get_seat(&state, (-1, -1), i as i8, j as i8);
      let br: char = get_seat(&state, (-1, 1), i as i8, j as i8);

      let empty_spaces: Vec<char> = vec![SEAT, WALL, FLOOR];
      let adj: Vec<char> = vec![tl, top, tr, left, right, bl, bottom, br];

      let occupied_count = adj
        .clone()
        .into_iter()
        .map(|a| !empty_spaces.contains(&a) as u8)
        .sum::<u8>();

      if occupied_count == 0 && state[i][j] == SEAT {
        map[i][j] = PERSON;
        changes += 1;
      }

      if state[i][j] == PERSON && occupied_count >= 5 {
        map[i][j] = SEAT;
        changes += 1;
      }
    }
  }

  changes
}
