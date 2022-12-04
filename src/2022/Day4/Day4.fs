namespace AdventOfCode

module Day4 =
    let inputPath = "./Day4/test.txt"

    let rangeToSet (range: string) =
        let bounds = range.Split("-") |> Array.map int
        [| for i in bounds[0] .. bounds[1] -> i |] |> Set.ofArray

    let getRangesAsSets =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x.Split(","))
        |> List.map (fun x -> x |> Array.map rangeToSet)
        |> List.map (fun x -> (x[0], x[1]))

    let isSuperSet (tuple: Set<int> * Set<int>) =
        let (set1, set2) = tuple
        Set.isSubset set1 set2 || Set.isSubset set2 set1

    let isIntersectingSet (tuple: Set<int> * Set<int>) =
        let (set1, set2) = tuple
        Set.intersect set1 set2 |> Set.isEmpty

    let sumBools list =
        list |> List.filter (fun x -> x) |> List.length

    let part1 = getRangesAsSets |> List.map isSuperSet |> sumBools

    let part2 = getRangesAsSets |> List.map isIntersectingSet |> sumBools
