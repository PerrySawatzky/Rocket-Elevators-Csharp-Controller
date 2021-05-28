# Rocket-Elevators-Csharp-Controller
> Week 2 project for Rocket Elevators. Subsidiary of Codeboxx Technologies
## Table of Contents
* [General](#general)
* [Technologies](#technologies)
* [Setup](#setup)
* [Code Examples](#Code-Examples)

## General
This project features a simulated elevator sender and retriever algorithm as well as a column selector for a commercial setting. The project operates in the command terminal.
It has three methods, two in the Battery class; assignElevator & findBestColumn, and one in the Column class; requestElevator.

## Technologies
Project was created in two languages:
* C# .NET Core 3.0
* Golang version 1.16.4

## Setup
To run this project, clone it through your Command Line Interface locally by entering:
```
gh repo clone PerrySawatzky/Rocket-Elevators-Csharp-Controller

Golang is required, [download here](https://https://dotnet.microsoft.com/download)

## Code Examples
This repo includes 4 test scenarios at the bottom of the file, starting at line 410.

However only five lines are absolutely critical. The first creates the Battery, the two subsequent create the Battery methods:
```
Battery battery1 = new Battery(1, 4, 60, 6, 5);
battery1.findBestColumn(20);
battery1.assignElevator(20, "up");
Console.WriteLine(battery1.bestElevator.ID);
Console.WriteLine(battery1.bestColumn.ID);
```
The five parameters for Battery are id, amount of columns, amount of floors, amount of basements and amount of elevators per column, respectfully.

We assign a variable, in this case 'battery1' to the findBestColumn method in order for the same column to be assigned when completing the subsequent assignElevator method. 

The only parameter in the findBestColumn method is the floor number.

The two parameter in the assignElevator method are the floor the user requested and the direction.

In order to set up a complex scenario, you can set certain properties of the elevators before hand. This is what that would look like:
```
battery1.columnsList[1].elevatorsList[0].currentFloor = 20;
battery1.columnsList[1].elevatorsList[1].currentFloor = 3;
battery1.columnsList[1].elevatorsList[2].currentFloor = 13;
battery1.columnsList[1].elevatorsList[3].currentFloor = 15;
battery1.columnsList[1].elevatorsList[4].currentFloor = 6;
battery1.columnsList[1].elevatorsList[0].floorRequestsList.Add(5);
battery1.columnsList[1].elevatorsList[1].floorRequestsList.Add(15);
battery1.columnsList[1].elevatorsList[2].floorRequestsList.Add(1);
battery1.columnsList[1].elevatorsList[3].floorRequestsList.Add(2);
battery1.columnsList[1].elevatorsList[4].floorRequestsList.Add(1);
battery1.columnsList[1].elevatorsList[0].status = "moving";
battery1.columnsList[1].elevatorsList[1].status = "moving";
battery1.columnsList[1].elevatorsList[2].status = "moving";
battery1.columnsList[1].elevatorsList[3].status = "moving";
battery1.columnsList[1].elevatorsList[4].status = "moving";
battery1.columnsList[1].elevatorsList[0].direction = "down";
battery1.columnsList[1].elevatorsList[1].direction = "up";
battery1.columnsList[1].elevatorsList[2].direction = "down";
battery1.columnsList[1].elevatorsList[3].direction = "down";
battery1.columnsList[1].elevatorsList[4].direction = "down";
battery1.assignElevator(20, "up");
Console.WriteLine("Elevator B5 is aka array[4] below");
Console.WriteLine(battery1.bestElevator.ID);
Console.WriteLine("Best Column ID Array # below");
Console.WriteLine(battery1.bestColumn.ID);
```
To run the algorithms in the command line terminal, simply type:
```
dotnet run
```
and hit enter.