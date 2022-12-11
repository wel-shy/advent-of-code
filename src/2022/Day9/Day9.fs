namespace AdventOfCode

module Day9 =
    let inputPath = "./Day9/test.txt"
    let mutable touched = Map.empty<string, bool>
    let mutable tail: int * int = (0, 0)

    let squareDiff point =
        let (x, y) = point
        float (x - y) ** 2

    let getEuclidianDistance (a: int * int) (b: int * int) =
        int (sqrt ((squareDiff a) + (squareDiff b)))

    let getNewPosition ins current =
        let (dir, amount) = ins
        let (x, y) = current

        match dir with
        | "R" -> (x + amount, y)
        | "L" -> (x - amount, y)
        | "U" -> (x, y + amount)
        | "D" -> (x, y - amount)
        | _ -> (x, y)

    let isTouching head tail =
        let (hx, hy) = head
        let (tx, ty) = tail

        hx = tx && hy = ty

    let updateTail dir =
        let (tx, ty) = tail

        match dir with
        | "R" -> (tx + 1, ty)
        | "L" -> (tx - 1, ty)
        | "U" -> (tx, ty + 1)
        | "D" -> (tx, ty - 1)
        | _ -> tail

    let length = 2

    let getLastTailPosition (path: List<int * int>) =
        match path.Length with
        | 0 -> tail
        | _ -> path[path.Length - 1]

    let applyInstruction current (ins: string) =
        let arr = ins.Split(" ")

        let hPath =
            [ 1 .. (int arr[1]) ] |> List.map (fun x -> getNewPosition (arr[0], x) current)

        let tPath = hPath[0 .. hPath.Length - length]
        printfn "%A" tPath

        let touchedValues =
            tPath
            |> List.fold (fun (acc: Map<string, bool>) (x, y) -> acc.Add($"{x},{y}", true)) touched

        touched <- touchedValues
        tail <- getLastTailPosition (tPath)

        hPath[hPath.Length - 1]

    let part1 =
        let x = inputPath |> Utils.readAllLines |> List.fold applyInstruction (0, 0)
        let points = touched.Keys |> List.ofSeq
        printfn "%A" points
        printfn "%A" ((touched.Keys |> List.ofSeq |> List.length) + 1)
        points
