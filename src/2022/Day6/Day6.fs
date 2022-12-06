namespace AdventOfCode

module Day6 =
    let inputPath = "./Day6/test.txt"

    let isSetUnique len (set: Set<char>) = set.Count = len

    let isPreviousSequenceUnique sequenceLength (originalList: List<char>) index _char =
        match index with
        | index when index < sequenceLength - 1 -> false
        | _ ->
            originalList.[index - sequenceLength + 1 .. index]
            |> Set.ofList
            |> isSetUnique sequenceLength

    let getInputAsChars =
        let line = inputPath |> Utils.readAllLines |> List.head
        line.ToCharArray() |> List.ofArray

    let getFirstTruthyValue list =
        list |> List.findIndex (fun x -> x) |> (+) 1

    let part1 =
        let chars = getInputAsChars
        let solver = isPreviousSequenceUnique 4 chars

        chars |> List.mapi solver |> getFirstTruthyValue

    let part2 =
        let chars = getInputAsChars
        let solver = isPreviousSequenceUnique 14 chars

        chars |> List.mapi solver |> getFirstTruthyValue
