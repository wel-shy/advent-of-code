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

    let applyInstructionToStack (stacks: string[] list) (instruction: Instruction) =
        let { stack = stack
              count = count
              destination = destination } =
            instruction

        let sourceStack = stacks[stack]

        let payload =
            sourceStack[sourceStack.Length - count .. sourceStack.Length - 1] |> Array.rev

        let destinationStack = stacks[destination]
        let newDestinationStack: string[] = Array.concat [ destinationStack; payload ]
        let newSourceStack = sourceStack.[0 .. sourceStack.Length - count - 1]

        stacks
        |> List.mapi (fun i x ->
            if i = destination then newDestinationStack
            elif i = stack then newSourceStack
            else x)

    let applyInstructionToStackV2 (stacks: string[] list) (instruction: Instruction) =
        let { stack = stack
              count = count
              destination = destination } =
            instruction

        let sourceStack = stacks[stack]

        let payload = sourceStack[sourceStack.Length - count .. sourceStack.Length - 1]

        let destinationStack = stacks[destination]
        let newDestinationStack: string[] = Array.concat [ destinationStack; payload ]
        let newSourceStack = sourceStack.[0 .. sourceStack.Length - count - 1]

        stacks
        |> List.mapi (fun i x ->
            if i = destination then newDestinationStack
            elif i = stack then newSourceStack
            else x)


    let part1 =
        let x = inputPath |> Utils.readAllLines
        let indexOfBreak = x |> List.findIndex (fun x -> x = "")

        let stacks = x[0 .. (indexOfBreak - 1)] |> List.map (parseStackFromLine)

        let instructions =
            x[(indexOfBreak + 1) .. (x.Length - 1)]
            |> List.map parseInstructionFromLine
            |> List.map (fun x ->
                { stack = x[1] - 1
                  count = x[0]
                  destination = x[2] - 1 })


        instructions
        |> List.fold applyInstructionToStack stacks
        |> List.map (fun x -> x[x.Length - 1])
        |> String.concat ""

    let part2 =
        let x = inputPath |> Utils.readAllLines
        let indexOfBreak = x |> List.findIndex (fun x -> x = "")
        let stacks = x[0 .. (indexOfBreak - 1)] |> List.map (parseStackFromLine)

        let instructions =
            x[(indexOfBreak + 1) .. (x.Length - 1)]
            |> List.map parseInstructionFromLine
            |> List.map (fun x ->
                { stack = x[1] - 1
                  count = x[0]
                  destination = x[2] - 1 })

        instructions
        |> List.fold applyInstructionToStackV2 stacks
        |> List.map (fun x -> x[x.Length - 1])
        |> String.concat ""
