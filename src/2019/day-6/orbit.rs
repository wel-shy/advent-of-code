use std::collections::HashMap;
use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

#[derive(Debug)]
struct Orbit {
    center: String,
    satellite: String,
}

const BASE: &str = "COM";

fn main() {
    //************ PART 1 ***********************************************************
    let mut orbits: Vec<Orbit> = vec![];
    if let Ok(lines) = read_lines("./input.txt") {
        for line in lines {
            if let Ok(l) = line {
                orbits.push(Orbit {
                    center: l.split(")").nth(0).unwrap().to_string(),
                    satellite: l.split(")").nth(1).unwrap().to_string(),
                })
            }
        }
    }

    let graph: HashMap<&str, &str> = orbits
        .iter()
        .map(|orbit| (orbit.satellite.as_str(), orbit.center.as_str()))
        .collect();

    let orbit_count: u32 = graph.keys().map(|x| count_orbits(x, &graph)).sum();
    println!("orbit count = {}", orbit_count);

    //************ PART 2 ***********************************************************
    let y_t_b_str: String = get_orbit_path(graph.get("YOU").unwrap(), &graph);
    let mut you_to_base: Vec<&str> = y_t_b_str.split(",").collect();
    let s_t_b_str: String = get_orbit_path(graph.get("SAN").unwrap(), &graph);
    let mut san_to_base: Vec<&str> = s_t_b_str.split(",").collect();
    you_to_base.reverse();
    san_to_base.reverse();

    let (y_dist, s_dist) = find_split_distance_to_split(you_to_base, san_to_base);

    // -2 to remove jump to common node
    println!("distance to santa {}", y_dist + s_dist - 2);
}

fn count_orbits(satellite: &str, graph: &HashMap<&str, &str>) -> u32 {
    if satellite == BASE {
        0
    } else {
        1 + count_orbits(graph.get(satellite).unwrap(), graph)
    }
}

fn get_orbit_path(satellite: &str, graph: &HashMap<&str, &str>) -> String {
    if satellite == BASE {
        BASE.to_string()
    } else {
        return format!(
            "{},{}",
            satellite.to_string(),
            get_orbit_path(graph.get(satellite).unwrap(), graph)
        );
    }
}

fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
where
    P: AsRef<Path>,
{
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}

fn find_split_distance_to_split(y_path: Vec<&str>, s_path: Vec<&str>) -> (usize, usize) {
    let mut distance_to_common_from_base: usize = 0;
    for i in 0..s_path.len() {
        if y_path[i] != s_path[i] {
            distance_to_common_from_base = i - 1;
            break;
        }
    }

    (
        y_path.len() - distance_to_common_from_base,
        s_path.len() - distance_to_common_from_base,
    )
}
