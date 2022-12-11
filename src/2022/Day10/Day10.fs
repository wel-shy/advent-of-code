namespace AdventOfCode

module Day10 =
    let inputPath = "./Day10/input.txt"
    let rowLength = 40
    let rowUpperBoundaries = [ 40; 80; 120; 160; 200; 240 ]
    let getRow = [ for _ in 0 .. (rowLength - 1) -> " " ]
    let rows = [ for _ in 0..5 -> getRow ]

    let parseToCycle (instruction: string) =
        let arr = instruction.Split(" ")

        match arr[0] with
        | "noop" -> [ 0 ]
        | "addx" -> [ 0; int arr[1] ]
        | _ -> []

    let cycles =
        inputPath |> Utils.readAllLines |> List.map parseToCycle |> List.collect id

    let getXAtCycleWithInitialValue initialValue (list: List<int>) cycle =
        let valueAtCycle = list[0 .. cycle - 2] |> List.sum // skip falling cycle values
        valueAtCycle + initialValue

    let getValueAtCycle = getXAtCycleWithInitialValue 1 cycles

    let getStrengthAtCycle cycle = (getValueAtCycle cycle) * cycle

    let applyCycle index _ =
        let spriteValue = getValueAtCycle (index + 1)
        let spritePosition = [ (spriteValue - 1) .. (spriteValue + 1) ]
        let writerIndex = (index) % 40

        match writerIndex with
        | index when spritePosition |> List.contains (index) -> "#"
        | _ -> " "

    let part2 =
        let ans = cycles |> List.mapi applyCycle

        ans
        |> Seq.ofList
        |> Seq.chunkBySize 40
        |> List.ofSeq
        |> List.map (fun x -> String.concat "" x)
