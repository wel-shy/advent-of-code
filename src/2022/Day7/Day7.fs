namespace AdventOfCode

module Day7 =
    let inputPath = "./Day7/input.txt"

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
        let key = [| parent; node.Name |] |> String.concat (".")
        acc |> Map.add key node

    let getParentFileName (n: string) =
        let split = n.Split(".")

        split[0 .. split.Length - 2] |> String.concat "."

    let handleCd (fs: Map<string, Node>) (command: string) =
        let split = command.Split(" ")

        if (split[ 2 ].Equals("..")) then
            parent <- getParentFileName (parent)
        else
            parent <- [| parent; split[2] |] |> String.concat (".")

        fs

    let handleCommand (acc: Map<string, Node>) (command: string) =
        match command with
        | command when command.Equals("$ ls") -> acc
        | command when command.Contains("$ cd") -> handleCd acc command
        | _ -> handleDirOrFile acc command

    let rec getDirectorySize (dir: Node) (fs: Map<string, Node>) =
        let id = [| dir.Parent; dir.Name |] |> String.concat (".")

        fs.Values
        |> Seq.filter (fun (x: Node) -> x.Parent.Equals(id))
        |> Seq.map (fun x -> if x.IsDirectory then getDirectorySize x fs else x.Weight)
        |> List.ofSeq
        |> List.sum

    let part1 =
        let limit = 100000
        let log = inputPath |> Utils.readAllLines

        let fs =
            log[1 .. (log.Length - 1)] |> List.fold handleCommand Map.empty<string, Node>

        fs.Values
        |> Seq.filter (fun x -> x.IsDirectory)
        |> Seq.map (fun x -> getDirectorySize x fs)
        |> Seq.filter (fun (size) -> size <= limit)
        |> List.ofSeq
        |> List.sum
