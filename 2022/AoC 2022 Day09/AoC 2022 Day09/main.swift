//
//  main.swift
//  AoC 2022 Day09
//
//  Created by Adam Horner on 09/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day09-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Data Structures

struct Point: Hashable, Equatable, CustomStringConvertible {
    var x: Int
    var y: Int
    
    var description: String {
        return "(\(x), \(y))"
    }
    
    init() {
        self.x = 0
        self.y = 0
    }
    
    init(x: Int, y: Int) {
        self.x = x
        self.y = y
    }
}

class Rope: CustomStringConvertible {
    var head = Point()
    var tail = Point()
    var tailHistory: Set = [Point()]
    
    public var description: String {
        return "Rope with head: \(head), and tail: \(tail). TailHistory: \(tailHistory)"
    }
    
    func moveHead(up: Bool) {
        let newY = head.y + (up ? 1 : -1)
        head = Point(x: head.x, y: newY)
        moveTail()
    }
    
    func moveHead(right: Bool) {
        let newX = head.x + (right ? 1 : -1)
        head = Point(x: newX, y: head.y)
        moveTail()
    }
    
    private func moveTail() {
        if head == tail {
            return
        }
        var newX = tail.x
        var newY = tail.y
        if tail.x < head.x-1 {
            newX = head.x - 1
            newY = head.y
        } else if tail.x > head.x+1 {
            newX = head.x + 1
            newY = head.y
        }
        if tail.y < head.y-1 {
            newX = head.x
            newY = head.y - 1
        } else if tail.y > head.y+1 {
            newX = head.x
            newY = head.y + 1
        }
        tail = Point(x: newX, y: newY)
        //add tail to history
        tailHistory.insert(tail)
    }
}

//MARK: - Main loop

let rope = Rope()
let lines = inputText.split(separator: "\n")
print(rope)
for line in lines {
    let components = line.split(separator: " ")
    let distance = Int(components[1])!
    print("executing movement \(line)")
    for _ in 1...distance {
        let command = components[0]
        switch command {
        case "U", "D":
            rope.moveHead(up: command == "U")
        case "L", "R":
            rope.moveHead(right: command == "R")
        default:
            fatalError("Unexpected Command \(command)")
        }
        //print(rope)
        //print(rope.tailHistory.count)
    }
}

print("Tail has covered \(rope.tailHistory.count) points")
