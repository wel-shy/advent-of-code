namespace AdventOfCode

module Day1 =
    let inputPath = "./Day1/input.txt"

    let addElementToMDArray array value index =
        let mutable accCopy = List.toArray array
        accCopy.[index] <- Array.concat [ array.[index]; [| value |] ]
        Array.toList accCopy

    let reduceCalories (acc: List<array<int>>) (current: string) =
        if current.Equals("") then
            Array.empty<int> :: acc
        else
            addElementToMDArray acc (int current) 0

    let mapInputToElves =
        Utils.readAllLines (inputPath)
        |> List.fold reduceCalories [ Array.empty<int> ]
        |> List.map Array.sum

    let part1 = mapInputToElves |> List.max

    let part2 = mapInputToElves |> List.sortDescending |> List.take 3 |> List.sum
