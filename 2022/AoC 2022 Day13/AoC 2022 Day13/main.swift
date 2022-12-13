//
//  main.swift
//  AoC 2022 Day13
//
//  Created by Adam Horner on 13/12/2022.
//

import Foundation

//MARK: - Data Structures

class PuzzlePacket: Comparable, CustomStringConvertible {
    let integer: Int?
    var list: [PuzzlePacket]? = []
    var parent: PuzzlePacket?
    init(integer: Int) {
        self.integer = integer
        self.list = nil
    }
    init() {
        self.list = []
        self.integer = nil
    }
    func addToList(_ integer: Int) {
        addToList(PuzzlePacket(integer: integer))
    }
    func addToList(_ packet:PuzzlePacket) {
        list?.append(packet) ?? assertionFailure("Tried to add to a null list")
    }
    var description: String {
        if integer != nil {
            return String(integer!)
        }
        return "[\(list!.map{$0.description}.joined(separator: ", "))]"
    }
    
    static func < (lhs: PuzzlePacket, rhs: PuzzlePacket) -> Bool {
        if lhs.integer != nil && rhs.integer != nil {
            return lhs.integer! < rhs.integer!
        }
        if lhs.integer == nil && rhs.integer == nil {
            assert(lhs.list != nil && rhs.list != nil, "no integers and no lists found comparing PuzzlePackets")
            let loopTest = min(lhs.list!.count, rhs.list!.count)
            for i in 0..<loopTest {
                if lhs.list![i] < rhs.list![i] {
                    return true
                } else if lhs.list![i] > rhs.list![i] {
                    return false
                }
            }
            return lhs.list!.count < rhs.list!.count
        }
        if lhs.integer != nil {
            let lhsTempPuzzlePacket = PuzzlePacket() //list
            lhsTempPuzzlePacket.addToList(lhs.integer!)
            return lhsTempPuzzlePacket < rhs
        } else {
            assert(lhs.list != nil && rhs.integer != nil, "impossible logic case in PuzzlePacket comparator")
            let rhsTempPuzzlePacket = PuzzlePacket() //list
            rhsTempPuzzlePacket.addToList(rhs.integer!)
            return lhs < rhsTempPuzzlePacket
        }
    }
    
    static func == (lhs: PuzzlePacket, rhs: PuzzlePacket) -> Bool {
        if lhs.integer != nil {
            if rhs.integer == nil {
                return false
            }
            return lhs.integer! == rhs.integer!
        }
        if rhs.integer != nil {
            return false
        }
        if lhs.list!.count == rhs.list!.count {
            for i in 0..<lhs.list!.count {
                if lhs.list![i] != rhs.list![i] {
                    return false
                }
            }
            return true
        } else {
            return false
        }
    }
}

struct PacketPair {
    let left: PuzzlePacket
    let right: PuzzlePacket
}

//MARK: - Functions

func parsePairs(file: String) throws -> [PacketPair] {
    let rows = try String(contentsOfFile: file).split(separator: "\n")
    var packetPairs: [PacketPair] = []
    var lastPacket: PuzzlePacket?
    for row in rows {
        let debugRow = String(row)
        var additionPacket: PuzzlePacket?
        var currentPacket: PuzzlePacket?
        var integer: Int?
        for character in row.split(separator: "") {
            let debugChar = String(character)
            if character == "[" {
                if currentPacket == nil {
                    currentPacket = PuzzlePacket()
                    additionPacket = currentPacket
                } else {
                    let nextPacket = PuzzlePacket()
                    nextPacket.parent = currentPacket
                    currentPacket!.addToList(nextPacket)
                    currentPacket = nextPacket
                }
            } else if character == "]" {
                //end of list
                if integer != nil {
                    currentPacket!.addToList(integer!)
                    integer = nil
                }
                if currentPacket?.parent != nil {
                    currentPacket = currentPacket?.parent
                }
            } else if character == "," {
                if integer != nil {
                    currentPacket!.addToList(integer!)
                    integer = nil
                }
            } else {
                // this has to be an integer here, so force conversion
                let newInteger: Int = Int(character)!
                if integer == nil {
                    integer = newInteger
                } else {
                    integer = (integer! * 10) + newInteger
                }
            }
        }
        if lastPacket == nil {
            assert(additionPacket != nil)
            lastPacket = additionPacket
        } else {
            packetPairs.append(PacketPair(left: lastPacket!, right: additionPacket!))
            lastPacket = nil
        }
    }
    print(packetPairs.count)
    return packetPairs
}

//MARK: - Main Loop

let testFile = "/Users/adam/Development/Personal/AdventOfCode/2022/day13-test.txt"
let dataFile = "/Users/adam/Development/Personal/AdventOfCode/2022/day13-data.txt"

var packetPairs = try! parsePairs(file: dataFile)

var index = 0
var accumulator = 0
for pair in packetPairs {
    index += 1
    //print("comparing: \n  left: \(pair.left)\n right: \(pair.right)")
    if pair.left < pair.right {
        accumulator += index
        //print("packets are in order")
    } else {
        //print("packets are not in order")
    }
}
print("Part 1: sum of indexes in order is: \(accumulator)")
