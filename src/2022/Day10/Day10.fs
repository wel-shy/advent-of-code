namespace AdventOfCode

module Day10 =
    let inputPath = "./Day10/input.txt"
    let rowLength = 40
    let getRow = [ for _ in 0 .. (rowLength - 1) -> " " ]
    let rows = [ for _ in 0..5 -> getRow ]

    let parseToCycle (instruction: string) =
        let arr = instruction.Split(" ")

        match arr[0] with
        | "noop" -> [ 0 ]
        | "addx" -> [ 0; int arr[1] ]
        | _ -> []

    let getCycles = Utils.readAllLines >> List.map parseToCycle >> List.collect id
    let cycles = inputPath |> getCycles

    let getXAtCycleWithInitialValue initialValue (list: List<int>) cycle =
        let valueAtCycle = list[0 .. cycle - 2] |> List.sum // skip falling cycle values
        valueAtCycle + initialValue

    let getValueAtCycle = getXAtCycleWithInitialValue 1 cycles

    let getStrengthAtCycle cycle = (getValueAtCycle cycle) * cycle

    let applyCycle index _ =
        let spriteValue = getValueAtCycle (index + 1)
        let spritePosition = [ (spriteValue - 1) .. (spriteValue + 1) ]
        let writerIndex = index % rowLength
        let isWritingSprite = spritePosition |> List.contains writerIndex

        match isWritingSprite with
        | true -> "#"
        | _ -> " "

    let part1 =
        [ 20; 60; 100; 140; 180; 220 ] |> List.map getStrengthAtCycle |> List.sum

    let part2 =
        cycles
        |> List.mapi applyCycle
        |> Seq.ofList
        |> Seq.chunkBySize rowLength
        |> List.ofSeq
        |> List.map (fun x -> String.concat "" x)
