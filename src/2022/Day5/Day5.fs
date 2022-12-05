namespace AdventOfCode

module Day5 =
    let inputPath = "./Day5/input.txt"

    type Instruction =
        { stack: int
          count: int
          destination: int }

    let parseStackFromLine (line: string) =
        line.Split(" ") |> Array.map (fun x -> x.Replace("[", "").Replace("]", ""))

    let parseInstructionFromLine (line: string) =
        line
        |> System.Text.RegularExpressions.Regex("[0-9]+").Matches
        |> Seq.map (fun x -> int x.Value)
        |> Seq.toList

    let getPayload count isV2 (sourceStack: List<string>) =
        let payload = sourceStack[sourceStack.Length - count .. sourceStack.Length - 1]
        if isV2 then payload else payload |> List.rev

    let applyInstructionToStack (isV2: bool) (stacks: string[] list) (instruction: Instruction) =
        let { stack = stack
              count = count
              destination = destination } =
            instruction

        let sourceStack = stacks[stack]
        let payload = sourceStack |> List.ofArray |> getPayload count isV2 |> List.toArray

        stacks
        |> List.mapi (fun i x ->
            match i with
            | i when i = destination -> Array.concat [ stacks[destination]; payload ]
            | i when i = stack -> sourceStack.[0 .. sourceStack.Length - count - 1]
            | _ -> x)

    let getInstructions breakIndex (lines: List<string>) =
        lines[(breakIndex + 1) .. (lines.Length - 1)]
        |> List.map parseInstructionFromLine
        |> List.map (fun x ->
            { stack = x[1] - 1
              count = x[0]
              destination = x[2] - 1 })

    let getTopOfStacks (stacks: List<string[]>) =
        stacks |> List.map (fun x -> x.[x.Length - 1]) |> String.concat ("")

    let getInstructionsAndStacks =
        let lines = inputPath |> Utils.readAllLines
        let indexOfBreak = lines |> List.findIndex (fun x -> x = "")
        let stacks = lines[0 .. (indexOfBreak - 1)] |> List.map parseStackFromLine
        let instructions = lines |> getInstructions indexOfBreak

        (instructions, stacks)

    let part1 =
        let (instructions, stacks) = getInstructionsAndStacks
        let solveStack = applyInstructionToStack false

        instructions |> List.fold solveStack stacks |> getTopOfStacks

    let part2 =
        let (instructions, stacks) = getInstructionsAndStacks
        let solveStack = applyInstructionToStack true

        instructions |> List.fold solveStack stacks |> getTopOfStacks
