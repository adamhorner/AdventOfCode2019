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
    var ropePoints: [Point]
    var tailHistory: Set = [Point()]
    var head: Point {
        return ropePoints[0]
    }
    var tail: Point {
        return ropePoints[ropePoints.count-1]
    }

    init(ropeSize: Int) {
        assert(ropeSize >= 2)
        ropePoints = []
        for _ in 0..<ropeSize {
            ropePoints.append(Point())
        }
    }
    
    public var description: String {
        return "Rope with points: \(ropePoints).\n TailHistory: \(tailHistory)"
    }
    
    func moveHead(up: Bool) {
        let newY = head.y + (up ? 1 : -1)
        ropePoints[0] = Point(x: head.x, y: newY)
        moveTail()
    }
    
    func moveHead(right: Bool) {
        let newX = head.x + (right ? 1 : -1)
        ropePoints[0] = Point(x: newX, y: head.y)
        moveTail()
    }
    
    private func moveTail() {
        for i in 1..<ropePoints.count {
            let previousKnot = ropePoints[i-1]
            let currentKnot = ropePoints[i]
            if previousKnot != currentKnot {
                var newX = currentKnot.x
                var newY = currentKnot.y
                if currentKnot.x < previousKnot.x-1 {
                    newX = previousKnot.x - 1
                    newY = previousKnot.y
                } else if currentKnot.x > previousKnot.x+1 {
                    newX = previousKnot.x + 1
                    newY = previousKnot.y
                }
                if currentKnot.y < previousKnot.y-1 {
                    newX = previousKnot.x
                    newY = previousKnot.y - 1
                } else if currentKnot.y > previousKnot.y+1 {
                    newX = previousKnot.x
                    newY = previousKnot.y + 1
                }
                ropePoints[i] = Point(x: newX, y: newY)
            }
        }
        //add tail to history
        tailHistory.insert(tail)
    }
}

//MARK: - Main loop

let lines = inputText.split(separator: "\n")

func simulateRope(size: Int) -> Int {
    let rope = Rope(ropeSize: size)
    for line in lines {
        let components = line.split(separator: " ")
        let distance = Int(components[1])!
        //print("executing movement \(line)")
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
    //print(rope)
    return rope.tailHistory.count
}

for i in 2...30 {
    print("Rope tail for rope of size \(i) covers \(simulateRope(size: i)) points")
}
