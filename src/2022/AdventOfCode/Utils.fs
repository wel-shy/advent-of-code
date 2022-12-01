namespace AdventOfCode

open System
open System.IO

module Utils =
    let readAllLines fileName =
        let lines = File.ReadAllLines(fileName)
        let list = Seq.toList lines

        list
