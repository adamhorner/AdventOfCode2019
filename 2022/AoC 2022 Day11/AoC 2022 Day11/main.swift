//
//  main.swift
//  AoC 2022 Day11
//
//  Created by Adam Horner on 12/12/2022.
//

import Foundation

//MARK: - Data Structures

struct Item: CustomStringConvertible {
    var description: String {
        return String(WorryLevel)
    }
    
    var WorryLevel: Int
}

enum WorryOperation: CustomStringConvertible {
    case Addition(of: Int)
    case Multiplication(by: Int)
    case Squared
    var description: String {
        switch self {
        case .Addition(let val):
            return "Add \(val)"
        case .Multiplication(let val):
            return "Multiply by \(val)"
        case .Squared:
            return "Squared"
        }
    }
}

struct Monkey {
    var items: [Item] = []
    var worryOperation: WorryOperation?
    var throwTestDivisor: Int = 0
    var throwTestTrueMonkey: Int = -1
    var throwTestFalseMonkey: Int = -1
    var itemInspections: Int = 0
    mutating func doCatch(item: Item) {
        items.append(item)
    }
    mutating func throwItem() -> (monkeyNumber: Int, item: Item) {
        var itemWorry = items.removeFirst().WorryLevel
        switch worryOperation {
        case .Addition(let of):
            itemWorry += of
        case .Multiplication(let by):
            itemWorry *= by
        case .Squared:
            itemWorry = itemWorry * itemWorry
        case nil:
            preconditionFailure("No worryOperation, failed to throw item")
        }
        itemWorry /= 3
        itemInspections += 1
        let newItem = Item(WorryLevel: itemWorry)
        if itemWorry % throwTestDivisor == 0 {
            return (throwTestTrueMonkey, newItem)
        } else {
            return (throwTestFalseMonkey, newItem)
        }
    }
}

//MARK: - Functions

func getLines(from file: String) throws -> [String] {
    var fileText: String
    fileText = try String(contentsOfFile: file)
    return fileText.components(separatedBy: "\n")
}

func parseMonkeys(lines: [String]) -> [Monkey] {
    print("starting to parse \(lines.count) lines")
    var monkeys: [Monkey] = []
    var monkeyCounter = -1
    for line in lines {
        if line == "" {
            //printMonkeyGroup(group: monkeys)
        } else {
            var words = line.split(separator: " ")
            switch words[0] {
            case "Monkey":
                monkeyCounter += 1
                assert(monkeyCounter == Int(String(words[1].first!)))
                monkeys.append(Monkey())
            case "Starting":
                for itemWorryLevel in line.components(separatedBy: ": ")[1].components(separatedBy: ", ") {
                    monkeys[monkeyCounter].doCatch(item: Item(WorryLevel: Int(itemWorryLevel)!))
                }
            case "Operation:":
                switch words[4] {
                case "+":
                    monkeys[monkeyCounter].worryOperation = .Addition(of: Int(words[5])!)
                case "*":
                    if words[5] == "old" {
                        monkeys[monkeyCounter].worryOperation = .Squared
                    } else {
                        monkeys[monkeyCounter].worryOperation = .Multiplication(by: Int(words[5])!)
                    }
                default:
                    assertionFailure("Operation Not Supported: \(line)")
                }
            case "Test:":
                monkeys[monkeyCounter].throwTestDivisor = Int(words[3])!
            case "If":
                if words[1] == "true:" {
                    monkeys[monkeyCounter].throwTestTrueMonkey = Int(words[5])!
                } else {
                    monkeys[monkeyCounter].throwTestFalseMonkey = Int(words[5])!
                }
            default:
                assertionFailure("Unsupported parsing line: \(line). Unrecognised token: \(words[0])")
            }
        }
        //print(monkeys[monkeyCounter])
    }
    return monkeys
}

func printMonkeyGroup(group: [Monkey]) {
    var i = 0
    for monkey in group {
        print("Monkey \(i): \(monkey.items)")
        i += 1
    }
    print("")
}

func printMonkeyInspections(group: [Monkey]) {
    var i = 0
    for monkey in group {
        print("Monkey \(i) inspected items \(monkey.itemInspections) times")
    }
}

//MARK: - Main Logic

let datafile = "/Users/adam/Development/Personal/AdventOfCode/2022/day11-data.txt"
let testfile = "/Users/adam/Development/Personal/AdventOfCode/2022/day11-test.txt"
var monkeys: [Monkey]

do {
    monkeys = try parseMonkeys(lines: getLines(from: testfile))
} catch {
    print("Couldn't read text file \(datafile), exiting")
    exit(1)
}

printMonkeyGroup(group: monkeys)
for round in 1...20 {
    for i in 0..<monkeys.count {
        for _ in monkeys[i].items {
            let thrownItem = monkeys[i].throwItem()
            monkeys[thrownItem.monkeyNumber].doCatch(item: thrownItem.item)
        }
    }
    print("After round \(round), the monkeys are holding items with these worry levels:")
    printMonkeyGroup(group: monkeys)
}
printMonkeyInspections(group: monkeys)
let monkeyInspections = monkeys.map{$0.itemInspections}.sorted().reversed()
let monkeyBusinessLevel = (monkeyInspections.first ?? 0) * (monkeyInspections.dropFirst().first ?? 0)
print ("Part 1: MonkeyBusinessLevel = \(monkeyBusinessLevel)")


//let monkeys = parseMonkeys(lines: getLines(from: datafile))
