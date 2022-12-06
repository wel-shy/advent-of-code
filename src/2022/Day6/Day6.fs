namespace AdventOfCode

module Day6 =
    let inputPath = "./Day6/test.txt"

    let isPreviousSequenceUnique sequenceLength (originalList: List<char>) index _char =
        if index < sequenceLength - 1 then
            false
        else
            let l = originalList.[index - sequenceLength + 1 .. index]
            let sequence = l |> Set.ofList
            sequence.Count = sequenceLength

    let part1 =
        let line = inputPath |> Utils.readAllLines |> List.head
        let chars = line.ToCharArray() |> List.ofArray

        let solver = isPreviousSequenceUnique 4 chars
        chars |> List.mapi solver |> List.findIndex (fun x -> x) |> (+) 1

    let part2 =
        let line = inputPath |> Utils.readAllLines |> List.head
        let chars = line.ToCharArray() |> List.ofArray

        let solver = isPreviousSequenceUnique 14 chars
        chars |> List.mapi solver |> List.findIndex (fun x -> x) |> (+) 1
