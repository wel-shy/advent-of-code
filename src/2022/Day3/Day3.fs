namespace AdventOfCode

module Day3 =
    let inputPath = "./Day3/input.txt"
    let upperCaseCharAsciiDiff = 38
    let lowerCaseCharAsciiDiff = 96

    let splitStringInHalf (s: string) =
        let middle = (s.Length + 1) / 2
        [ s.ToCharArray()[0 .. middle - 1]; s.ToCharArray()[middle .. (s.Length - 1)] ]

    let intersectArrays (list: char[] seq) =
        list
        |> Seq.map (fun x -> x |> Set.ofArray)
        |> Seq.reduce (fun x acc -> Set.intersect x acc)
        |> Seq.head

    let parseLetterToScore (c: char) =
        let value = int c
        let lowerCaseBoundary = int 'a'

        match value with
        | value when value < lowerCaseBoundary -> value - upperCaseCharAsciiDiff
        | _ -> value - lowerCaseCharAsciiDiff

    let chunkList chunkSize list =
        list |> Seq.chunkBySize chunkSize |> Seq.toList

    let getScore chars =
        chars |> List.map parseLetterToScore |> List.sum

    let part1 =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x |> splitStringInHalf)
        |> List.map intersectArrays
        |> getScore

    let part2 =
        inputPath
        |> Utils.readAllLines
        |> chunkList 3
        |> List.map (fun x -> x |> Seq.map (fun x -> x.ToCharArray()))
        |> List.map (fun x -> x |> intersectArrays)
        |> getScore
