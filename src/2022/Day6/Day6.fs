namespace AdventOfCode

module Day6 =
    let inputPath = "./Day6/test.txt"

    let isSetUnique len (set: Set<char>) = set.Count = len

    let isMarker markerLength = Set.ofList >> isSetUnique markerLength

    let isPreviousSequenceUnique sequenceLength (originalList: List<char>) index _char =
        match index with
        | index when index < sequenceLength - 1 -> false
        | _ -> originalList.[index - sequenceLength + 1 .. index] |> isMarker sequenceLength

    let getFirstLineOfFile = Utils.readAllLines >> List.head

    let getInputAsChars =
        let line = inputPath |> getFirstLineOfFile
        line.ToCharArray() |> List.ofArray

    let getFirstTruthyValue = List.findIndex (fun x -> x) >> (+) 1

    let solve seqLen originalList =
        List.mapi (isPreviousSequenceUnique seqLen originalList) >> getFirstTruthyValue

    let part1 =
        let chars = getInputAsChars
        chars |> solve 4 chars

    let part2 =
        let chars = getInputAsChars
        chars |> solve 14 chars
