namespace AdventOfCode

module Day8 =

    let inputPath = "./Day8/test.txt"
    let visibilityMap = Map.empty<string, int>

    let getExteriorCount (list: List<int[]>) =
        let v = (list |> List.length)
        let h = (list[0].Length - 2)

        (v + h) * 2

    let isTallerThanPrevious (row: int[]) index =
        if index = 0 then
            true
        else if index = 1 then
            row[1] > row[0]
        else
            let previous = row[0 .. (index - 1)]
            let max = previous |> Array.max

            row[index] > max

    let mapRow (row: int[]) =
        let solver = isTallerThanPrevious row
        row |> Array.mapi (fun x _y -> solver x)

    let getAColumn (c: int) (A: List<int[]>) =
        let column =
            A
            |> List.fold (fun (acc: List<int>) (cur: int[]) -> List.append acc [ cur[c] ]) List.empty<int>

        column

    let rotate (A: List<int[]>) =
        seq {
            for i in 0 .. A[0].Length - 1 do
                yield getAColumn (i) A
        }
        |> (List.ofSeq >> List.map Array.ofList)

    let rotateBack2DArray A =
        let mutable map = List.empty<List<int>>

        map


    let part1 =
        let trees =
            inputPath
            |> Utils.readAllLines
            |> List.map (fun x -> x |> Seq.toArray |> Array.map (fun y -> int y - int '0'))

        let extCount = trees |> getExteriorCount
        let fromLeft = trees |> List.map mapRow

        let fromRight =
            trees |> List.map (fun row -> mapRow (row |> Array.rev)) |> List.map Array.rev

        let fromTop = trees |> rotate |> List.ofSeq |> List.map (fun row -> mapRow (row))

        let fromBotton =
            trees
            |> rotate
            |> List.map Array.rev
            |> List.map (fun row -> mapRow (row))
            |> List.map Array.rev

        fromTop
