namespace AdventOfCode

module Day8 =

    let inputPath = "./Day8/input.txt"

    let getTopography =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x |> Seq.toList |> List.map (fun y -> int y - int '0'))

    let trees = getTopography

    let getColumn = Utils.getColumnFrom2DArray trees

    let getFromLeft (list: List<int>) (index: int) = list[0 .. (index - 1)]

    let getFromRight (list: List<int>) (index: int) =
        list[index + 1 .. list.Length - 1] |> List.rev

    let getFromTopWithTrees x y =
        let column = getColumn x
        column[0 .. (y - 1)]

    let getFromBottonWithTrees x y =
        let column = getColumn x
        column[(y + 1) .. (column.Length - 1)]

    let isExterior x y =
        if y = 0 then true
        elif x = 0 then true
        elif y = trees.Length - 1 then true
        elif x = trees[y].Length - 1 then true
        else false

    let isVisible directions tree =
        let toCheck =
            directions
            |> List.ofArray
            |> List.map (fun x -> x |> List.max)
            |> List.filter (fun item -> tree > item)
            |> List.length

        toCheck > 0

    let isBiggerThanPrevious limit (path: List<int>) cur = path[cur] < limit

    let getAllDirections x y =
        let row = trees[y]

        let left = getFromLeft row x |> List.rev
        let right = getFromRight row x |> List.rev
        let top = getFromTopWithTrees x y |> List.rev
        let bottom = getFromBottonWithTrees x y

        [| left; right; top; bottom |]

    let getDistanceWithTrees limit (path: List<int>) =
        let isBiggerMap = path |> List.mapi (fun i x -> isBiggerThanPrevious limit path i)
        let firstSmaller = isBiggerMap |> List.tryFindIndex (fun x -> x = false)

        match firstSmaller with
        | Some x -> path[0 .. (x)] |> Seq.length
        | _ -> isBiggerMap.Length

    let solve x y =
        if isExterior x y then
            true
        else
            let t = trees[y][x]
            let directions = getAllDirections x y

            isVisible directions t

    let getScenicScore x y =
        let t = trees[y][x]
        let getDistance = getDistanceWithTrees t

        getAllDirections x y
        |> Array.map getDistance
        |> Array.fold (fun acc cur -> acc * cur) 1

    let applyToEveryPoint fn =
        List.mapi (fun y list -> list |> List.mapi (fun x _ -> fn x y))

    let part1 =
        trees
        |> applyToEveryPoint solve
        |> List.map (fun x -> x |> List.filter (fun isVisible -> isVisible) |> List.length)
        |> List.sum

    let part2 =
        trees |> applyToEveryPoint getScenicScore |> List.map List.max |> List.max
