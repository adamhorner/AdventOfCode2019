//
//  main.swift
//  AoC 2022 Day04
//
//  Created by Adam Horner on 06/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day04-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Functions

func convertRangeToSet(_ input: String) -> Set<Int> {
    var result = Set<Int>()
    let rangeValues = input.split(separator: "-")
    assert(rangeValues.count == 2)
    if let lower = Int(rangeValues[0]) {
        if let upper = Int(rangeValues[1]) {
            for x in lower...upper {
                result.insert(x)
            }
        } else {
            print("upper could not be resolved, nil value")
        }
    } else {
        print("lower could not be resolved, nil value")
    }
    return result
}

//MARK: - Main Loop
let lines = inputText.split(separator: "\n")

var subsetCounter = 0
var overlapCounter = 0

for line in lines {
    let ranges = line.split(separator: ",")
    assert(ranges.count == 2)
    let elfSet1 = convertRangeToSet(String(ranges[0]))
    let elfSet2 = convertRangeToSet(String(ranges[1]))
    if elfSet2.isSubset(of: elfSet1) || elfSet1.isSubset(of: elfSet2) {
        subsetCounter += 1
    }
    if !elfSet1.isDisjoint(with: elfSet2) {
        overlapCounter += 1
    }
}
print("Total subset count: \(subsetCounter)")
print("Total overlap count: \(overlapCounter)")
