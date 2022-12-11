namespace AdventOfCode

open System.IO

module Utils =
    let readAllLines fileName =
        let lines = File.ReadAllLines(fileName)
        let list = Seq.toList lines

        list

    let getColumnFrom2DArray (A: List<List<int>>) (c: int) =
        let column =
            A
            |> List.fold (fun (acc: List<int>) (cur: List<int>) -> List.append acc [ cur[c] ]) List.empty<int>

        column
