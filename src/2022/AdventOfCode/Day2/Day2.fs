namespace AdventOfCode

module Day2 =
    let inputPath = "./Day2/input.txt"

    type Result =
        | WIN
        | DRAW
        | LOSS

    type ResultScore =
        | WIN = 6
        | DRAW = 3
        | LOSS = 0

    let itemScore = [ "A", 1; "B", 2; "C", 3 ] |> Map.ofList
    let losingPairs = [ "A", "C"; "B", "A"; "C", "B" ] |> Map.ofList
    let winingPairs = [ "C", "A"; "A", "B"; "B", "C" ] |> Map.ofList
    let selectionMap = [ "X", "A"; "Y", "B"; "Z", "C" ] |> Map.ofList

    let parseInputToResult requiredResult =
        match requiredResult with
        | "X" -> LOSS
        | "Y" -> DRAW
        | "Z" -> WIN
        | _ -> failwith "Invalid input"

    let parseLineToTuple (line: string) =
        let x = line.Split(" ")
        (x[0], x[1])

    let getLines filePath =
        Utils.readAllLines (filePath)
        |> List.map (fun (x: string) -> parseLineToTuple x)

    let getResultScore oponent you =
        let isWin = winingPairs[oponent] = you
        let isDraw = oponent = you

        match isWin with
        | true -> ResultScore.WIN
        | false ->
            match isDraw with
            | true -> ResultScore.DRAW
            | false -> ResultScore.LOSS

    let getScore (x, y) =
        itemScore[y] + LanguagePrimitives.EnumToValue(getResultScore x y)

    let getMatchingPair move =
        match move with
        | (oponent, DRAW) -> oponent
        | (oponent, WIN) -> winingPairs[oponent]
        | (oponent, LOSS) -> losingPairs[oponent]

    let part1: int =
        inputPath
        |> getLines
        |> List.map (fun (oponent, requiredResult) -> getScore (oponent, selectionMap[requiredResult]))
        |> List.sum

    let part2 =
        inputPath
        |> getLines
        |> List.map (fun (oponent, requiredResult) ->
            (oponent, getMatchingPair ((oponent, parseInputToResult requiredResult))))
        |> List.map (fun (pair) -> getScore pair)
        |> List.sum
