use std::collections::{HashMap, HashSet};

#[derive(Debug)]
struct Orbit<'a> {
    center: &'a str,
    satellite: &'a str
}

fn main() {
    let map: Vec<&str> = vec!["COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L"];
    let orbits: Vec<Orbit> = map.iter().map(|s| Orbit {
        center: s.split(")").nth(0).unwrap().into(),
        satellite: s.split(")").nth(1).unwrap().into()
    }).collect();

    println!("{:#?}", orbits);

    let graph: HashMap<&str, &str> = orbits
        .iter()
        .map(|orbit| (orbit.satellite, orbit.center))
        .collect();

    let orbit_count: u32 = graph.keys().map(|x| count_orbits(x, &graph)).sum();

    println!("{:#?}", orbit_count);

}

fn count_orbits(satellite: &str, graph: &HashMap<&str, &str>) -> u32 {
    if satellite == "COM" {
        0
    } else {
        1 + count_orbits(graph.get(satellite).unwrap(), graph)
    }
}
