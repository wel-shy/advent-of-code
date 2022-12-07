namespace AdventOfCode

module Day7 =
    let inputPath = "./Day7/test.txt"

    type Node =
        { Name: string
          Parent: string
          Weight: int
          Children: Node list }

    let mutable currentNode =
        { Name = "/"
          Parent = null
          Weight = 0
          Children = List.empty<Node> }

    let nodes = Map.empty<string, Node>.Add ("/", currentNode)

    let mutable fs = List.empty<Node>

    let isNotFileOrDirectory (s: string) = s.Contains("$")

    let parseLinesToNode parentNode (line: string) =
        let split = line.Split(" ")

        match split[0] with
        | "dir" ->
            { Name = split[1]
              Parent = parentNode.Name
              Weight = 0
              Children = List.empty<Node> }
        | _ ->
            { Name = split[1]
              Parent = parentNode.Name
              Weight = int split[0]
              Children = List.empty<Node> }

    let handleList index (log: List<string>) =
        let remainingInstructions = log.[(index + 1) .. (log.Length - 1)]

        let x =
            remainingInstructions |> List.tryFindIndex (fun x -> x |> isNotFileOrDirectory)

        let endOfList =
            match x with
            | Some x -> x - 1
            | None -> remainingInstructions.Length - 1

        remainingInstructions[0..endOfList] |> List.map (parseLinesToNode currentNode)

    let handleCd (command: string) =
        printfn "cd: %s" command
        let split = command.Split(" ")

        if nodes.ContainsKey(split[2]) then
            currentNode <- nodes.[split[2]]
        else
            currentNode <-
                { Name = split[2]
                  Parent = currentNode.Name
                  Weight = 0
                  Children = List.empty<Node> }

            Map.add split[2] currentNode nodes

    let handleCommand list index (command: string) =
        match command with
        | command when command.Equals("$ ls") -> handleList index list
        | command when command.Contains("$ cd") -> handleCd command
        | _ -> null

    let part1 =
        let log = inputPath |> Utils.readAllLines
        let handler = handleCommand log[1 .. (log.Length - 1)]

        log[1 .. (log.Length - 1)] |> List.mapi handler
