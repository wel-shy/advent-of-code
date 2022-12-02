namespace AdventOfCodeWarmUp

module Day2 =
    let inputPath = "../inputs/day_2.txt"

    let getSumForInstruction (lines: List<string>) (ins: string) =
        lines
        |> List.filter (fun (x: string) -> x.IndexOf(ins) >= 0)
        |> List.map (fun x -> int (x.Split(" ")[1]))
        |> List.sum

    let part1 =
        let lines = AdventOfCode.Utils.readAllLines (inputPath)
        let forward = getSumForInstruction lines "forward"
        let down = getSumForInstruction lines "down"
        let up = getSumForInstruction lines "up"

        ((down - up) * forward)

// let part2 =
//     let aim = 0
//     let depth = 0
//     let lines = AdventOfCode.Utils.readAllLines (inputPath)
//     let horizontal = getSumForInstruction lines "forward"

//     for line in lines do
//         let ins = line.Split(" ")[0]
//         let value = int (line.Split(" ")[1])

//         if ins.Equals("forward") then
//             depth = aim * value
//         elif ins.Equals("down") then
//             aim = value + aim
//         elif ins.Equals("up") then
//             aim = aim - value

//     printfn "%d %d" depth horizontal
//     depth * horizontalf
