//
//  main.swift
//  AoC 2022 Day06
//
//  Created by Adam Horner on 06/12/2022.
//

import Foundation

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day06-data.txt"

var inputText: String

do {
    inputText = try String(contentsOfFile: datafile)
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

//MARK: - Struct fifo buffer

struct FifoBuffer {
    let maxSize: Int
    private var dataArray: [String] = []
    
    init(maxSize: Int) {
        self.maxSize = maxSize
    }
    
    mutating func add(_ newString: String) {
        dataArray.append(newString)
        if dataArray.count > maxSize {
            dataArray.removeFirst(dataArray.count - maxSize)
        }
    }
    
    func isMarker() -> Bool {
        if dataArray.count < maxSize {
            return false
        }
        return isUnique(data: dataArray)
    }
}

//MARK: - Functions

func isUnique(data array: [String]) -> Bool {
    //recursive function
    if array.count < 2 {
        return true
    }
    var clone = array
    var last = clone.popLast()!
    for i in clone {
        if last == i {
            return false
        }
    }
    return isUnique(data: clone)
}

//MARK: - Main Routine

var StringArray = inputText.split(separator: "")
var startOfPackageMarker = FifoBuffer(maxSize: 4)
var startOfMessageMarker = FifoBuffer(maxSize: 14)
var sopmFound = false
var position = 0

for letter in StringArray {
    position += 1
    startOfPackageMarker.add(String(letter))
    startOfMessageMarker.add(String(letter))
    if startOfPackageMarker.isMarker() && !sopmFound {
        print("Start-of-package Marker found at position: \(position)")
        sopmFound = true
    }
    if startOfMessageMarker.isMarker() {
        print("Start-of-message Marker found at position: \(position)")
        exit(0)
    }
}
