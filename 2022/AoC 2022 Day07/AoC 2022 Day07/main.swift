//
//  main.swift
//  AoC 2022 Day07
//
//  Created by Adam Horner on 07/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day07-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Data Structures

struct File {
    let name: String
    let size: Int
}

class Directory {
    let name: String
    //TODO: it would be nice to wrap the parent directory logic entirely inside this class
    let parent: Directory?
    var directories = [Directory]()
    var files = [File]()
    
    init(name: String, parent: Directory?) {
        self.name = name
        self.parent = parent
        if parent == nil {
            assert(name == "/")
        }
    }
    
    func add(directory: Directory) {
        directories.append(directory)
    }
    
    func add(emptyDirectoryName: String) {
        directories.append(Directory(name: emptyDirectoryName, parent: self))
    }
    
    func add(file: File) {
        files.append(file)
    }
    
    func size() -> Int {
        let dirsSize = directories.reduce(0) { $0 + $1.size() }
        let filesSize = files.reduce(0) { $0 + $1.size }
        return dirsSize + filesSize
    }
    
    func directorySizeAccumulator(accumulator: [Int], maxSize: Int) -> [Int] {
        var returnArray = accumulator
        var mySize = self.size()
        if mySize <= maxSize {
            returnArray.append(mySize)
        }
        for directory in directories {
            returnArray = directory.directorySizeAccumulator(accumulator: returnArray, maxSize: maxSize)
        }
        return returnArray
    }
    
    func directorySizeAccumulator(accumulator: [Int], minSize: Int) -> [Int] {
        var returnArray = accumulator
        var mySize = self.size()
        if mySize >= minSize {
            returnArray.append(mySize)
        }
        for directory in directories {
            returnArray = directory.directorySizeAccumulator(accumulator: returnArray, minSize: minSize)
        }
        return returnArray
    }

    func getSubDirectory(called: String) -> Directory? {
        return directories.first { $0.name == called  }
    }
}

//MARK: - Parsing Loop

let rootDirectory = Directory(name: "/", parent: nil)
var currentWorkingDirectory = rootDirectory

for line in inputText.split(separator: "\n") {
    let tokens = line.components(separatedBy: " ")
    if tokens[0] == "$" {
        //this is a command
        switch tokens[1] {
        case "ls":
            // don't need to do anything
            break
        case "cd":
            if tokens[2] == ".." {
                // if we try to go above the parent directory, the input is broken, so we crash: force unwrap
                currentWorkingDirectory = currentWorkingDirectory.parent!
            } else if tokens[2] == "/" {
                currentWorkingDirectory = rootDirectory
            } else if let workingDirectoryUnwrapped = currentWorkingDirectory.getSubDirectory(called: tokens[2]) {
                currentWorkingDirectory = workingDirectoryUnwrapped
            } else {
                print("Error: Could not change directory: \(line)")
            }
        default:
            print("Error: Found an unexpected command: \(line)")
        }
    } else if tokens[0] == "dir" {
        //directory in listing, adding a directory to CWD
        currentWorkingDirectory.add(emptyDirectoryName: tokens[1])
    } else {
        //file in listing, first token should be size, second is name
        currentWorkingDirectory.add(file: File(name: tokens[1], size: Int(tokens[0])!))
    }
}

//MARK: - Results

let spaceUsed = rootDirectory.size()
print("Total root directory size: \(spaceUsed)")
let partOneAnswer = rootDirectory.directorySizeAccumulator(accumulator: [], maxSize: 100000).reduce(0, +)
print("Accumulated size of all folders no more then 100,000: \(partOneAnswer)")
let TotalSize = 70000000
let spaceNeeded = 30000000
assert(spaceUsed <= TotalSize)
assert(spaceUsed >= TotalSize - spaceNeeded)
let spaceToBeFreed = spaceUsed - (TotalSize - spaceNeeded)
print("Space needed to be freed: \(spaceToBeFreed)")
let partTwoAnswer = rootDirectory.directorySizeAccumulator(accumulator: [], minSize: spaceToBeFreed).reduce(Int.max) { min($0, $1)}
print("Smallest directory that can be deleted has size: \(partTwoAnswer)")
