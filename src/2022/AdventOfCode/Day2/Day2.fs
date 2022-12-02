namespace AdventOfCode

module Day2 =
    let inputPath = "./Day2/input.txt"

    let scores =
        [ "A", "R"; "B", "P"; "C", "S"; "X", "R"; "Y", "P"; "Z", "S" ] |> Map.ofList

    let itemScore = [ "R", 1; "P", 2; "S", 3; "X", 0; "Y", 3; "Z", 6 ] |> Map.ofList

    let compareMove l r =
        let lhs = r
        let rhs = l

        if lhs = rhs then 3
        elif lhs = "A" && rhs = "B" then 6
        elif lhs = "B" && rhs = "C" then 6
        elif lhs = "C" && rhs = "A" then 6
        else 0

    let getScore (move: string[]) =
        let lu = scores[move[1]]
        let result = compareMove move[1] move[0]
        let score = itemScore[lu]

        score + result

    let getWinningPair move =
        if move = "A" then "B"
        elif move = "B" then "C"
        else "A"

    let getLosingPair move =
        if move = "A" then "C"
        elif move = "B" then "A"
        else "B"

    let getMatchingPair (move: string[]) =
        if move[1] = "Y" then move[0]
        elif move[1] = "Z" then getWinningPair (move[0])
        else getLosingPair (move[0])

    let part1 =
        let lines =
            Utils.readAllLines (inputPath)
            |> List.map (fun (x: string) -> getScore (x.Split(" ")))
            |> List.sum

        lines

    let part2 =
        let lines =
            Utils.readAllLines (inputPath)
            |> List.map (fun (x: string) -> x.Split(" "))
            |> List.map (fun (x: string[]) -> [| x[0]; getMatchingPair x |])
            |> List.map (fun (x) -> getScore x)
            |> List.sum

        lines
