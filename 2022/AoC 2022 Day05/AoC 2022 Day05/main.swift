//
//  main.swift
//  AoC 2022 Day05
//
//  Created by Adam Horner on 06/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day05-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Stack Struct

struct Stack {
    private var items: [String] = []
    
    func peek() -> String {
        guard let topElement = items.first else { fatalError("This stack is empty.") }
        return topElement
    }
    
    mutating func pop() -> String {
        return items.removeFirst()
    }
  
    mutating func push(_ element: String) {
        items.insert(element, at: 0)
    }
    
    mutating func reverse() {
        items.reverse()
    }
}

//MARK: - variables and constants
let lines = inputText.split(separator: "\n", omittingEmptySubsequences: false)
var setupComplete = false
var stacks = Dictionary<String, Stack>()
stacks["1"] = Stack()
stacks["2"] = Stack()
stacks["3"] = Stack()
stacks["4"] = Stack()
stacks["5"] = Stack()
stacks["6"] = Stack()
stacks["7"] = Stack()
stacks["8"] = Stack()
stacks["9"] = Stack()

let stackPositions = [1,5,9,13,17,21,25,29,33]
let part1Mode = false

//MARK: - Main Loop
for line in lines {
    if let firstCharacter = line.first {
        if firstCharacter == "m" {
            print("processing an operation line: \(line)")
            let lineParts = line.split(separator: " ")
            assert(String(lineParts[0]) == "move")
            let quantity = Int(lineParts[1])!
            assert(String(lineParts[2]) == "from")
            let fromStack = String(lineParts[3])
            assert(String(lineParts[4]) == "to")
            let toStack = String(lineParts[5])
            if part1Mode {
                //part 1 mode, crane moves boxes one at a time
                for _ in 1...quantity {
                    let topItem = stacks[fromStack]?.pop()
                    stacks[toStack]?.push(topItem!)
                }
            } else {
                //part 2 mode, crane moves boxes a stack at a time
                var craneStack = Stack()
                for _ in 1...quantity {
                    craneStack.push((stacks[fromStack]?.pop())!)
                }
                for _ in 1...quantity {
                    stacks[toStack]?.push(craneStack.pop())
                }
            }
        } else {
            print("processing a setup line")
            let characters = line.split(separator: "")
            for i in stackPositions {
                let stackPosition = ((i-1)/4)+1
                let stackName = "\(stackPosition)"
                let stackItem = String(characters[i])
                if stackItem != " " {
                    stacks[stackName]?.push(stackItem)
                }
            }
        }
    } else {
        print("processing a blank line")
        // setup is done, so reverse the stacks before beginning move operations (unless we already did it)
        if !setupComplete {
            for i in 1...9 {
                // popping the first value should give us the name of the stack from description line underneath the stacks
                let stackName = stacks["\(i)"]?.pop()
                stacks["\(i)"]?.reverse()
                print(" reversed stack '\(stackName ?? "<nil>")' (should be the same as '\(i)'")
            }
            setupComplete = true
        }
    }
}
print("\nAll operations finished! Here are the items on top of the stacks, part 1 mode = \(part1Mode)")
var topString = ""
for i in 1...9 {
    topString += stacks["\(i)"]?.peek() ?? " "
}
print(topString)
