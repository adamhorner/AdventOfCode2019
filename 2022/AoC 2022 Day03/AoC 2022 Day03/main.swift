//
//  main.swift
//  AoC 2022 Day03
//
//  Created by Adam Horner on 05/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day03-data.txt"
let priorities = ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

func findPriorityOfCommonCharacter(left: String, right: String) -> Int? {
    if let common = Set(left).intersection(Set(right)).first {
        if let priority = Array(priorities).firstIndex(where: {$0==common}) {
            return priority + 1
        } else {
            print("Unexpected character encountered, cannot convert to a priority")
            return nil
        }
    } else {
        print("Cannot find a common character in each half of the read string")
        return nil
    }
}

//MARK: - Test area (comment out to run main loop
//print(findPriorityOfCommonCharacter(left: "Abcd", right: "Aefg")!)

//exit(0)
    

//MARK: - Main Loop
let lines = inputText.split(separator: "\n")
var totalPriorities = 0
var totalGroupPriorities = 0

var i: Int = 0
var currentGroup: [String] = []

for line in lines {
    let midPoint = line.index(line.startIndex, offsetBy: line.count/2)
    let firstHalf = line[..<midPoint]
    let secondHalf = line[midPoint...]
    totalPriorities += findPriorityOfCommonCharacter(left: String(firstHalf), right: String(secondHalf)) ?? 0
    if i % 3 < 2 {
        currentGroup.append(String(line))
        i += 1
    } else {
        //find common character of first two in the group
        let common = Set(currentGroup[0]).intersection(Set(currentGroup[1]))
        let commonString = common.map{String($0)}.reduce("", +)
        totalGroupPriorities += findPriorityOfCommonCharacter(left: commonString, right: String(line)) ?? 0
        //reset for next group
        i = 0
        currentGroup = []
    }
}
print("Total of priorities: \(totalPriorities)")
print("Total of group priorities: \(totalGroupPriorities)")
