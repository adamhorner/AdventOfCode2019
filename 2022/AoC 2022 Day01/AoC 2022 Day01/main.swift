//
//  main.swift
//  AoC 2022 Day01
//
//  Created by Adam Horner on 02/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day01-data.txt"

do {
    let inputText = try! String(String(contentsOfFile: datafile))
    let elvesCaloryLines = inputText.split(separator: "\n\n")
    var elvesCalories: [Int] = []
    for elfCaloryLines in elvesCaloryLines {
        elvesCalories.append(0)
        let caloryLines = elfCaloryLines.split(separator: "\n")
        for caloryString in caloryLines {
            if let calories = Int(caloryString) {
                elvesCalories[elvesCalories.endIndex-1] += calories
            }
        }
    }
    elvesCalories.sort()
    elvesCalories.reverse()
    print("maximum calories: \(elvesCalories[0])")
    let top3CaloryCarriersTotal = elvesCalories[0] + elvesCalories[1] + elvesCalories[2]
    print("Top 3 carriers total: \(top3CaloryCarriersTotal)")
}
