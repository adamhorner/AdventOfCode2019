//
//  main.swift
//  AoC 2022 Day10
//
//  Created by Adam Horner on 11/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day10-data.txt"
let testfile = "/Users/adam/Development/Personal/AdventOfCode/2022/day10-test.txt"

func getInstructions(from file: String) throws -> [String] {
    var fileText: String
    fileText = try String(contentsOfFile: file)
    return fileText.components(separatedBy: "\n")
}

var registerX = 1
var cycleCount = 0
var signalStrengthAccumulator = 0
var crtLine = ""

func checkSignalStrength() {
    if [20,60,100,140,180,220,260,300].contains(cycleCount) {
        let signalStrength = cycleCount*registerX
        //print("At CycleCount \(cycleCount), registerX is \(registerX), so signal strength is \(signalStrength)")
        signalStrengthAccumulator += signalStrength
    }
    var pixelPosition = (cycleCount-1) % 40
    //print ("cycleCount: \(cycleCount), pixel position: \(pixelPosition), registerX: \(registerX), sprite: \((registerX-1)...(registerX+1))")
    if ((registerX-1)...(registerX+1)).contains(pixelPosition) {
        crtLine += "#"
    } else {
        crtLine += " "
    }
    if pixelPosition == 39 {
        print(crtLine)
        crtLine = ""
    }
}

var instructions: [String]
try! instructions = getInstructions(from: datafile)
for instruction in instructions {
    //print("Executing instruction: \(instruction)")
    if instruction == "" {
        // this is a blank line, do nothing
        cycleCount += 0
    } else if instruction == "noop" {
        cycleCount += 1
        checkSignalStrength()
    } else {
        // this should be an addx instruction
        var components = instruction.components(separatedBy: " ")
        if components[0] != "addx" {
            print("Found an unexpected instruction: \(instruction)")
        }
        cycleCount += 1
        checkSignalStrength()
        cycleCount += 1
        checkSignalStrength()
        if let addx = Int(components[1]) {
            registerX += addx
        } else {
            print("Could not parse instruction: \(instruction)")
        }
    }
}
print ("Signal Strength Accumulator: \(signalStrengthAccumulator)")
