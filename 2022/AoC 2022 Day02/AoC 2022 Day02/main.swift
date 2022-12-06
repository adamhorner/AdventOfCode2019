//
//  main.swift
//  AoC 2022 Day02
//
//  Created by Adam Horner on 05/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day02-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

let lines = inputText.split(separator: "\n")

var part1TotalScore: Int = 0
var part2TotalScore: Int = 0

for line in lines {
    switch line {
    case "A X":
        part1TotalScore += 3 + 1
        part2TotalScore += 0 + 3
    case "A Y":
        part1TotalScore += 6 + 2
        part2TotalScore += 3 + 1
    case "A Z":
        part1TotalScore += 0 + 3
        part2TotalScore += 6 + 2
    case "B X":
        part1TotalScore += 0 + 1
        part2TotalScore += 0 + 1
    case "B Y":
        part1TotalScore += 3 + 2
        part2TotalScore += 3 + 2
    case "B Z":
        part1TotalScore += 6 + 3
        part2TotalScore += 6 + 3
    case "C X":
        part1TotalScore += 6 + 1
        part2TotalScore += 0 + 2
    case "C Y":
        part1TotalScore += 0 + 2
        part2TotalScore += 3 + 3
    case "C Z":
        part1TotalScore += 3 + 3
        part2TotalScore += 6 + 1
    default:
        print("Found unexpected input line: \(line)")
    }
}

print("Total score for part 1 strategy is: \(part1TotalScore)")
print("Total score for part 2 strategy is: \(part2TotalScore)")
