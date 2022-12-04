namespace AdventOfCode

module Day4 =
    let inputPath = "./Day4/test.txt"

    let rangeToSet (range: string) =
        let bounds = range.Split("-") |> Array.map int
        [| for i in bounds[0] .. bounds[1] -> i |] |> Set.ofArray

    let isSuperSet (tuple: Set<int> * Set<int>) =
        let (set1, set2) = tuple

        if Set.isSubset set1 set2 || Set.isSubset set2 set1 then
            1
        else
            0

    let isIntersectingSet (tuple: Set<int> * Set<int>) =
        let (set1, set2) = tuple

        if Set.intersect set1 set2 |> Set.isEmpty then 0 else 1

    let part1 =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x.Split(","))
        |> List.map (fun x -> x |> Array.map rangeToSet)
        |> List.map (fun x -> (x[0], x[1]))
        |> List.map isSuperSet
        |> List.sum

    let part2 =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x.Split(","))
        |> List.map (fun x -> x |> Array.map rangeToSet)
        |> List.map (fun x -> (x[0], x[1]))
        |> List.map isIntersectingSet
        |> List.sum
