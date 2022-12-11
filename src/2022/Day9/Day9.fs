namespace AdventOfCode

module Day9 =
    let inputPath = "./Day9/input.txt"
    let mutable touched = Map.empty<string, bool>.Add ("0,0", true)
    let mutable tail: int * int = (0, 0)

    let squareDiff point =
        let (x, y) = point
        float (x - y) ** 2

    let getEuclidianDistance (a: int * int) (b: int * int) =
        let (ax, ay) = a
        let (bx, by) = b

        int (sqrt ((squareDiff (ax, bx)) + (squareDiff (ay, by))))

    let isNotTouching a b = (getEuclidianDistance a b) > 1

    let getNewPosition ins current =
        let (dir, amount) = ins
        let (x, y) = current

        match dir with
        | "R" -> (x + amount, y)
        | "L" -> (x - amount, y)
        | "U" -> (x, y + amount)
        | "D" -> (x, y - amount)
        | _ -> (x, y)

    let getLastKnownTailPosition (tails: List<int * int>) =
        if tails.Length > 0 then tails[tails.Length - 1] else tail

    let getTailPositions (tails: List<int * int>) (head: int * int) =
        let (tx, ty) = getLastKnownTailPosition tails

        let nextTx =
            match head with
            | (hx, _) when hx > tx -> tx + 1
            | (hx, _) when hx < tx -> tx - 1
            | _ -> tx

        let nextTy =
            match head with
            | (_, hy) when hy > ty -> ty + 1
            | (_, hy) when hy < ty -> ty - 1
            | _ -> ty

        [ (nextTx, nextTy) ] |> List.append tails

    let applyInstructionWithIterations iterations current (ins: string) =
        let arr = ins.Split(" ")

        for i in 0..iterations do
            let hPath =
                [ 1 .. (int arr[1]) ]
                |> List.mapi (fun i _ -> getNewPosition (arr[0], i + 1) current)

            let ans =
                hPath
                |> List.filter (fun x -> isNotTouching x tail)
                |> List.fold getTailPositions []

            if ans.Length > 0 then
                let touchedValues =
                    ans
                    |> List.fold (fun (acc: Map<string, bool>) (x, y) -> acc.Add($"{x},{y}", true)) touched

                touched <- touchedValues
                tail <- ans[ans.Length - 1]

        [ (0, 0) ][0 - 1]

    let part1 =
        let applyInstruction = applyInstructionWithIterations 1
        let _ = inputPath |> Utils.readAllLines |> List.fold applyInstruction (0, 0)
        let points = touched.Keys |> List.ofSeq

        ((points |> List.length))

    let part2 =
        let applyInstruction = applyInstructionWithIterations 10
        let _ = inputPath |> Utils.readAllLines |> List.fold applyInstruction (0, 0)
        let points = touched.Keys |> List.ofSeq

        ((points |> List.length))
