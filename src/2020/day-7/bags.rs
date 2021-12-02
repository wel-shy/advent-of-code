use std::collections::HashMap;
use std::fs;

const INPUT: &str = "./input.txt";

#[derive(Debug, Clone)]
struct Bag<'a> {
  name: &'a str,
  children: HashMap<&'a str, u32>,
}

fn main() {
  let contents: String = fs::read_to_string(INPUT).expect("Something went wrong reading the file");
  let temp: Vec<&str> = contents.split("\n").collect::<Vec<&str>>();

  let bags: Vec<Bag> = temp.into_iter().map(|b| line_to_bag(b)).collect();
  let bags_temp: Vec<Bag> = contents
    .split("\n")
    .collect::<Vec<&str>>()
    .into_iter()
    .map(|b| line_to_bag(b))
    .collect();

  // let valid_bag_count: u32 = bags
  //   .into_iter()
  //   .map(|b| bag_contains_target(b.name, &bags_temp, "shiny gold") as u32)
  //   .sum();

  let c: u32 = get_bag_count("shiny gold", &bags_temp);

  println!("{:#?}", c - 1);
}

fn line_to_bag<'a>(line: &'a str) -> Bag<'a> {
  let bags: Vec<&str> = line.split(", ").collect();
  let mut children: HashMap<&str, u32> = HashMap::new();
  for i in 1..bags.len() {
    children.insert(&bags[i][2..], bags[i][..1].parse::<u32>().unwrap());
  }
  Bag {
    name: bags[0],
    children,
  }
}

fn bag_contains_target<'a>(bag_name: &'a str, all_bags: &Vec<Bag>, target: &'a str) -> bool {
  let bag_opt: Option<&Bag> = get_bag(all_bags, bag_name);
  let bag: &Bag;
  if bag_opt.is_some() {
    bag = bag_opt.unwrap();
  } else {
    return false;
  }

  println!("{:#?}", bag);

  if bag.children.len() == 0 {
    return false;
  }

  let children_keys: Vec<&str> = bag.children.keys().cloned().collect();
  if children_keys.contains(&target) {
    return true;
  }

  let contains: u32 = children_keys
    .iter()
    .map(|x| bag_contains_target(x, all_bags, target) as u32)
    .sum();

  contains > 0
}

fn get_bag<'a>(bags: &'a Vec<Bag<'a>>, target: &'a str) -> Option<&'a Bag<'a>> {
  let bag: Option<&Bag> = None;
  for i in 0..bags.len() {
    if bags[i].name == target {
      return Some(&bags[i]);
    }
  }

  bag
}

fn get_bag_count(bag_name: &str, all_bags: &Vec<Bag>) -> u32 {
  let bag_opt: Option<&Bag> = get_bag(all_bags, bag_name);
  let bag: &Bag;
  if bag_opt.is_some() {
    bag = bag_opt.unwrap();
  } else {
    return 0;
  }

  if bag.children.len() == 0 {
    return 1;
  }

  let mut counter: u32 = 1;
  for (k, v) in bag.children.iter() {
    counter = counter + v * get_bag_count(k, all_bags);
  }

  counter
}
