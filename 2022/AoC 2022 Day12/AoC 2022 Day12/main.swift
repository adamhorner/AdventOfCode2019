//
//  main.swift
//  AoC 2022 Day12
//
//  Created by Adam Horner on 12/12/2022.
//

import Foundation

//MARK: - Data Structures

class MapLocation: Equatable, Hashable, CustomStringConvertible {
    let distanceFromTop: Int
    let distanceFromLeft: Int
    let height: Character
    var distanceFromStart = Int.max
    var visited = false
    var heightValue: Int {
        let heightMapper: [Character] = ["0", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"]
        if let mapHeight = heightMapper.firstIndex(of: height) {
            assert((1...26).contains(mapHeight), "Invalid numerical map height of \(mapHeight) for \(height)")
            return mapHeight
        } else {
            assertionFailure("Cannot derive numerical height from height of: \(height)")
            return -1
        }
    }
    init(distanceFromTop: Int, distanceFromLeft: Int, height: Character) {
        self.distanceFromTop = distanceFromTop
        self.distanceFromLeft = distanceFromLeft
        self.height = height
    }
    var description: String {
        return "(X: \(distanceFromLeft), Y: \(distanceFromTop), H: \(height))"
    }
    func hash(into hasher: inout Hasher) {
        hasher.combine(distanceFromTop)
        hasher.combine(distanceFromLeft)
    }
    static func ==(lhs: MapLocation, rhs: MapLocation) -> Bool {
        return lhs.distanceFromTop == rhs.distanceFromTop && lhs.distanceFromLeft == rhs.distanceFromLeft
    }
}

struct Map: CustomStringConvertible {
    var mapLocations: [[MapLocation]]
    var start: MapLocation
    var end: MapLocation
    // always assume a rectangular map, i.e. all rows are the same length
    var mapHeight: Int {
        return mapLocations.count
    }
    var mapWidth: Int {
        return mapLocations[0].count
    }
    func getNSEWMoves(from: MapLocation) -> [MapLocation] {
        var nextMoves: [MapLocation] = []
        if from.distanceFromTop > 0 {
            nextMoves.append(mapLocations[from.distanceFromTop-1][from.distanceFromLeft])
        }
        if from.distanceFromTop < mapHeight-1 {
            nextMoves.append(mapLocations[from.distanceFromTop+1][from.distanceFromLeft])
        }
        if from.distanceFromLeft > 0 {
            nextMoves.append(mapLocations[from.distanceFromTop][from.distanceFromLeft-1])
        }
        if from.distanceFromLeft < mapWidth-1 {
            nextMoves.append(mapLocations[from.distanceFromTop][from.distanceFromLeft+1])
        }
        return nextMoves.filter{$0.heightValue<=(from.heightValue+1) && !$0.visited}
    }
    var description: String {
        var mapRowStrings = mapLocations.map{
            ($0.map({$0.height}).reduce("") {"\($0)\($1)"})
        }
        return mapRowStrings.map{"\($0)\n"}.reduce("") {"\($0)\($1)"}
    }
}

//MARK: - Functions

func parseMap(from filePath: String) throws -> Map {
    let rows = try String(contentsOfFile: filePath).components(separatedBy: "\n")
    var mapLocations: [[MapLocation]] = []
    var start: MapLocation?
    var end: MapLocation?
    var rowNumber = -1
    for row in rows {
        if row != "" {
            rowNumber += 1
            var columnNumber = -1
            var mapRow: [MapLocation] = []
            for position in row.split(separator: "") {
                columnNumber += 1
                var height = position.first!
                if position == "S" {
                    height = "a"
                    start = MapLocation(distanceFromTop: rowNumber, distanceFromLeft: columnNumber, height: height)
                    mapRow.append(start!)
                } else if position == "E" {
                    height = "z"
                    end = MapLocation(distanceFromTop: rowNumber, distanceFromLeft: columnNumber, height: height)
                    mapRow.append(end!)
                } else {
                    mapRow.append(MapLocation(distanceFromTop: rowNumber, distanceFromLeft: columnNumber, height: height))
                }
            }
            mapLocations.append(mapRow)
        }
    }
    return Map(mapLocations: mapLocations, start: start!, end: end!)
}

func findShortestDistance(from startLocation: MapLocation, to endLocation: MapLocation) -> Int {
    // reset map to run another time
    for y in 0..<worldMap.mapHeight {
        for x in 0..<worldMap.mapWidth {
            worldMap.mapLocations[y][x].distanceFromStart = Int.max
            worldMap.mapLocations[y][x].visited = false
        }
    }

    //startLocation.visited = true
    startLocation.distanceFromStart = 0
    var visitableLocations: Set<MapLocation> = [startLocation]
    
    while !(endLocation.visited || visitableLocations.isEmpty) {
        for location in visitableLocations {
            if location.visited {
                visitableLocations.remove(location)
            } else {
                let distance = location.distanceFromStart+1
                for nextLocation in worldMap.getNSEWMoves(from: location) {
                    if nextLocation.distanceFromStart > distance {
                        nextLocation.distanceFromStart = distance
                        visitableLocations.insert(nextLocation)
                    }
                }
                location.visited = true
            }
        }
    }
    return worldMap.end.distanceFromStart
}

//MARK: - Main Loop

let mapFile = "/Users/adam/Development/Personal/AdventOfCode/2022/day12-data.txt"

let worldMap = try parseMap(from: mapFile)
print(worldMap)

let distanceFromStart = findShortestDistance(from: worldMap.start, to: worldMap.end)
print("Part 1 distance: \(distanceFromStart)")

// part 2
var shortestDistanceToEnd = distanceFromStart
var lowestMapLocations: [MapLocation] = []

// find all the lowest points
for y in 0..<worldMap.mapHeight {
    for x in 0..<worldMap.mapWidth {
        let location = worldMap.mapLocations[y][x]
        if location.height == "a" {
            lowestMapLocations.append(location)
        }
    }
}
        
var checkNumber = 0
for possibleStartPoint in lowestMapLocations {
    checkNumber += 1
    //print("checking for distance less than \(shortestDistanceToEnd). Test \(checkNumber)/\(lowestMapLocations.count)")
    var thisDistance = findShortestDistance(from: possibleStartPoint, to: worldMap.end)
    if thisDistance < shortestDistanceToEnd {
        shortestDistanceToEnd = thisDistance
    }
}
print("Part 2 distance: \(shortestDistanceToEnd)")
