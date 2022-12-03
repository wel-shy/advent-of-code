namespace AdventOfCodeWarmUp

module Day1 =
    let inputPath = "./inputs/day_1.txt"
    let windowSize = 3

    let getWindowSum (list: List<int>) windowSize index =
        list[(index - windowSize) .. (index - 1)] |> List.sum

    let mapToGreaterThanPrevious list =
        list |> List.mapi (fun i x -> if i > 0 && x > list[i - 1] then 1 else 0)

    let mapToGreaterThanPreviousWindow list =
        list
        |> List.mapi (fun i x ->
            let isGreaterThanPreviousWindow =
                getWindowSum list windowSize i > getWindowSum list windowSize (i - 1)

            match isGreaterThanPreviousWindow && i >= windowSize with
            | true -> 1
            | false -> 0)

    let part1 =
        inputPath
        |> AdventOfCode.Utils.readAllLines
        |> List.map int
        |> mapToGreaterThanPrevious
        |> List.sum

    let part2 =
        inputPath
        |> AdventOfCode.Utils.readAllLines
        |> List.map int
        |> mapToGreaterThanPreviousWindow
        |> List.sum
