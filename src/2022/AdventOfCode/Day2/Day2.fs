namespace AdventOfCode

module Day2 =
    let inputPath = "./Day2/input.txt"

    type ResultScore =
        | WIN = 6
        | DRAW = 3
        | LOSS = 0

    let selectionMap = [ "X", "A"; "Y", "B"; "Z", "C" ] |> Map.ofList
    let itemScore = [ "A", 1; "B", 2; "C", 3 ] |> Map.ofList
    let losingPairs = [ "A", "C"; "B", "A"; "C", "B" ] |> Map.ofList
    let winingPairs = [ "C", "A"; "A", "B"; "B", "C" ] |> Map.ofList

    let parseLineToTuple (line: string) =
        let x = line.Split(" ")
        (x[0], x[1])

    let getLines =
        Utils.readAllLines (inputPath)
        |> List.map (fun (x: string) -> parseLineToTuple x)

    let getResultScore l r =
        let isWin = winingPairs[l] = r
        let isDraw = l = r

        match isWin with
        | true -> ResultScore.WIN
        | false ->
            match isDraw with
            | true -> ResultScore.DRAW
            | false -> ResultScore.LOSS

    let getScore (x, y) =
        itemScore[y] + LanguagePrimitives.EnumToValue(getResultScore x y)

    let getMatchingPair move =
        let (x, y) = move

        match y with
        | "Y" -> x
        | "Z" -> winingPairs[x]
        | _ -> losingPairs[x]

    let part1: int =
        getLines |> List.map (fun (x, y) -> getScore (x, selectionMap[y])) |> List.sum

    let part2 =
        getLines
        |> List.map (fun (x, y) -> (x, getMatchingPair ((x, y))))
        |> List.map (fun (x) -> getScore x)
        |> List.sum
