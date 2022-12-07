namespace AdventOfCode

module Day7 =
    let inputPath = "./Day7/test.txt"

    type Node =
        { Name: string
          Parent: string
          Weight: int
          IsDirectory: bool }

    let mutable parent = "/"

    let createNode (rawName: string) =
        let split = rawName.Split(" ")
        let isDir = split[ 0 ].Equals("dir")

        match isDir with
        | true ->
            { Name = split[1]
              Parent = parent
              Weight = 0
              IsDirectory = true }
        | false ->
            { Name = split[1]
              Parent = parent
              Weight = int split[0]
              IsDirectory = false }

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

    let rec getDirectorySize (dir: string) (fs: Map<string, Node>) =
        fs.Values
        |> Seq.filter (fun (x: Node) -> x.Parent.Equals(dir))
        |> Seq.map (fun x ->
            if x.IsDirectory then
                getDirectorySize x.Name fs
            else
                x.Weight)
        |> Seq.sum

    let part1 =
        let log = inputPath |> Utils.readAllLines

        let fs =
            log[1 .. (log.Length - 1)] |> List.fold handleCommand Map.empty<string, Node>

        getDirectorySize "d" fs
