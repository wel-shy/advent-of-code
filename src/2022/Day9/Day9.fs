namespace AdventOfCode

module Day9 =
    let inputPath = "./Day9/input.txt"
    let mutable touched = Map.empty<string, bool>.Add ("500,500", true)
    let mutable tail: int * int = (0, 0)

    let squareDiff point =
        let (x, y) = point
        float (x - y) ** 2

    let getEuclidianDistance (a: int * int) (b: int * int) =
        let (ax, ay) = a
        let (bx, by) = b

        int (sqrt ((squareDiff (ax, bx)) + (squareDiff (ay, by))))

    let isTouching a b = (getEuclidianDistance a b) <= 1

    let getNewPosition ins current =
        let (dir, amount) = ins
        let (x, y) = current

        match dir with
        | "R" -> (x + amount, y)
        | "L" -> (x - amount, y)
        | "U" -> (x, y + amount)
        | "D" -> (x, y - amount)
        | _ -> (x, y)

    let length = 2

    let getLastTailPosition (path: List<int * int>) =
        match path.Length with
        | 0 -> tail
        | _ -> path[path.Length - 1]

    let getTPath hPath =
        let pointsFurhterThanAcceptedLength =
            hPath |> List.filter (fun x -> not (isTouching x tail))

        let idx =
            if pointsFurhterThanAcceptedLength.Length > 0 then
                hPath |> List.findIndex (fun x -> x = pointsFurhterThanAcceptedLength[0])
            else
                -1

        pointsFurhterThanAcceptedLength[0 .. (pointsFurhterThanAcceptedLength.Length) - 2]
        |> List.append (if idx > 0 then [ hPath[idx - 1] ] else [])

    let applyInstruction current (ins: string) =
        let arr = ins.Split(" ")

        let hPath =
            [ 1 .. (int arr[1]) ]
            |> List.mapi (fun i _ -> getNewPosition (arr[0], i + 1) current)

        let tPath = getTPath hPath

        let touchedValues =
            tPath
            |> List.fold (fun (acc: Map<string, bool>) (x, y) -> acc.Add($"{x},{y}", true)) touched

        printfn "%A" tPath

        touched <- touchedValues
        tail <- getLastTailPosition (tPath)

        hPath[hPath.Length - 1]

    let part1 =
        let x = inputPath |> Utils.readAllLines |> List.fold applyInstruction (500, 500)
        let points = touched.Keys |> List.ofSeq
        // printfn "%A" points
        // printfn "%A" ((points |> List.length) + 1)

        // printfn "%A" (getEuclidianDistance (4, 1) (4, 0))

        ((points |> List.length))
