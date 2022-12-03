namespace AdventOfCode

module Day3 =
    let inputPath = "./Day3/input.txt"

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
        | value when value < lowerCaseBoundary -> value - 38
        | _ -> value - 96

    let part1 =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x |> splitStringInHalf)
        |> List.map (fun x -> (Set.ofArray x[0], Set.ofArray x[1]))
        |> List.map (fun (x, y) -> Set.intersect x y |> Set.toList)
        |> List.map (fun x -> x[0])
        |> List.map (fun x -> parseLetterToScore x)
        |> List.sum

    let part2 =
        inputPath
        |> Utils.readAllLines
        |> Seq.ofList
        |> Seq.chunkBySize 3
        |> Seq.map (fun x -> x |> Seq.map (fun x -> x.ToCharArray()))
        |> Seq.map (fun x -> x |> intersectArrays)
        |> Seq.map (fun x -> parseLetterToScore x)
        |> Seq.sum
