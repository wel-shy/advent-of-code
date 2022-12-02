namespace AdventOfCode

module Day2 =
    let inputPath = "./Day2/input.txt"

    let winningPairs = [ "A", "C"; "B", "A"; "C", "B" ] |> Map.ofList
    let losingPairs = [ "C", "A"; "A", "B"; "B", "C" ] |> Map.ofList

    let getLines =
        Utils.readAllLines (inputPath) |> List.map (fun (x: string) -> x.Split(" "))

    type ResultScore =
        | WIN = 6
        | DRAW = 3
        | LOSS = 0

    let getResultScore l r =
        let isWin = (l = "A" && r = "B") || (l = "B" && r = "C") || (l = "C" && r = "A")

        if l = r then ResultScore.DRAW
        elif isWin then ResultScore.WIN
        else ResultScore.LOSS

    let getScore (move: string[]) =
        let itemScore = [ "A", 1; "B", 2; "C", 3 ] |> Map.ofList

        itemScore[move[1]]
        + LanguagePrimitives.EnumToValue(getResultScore move[0] move[1])

    let getMatchingPair (move: string[]) =
        if move[1] = "Y" then move[0]
        elif move[1] = "Z" then losingPairs[move[0]]
        else winningPairs[move[0]]

    let part1: int =
        let selectionMap = [ "X", "A"; "Y", "B"; "Z", "C" ] |> Map.ofList

        getLines
        |> List.map (fun (x: string[]) -> getScore ([| x[0]; selectionMap[x[1]] |]))
        |> List.sum

    let part2 =
        getLines
        |> List.map (fun (x: string[]) -> [| x[0]; getMatchingPair x |])
        |> List.map (fun (x) -> getScore x)
        |> List.sum
