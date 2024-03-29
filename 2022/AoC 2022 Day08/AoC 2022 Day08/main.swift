//
//  main.swift
//  AoC 2022 Day08
//
//  Created by Adam Horner on 08/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day08-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Data Structures

struct TreeMap {
    var treeMap: [[Int]] = []
    var currentRow: Int = -1
    
    mutating func addRow() {
        let newRow: [Int] = []
        treeMap.append(newRow)
        currentRow += 1
    }
    
    mutating func addTree(height: Int) {
        treeMap[currentRow].append(height)
    }
    
    func getHeight(row: Int, column: Int) -> Int {
        return treeMap[row][column]
    }
    
    func columnLength() -> Int {
        return treeMap.count
    }
    
    func rowLength(_ row: Int? = nil) -> Int {
        // use currentRow if row is not specified
        let myRow = row ?? currentRow
        assert(myRow >= 0)
        return treeMap[myRow].count
    }
    
    func getColumn(_ column: Int) -> [Int] {
        var columnHeights: [Int] = []
        for row in 0..<columnLength() {
            columnHeights.append(treeMap[row][column])
        }
        return columnHeights
    }
    
    func isVisible(row: Int, column: Int) -> Bool {
        let rowLength = rowLength(row)
        let columnLength = columnLength()
        assert(row >= 0 || row < rowLength || column >= 0 || column < columnLength, "given row or column are outside bounds of treemap, row: \(row), column: \(column)")
        if row == 0 || column == 0 || row == rowLength-1 || column == columnLength-1 {
            return true
        }
        let myHeight = getHeight(row: row, column: column)
        let maxLeft = treeMap[row][0..<column].reduce(Int.min, max)
        if maxLeft < myHeight {
            return true
        }
        let maxRight = treeMap[row][column+1..<rowLength].reduce(Int.min, max)
        if maxRight < myHeight {
            return true
        }
        let treeColumn = getColumn(column)
        let maxTop = treeColumn[0..<row].reduce(Int.min, max)
        if maxTop < myHeight {
            return true
        }
        let maxBottom = treeColumn[row+1..<columnLength].reduce(Int.min, max)
        if maxBottom < myHeight {
            return true
        }
        //else
        return false
    }
    
    func scenicScore(row: Int, column: Int) -> Int {
        let rowLength = rowLength(row)
        let columnLength = columnLength()
        assert(row >= 0 || row < rowLength || column >= 0 || column < columnLength, "given row or column are outside bounds of treemap, row: \(row), column: \(column)")
        if row == 0 || column == 0 || row == rowLength-1 || column == columnLength-1 {
            return 0
        }
        let myHeight = getHeight(row: row, column: column)
        var northCount = 0
        var southCount = 0
        var eastCount = 0
        var westCount = 0
        for north in stride(from: row-1, through: 0, by: -1) {
            northCount += 1
            if getHeight(row: north, column: column) >= myHeight {
                break
            }
        }
        for west in stride(from: column-1, through: 0, by: -1) {
            westCount += 1
            if getHeight(row: row, column: west) >= myHeight {
                break
            }
        }
        for south in row+1..<rowLength {
            southCount += 1
            if getHeight(row: south, column: column) >= myHeight {
                break
            }
        }
        for east in column+1..<columnLength {
            eastCount += 1
            if getHeight(row: row, column: east) >= myHeight {
                break
            }
        }
        return northCount * southCount * eastCount * westCount
    }
}

//MARK: - Main Loop

var rows = inputText.split(separator: "\n")
var treeMap = TreeMap()

for row in rows {
    treeMap.addRow()
    let treeHeights = row.split(separator: "")
    for treeHeight in treeHeights {
        treeMap.addTree(height: Int(treeHeight)!)
    }
}

// map built, let's check it has the expected number of rows and columns
print("TreeMap built, rows: \(treeMap.columnLength()), columns: \(treeMap.rowLength())")
var visibleTreeCount = 0
for row in 0..<treeMap.rowLength() {
    for column in 0..<treeMap.columnLength() {
        if treeMap.isVisible(row: row, column: column) {
            visibleTreeCount += 1
        }
    }
}

print("Visible tree count in treeMap: \(visibleTreeCount)")

var maxScenicScore = 0
for row in 0..<treeMap.rowLength() {
    for column in 0..<treeMap.columnLength() {
        maxScenicScore = max(maxScenicScore, treeMap.scenicScore(row: row, column: column))
    }
}

print("Maximum scenic score in treeMap: \(maxScenicScore)")
