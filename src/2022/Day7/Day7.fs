namespace AdventOfCode

module Day7 =
    let inputPath = "./Day7/test.txt"

    type Node =
        { Name: string
          Parent: string
          Weight: int
          Children: Node list }

    let mutable parent = "/"

    let createNode (rawName: string) =
        let split = rawName.Split(" ")
        let isDir = split[ 0 ].Equals("dir")

        match isDir with
        | true ->
            { Name = split[1]
              Parent = parent
              Weight = 0
              Children = List.empty<Node> }
        | false ->
            { Name = split[1]
              Parent = parent
              Weight = int split[0]
              Children = List.empty<Node> }

    let handleDirOrFile (acc: Map<string, Node>) (name: string) =
        let node = createNode name
        acc |> Map.add node.Name node

    let handleCd (fs: Map<string, Node>) (command: string) =
        printfn "cd: %s" command
        let split = command.Split(" ")

        if (split[ 2 ].Equals("..")) then
            parent <- fs.[parent].Parent
        else
            parent <- split[2]

        fs

    let handleCommand (acc: Map<string, Node>) (command: string) =
        match command with
        | command when command.Equals("$ ls") -> acc
        | command when command.Contains("$ cd") -> handleCd acc command
        | _ -> handleDirOrFile acc command

    let part1 =
        let log = inputPath |> Utils.readAllLines

        log[1 .. (log.Length - 1)] |> List.fold handleCommand Map.empty<string, Node>
