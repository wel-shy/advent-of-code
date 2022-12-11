namespace AdventOfCode

module Day10 =
    let inputPath = "./Day10/input.txt"

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

    let part1 =
        [ 20; 60; 100; 140; 180; 220 ] |> List.map getStrengthAtCycle |> List.sum
