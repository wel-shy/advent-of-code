namespace AdventOfCode

module Day8 =

    let inputPath = "./Day8/input.txt"

    let getAColumn (c: int) (A: List<List<int>>) =
        let column =
            A
            |> List.fold (fun (acc: List<int>) (cur: List<int>) -> List.append acc [ cur[c] ]) List.empty<int>

        column

    let isTallest (list: List<int>) tree = tree > (list |> List.max)

    let getFromLeft (list: List<int>) (index: int) = list[0 .. (index - 1)]

    let getFromRight (list: List<int>) (index: int) =
        list[index + 1 .. list.Length - 1] |> List.rev

    let getFromTopWithTrees trees x y =
        let column = getAColumn x trees
        column[0 .. (y - 1)]

    let getFromBottonWithTrees trees x y =
        let column = getAColumn x trees
        column[(y + 1) .. (column.Length - 1)]

    let isExterior (map: List<List<int>>) x y =
        if y = 0 then true
        elif x = 0 then true
        elif y = map.Length - 1 then true
        elif x = map[y].Length - 1 then true
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

    let getDistanceWithTrees limit (path: List<int>) =
        let isBiggerMap = path |> List.mapi (fun i x -> isBiggerThanPrevious limit path i)
        let firstSmaller = isBiggerMap |> List.tryFindIndex (fun x -> x = false)

        match firstSmaller with
        | Some x -> path[0 .. (x)] |> Seq.length
        | _ -> isBiggerMap.Length

    // path[0 .. (firstSmaller - 1)] |> Seq.length

    let solve (map: List<List<int>>) x y =
        if isExterior map x y then
            true
        else
            let row = map[y]
            let t = map[y][x]

            let left = getFromLeft row x |> List.rev
            let right = getFromRight row x |> List.rev
            let top = getFromTopWithTrees map x y
            let bottom = getFromBottonWithTrees map x y

            isVisible [| left; right; top; bottom |] t

    let getScenicScore (map: List<List<int>>) x y =
        let row = map[y]
        let t = map[y][x]
        let getDistance = getDistanceWithTrees t

        let left = getFromLeft row x |> List.rev
        let right = getFromRight row x |> List.rev
        let top = getFromTopWithTrees map x y |> List.rev
        let bottom = getFromBottonWithTrees map x y

        [| left; right; top; bottom |]
        |> Array.map getDistance
        |> Array.fold (fun acc cur -> acc * cur) 1

    let getTopography =
        inputPath
        |> Utils.readAllLines
        |> List.map (fun x -> x |> Seq.toList |> List.map (fun y -> int y - int '0'))

    let part1 =
        let trees = getTopography

        trees
        |> List.mapi (fun y list -> list |> List.mapi (fun x _ -> solve trees x y))
        |> List.map (fun x -> x |> List.filter (fun isVisible -> isVisible) |> List.length)
        |> List.sum

    let part2 =
        let trees = getTopography

        trees
        |> List.mapi (fun y list -> list |> List.mapi (fun x _ -> getScenicScore trees x y))
        |> List.map List.max
        |> List.max
