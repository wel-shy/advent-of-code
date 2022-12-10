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


    let solve (map: List<List<int>>) x y =
        if isExterior map x y then
            true
        else
            let row = map[y]
            let t = map[y][x]

            let left = getFromLeft row x
            let right = getFromRight row x
            let top = getFromTopWithTrees map x y
            let bottom = getFromBottonWithTrees map x y

            let toCheck =
                [| left; right; top; bottom |]
                |> List.ofArray
                |> List.map (fun x -> x |> List.max)
                |> List.filter (fun tree -> t > tree)
                |> List.length

            toCheck > 0

    let part1 =
        let trees =
            inputPath
            |> Utils.readAllLines
            |> List.map (fun x -> x |> Seq.toList |> List.map (fun y -> int y - int '0'))

        trees
        |> List.mapi (fun y list -> list |> List.mapi (fun x _ -> solve trees x y))
        |> List.map (fun x -> x |> List.filter (fun isVisible -> isVisible) |> List.length)
        |> List.sum
