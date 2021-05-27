using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Csharp_Controller
{
    class Battery
    {
        //Attributes
        public int ID;
        public string status;
        public Column bestColumn;
        public Elevator bestElevator;
        public int bestScore;
        public int referenceGap;
        public List<Column> columnList;
        //public Elevator checkIfElevatorIsBetter;
        public List<FloorRequestButton> floorRequestButtonsList;
        //A fun way to solve a dumb _isBasement problem. First column is dedicated to basement, the rest are not.
        public bool trueFalseinator(int i)
        {
            if (i == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.ID = _id;
            this.status = "charged";
            columnList = new List<Column>();
            floorRequestButtonsList = new List<FloorRequestButton>();
            //Column constructor
            for (int i = 0; i < _amountOfColumns; i++)
            {
                var column = new Column(i, _amountOfElevatorPerColumn, i, trueFalseinator(i));
                columnList.Add(column);
                //servedFloor adder
                if (i == 0)
                {
                    for (int x = 0; x < 6; x++)
                    {
                        columnList[0].servedFloors.Add(x - 6);
                    }
                    columnList[0].servedFloors.Add(1);
                } //Add basement floors and lobby to servedFloors list
                if (i == 1)
                {
                    for (int x = 0; x < 20; x++)
                    {
                        columnList[1].servedFloors.Add(x + 1);
                    }//Add floors 1-21 to the column.servedFloors list
                }
                if (i == 2)
                {
                    columnList[2].servedFloors.Add(1);
                    for (int x = 20; x < 40; x++)
                    {
                        columnList[2].servedFloors.Add(x + 1);
                    }//Add floors 1 + 21-40 to the column.servedFloors list
                }
                if (i == 3)
                {
                    columnList[3].servedFloors.Add(1);
                    for (int x = 40; x < 60; x++)
                    {
                        columnList[3].servedFloors.Add(x + 1);
                    }//Add floors 1 + 41-60 to the column.servedFloors list
                }
            }
            //Lobby only up and down buttons
            for (int i = 0; i < 1; i++)
            {
                var upFloorRequestButtonCreator = new FloorRequestButton(i, i + 1, "up");
                floorRequestButtonsList.Add(upFloorRequestButtonCreator);
            }
            for (int i = 0; i < 1; i++)
            {
                var downFloorRequestButtonCreator = new FloorRequestButton(i, i + 1, "down");
                floorRequestButtonsList.Add(downFloorRequestButtonCreator);
            }
        }
        public Column findBestColumn(int _requestedFloor)
        {
            //Column bestColumn = null;
            for (int i = 0; i < 4; i++)
            {//For eacg if the 4 columns, look for a column that serves this floor.
                if (this.columnList[i].servedFloors.Contains(_requestedFloor))
                {
                    this.bestColumn = this.columnList[i];
                    //Console.WriteLine(this.bestColumn.ID);
                }
            }
            return this.bestColumn;
        }
        public Elevator assignElevator(int _requestedFloor, string _direction)
        {
            bestElevator = null;
            bestScore = 5;
            referenceGap = 100000;

            if (_requestedFloor > 1)
            {
                foreach (Elevator elevator in this.bestColumn.elevatorList)
                {
                    if (elevator.currentFloor == 1 && elevator.status == "stopped")
                    {
                        bestElevator = elevator;
                        bestScore = 1;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }
                    if (elevator.currentFloor == 1 && elevator.status == "idle")
                    {
                        bestElevator = elevator;
                        bestScore = 2;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);

                    }
                    if (elevator.floorRequestList.Contains(1) && Math.Abs(elevator.currentFloor - 1) < referenceGap && elevator.direction == "down")
                    {
                        bestElevator = elevator;
                        bestScore = 3;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }
                    if (elevator.status == "idle")
                    {
                        bestElevator = elevator;
                        bestScore = 5;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }

                }
                while(bestElevator.currentFloor > 1){
                    bestElevator.currentFloor--;
                    Console.WriteLine("Elevator is on floor " + bestElevator.currentFloor);
                }
                while(bestElevator.currentFloor > _requestedFloor){
                bestElevator.currentFloor--;
                bestElevator.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator.currentFloor);
            }
            while(bestElevator.currentFloor < _requestedFloor){
                bestElevator.currentFloor++;
                bestElevator.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator.currentFloor);
            }
            while(bestElevator.currentFloor == _requestedFloor){
                bestElevator.status = "idle";
                Console.WriteLine("*DING* Elevator has arrived at floor " + _requestedFloor + ".");
                Console.WriteLine("Please exit now.");
                break;
            }
            }
            if (_requestedFloor < 1)
            {
                foreach (Elevator elevator in this.bestColumn.elevatorList)
                {
                    if (elevator.currentFloor == 1 && elevator.status == "stopped")
                    {
                        bestElevator = elevator;
                        bestScore = 1;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }
                    if (elevator.currentFloor == 1 && elevator.status == "idle")
                    {
                        bestElevator = elevator;
                        bestScore = 2;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);

                    }
                    if (elevator.floorRequestList.Contains(1) && Math.Abs(elevator.currentFloor - 1) < referenceGap && elevator.direction == "up")
                    {
                        bestElevator = elevator;
                        bestScore = 3;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }
                    if (elevator.status == "idle")
                    {
                        bestElevator = elevator;
                        bestScore = 4;
                        referenceGap = Math.Abs(elevator.currentFloor - 1);
                    }
                }
            
            bestElevator.floorRequestList.Add(_requestedFloor);
            while(bestElevator.currentFloor > _requestedFloor){
                bestElevator.currentFloor--;
                bestElevator.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator.currentFloor);
            }
            while(bestElevator.currentFloor < _requestedFloor){
                bestElevator.currentFloor++;
                bestElevator.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator.currentFloor);
            }
            while(bestElevator.currentFloor == _requestedFloor){
                bestElevator.status = "idle";
                Console.WriteLine("*DING* Elevator has arrived at " + _requestedFloor + ".");
                break;
            }
            }
            return bestElevator;

        }
    }
    class Column
    {
        //Attributes
        public int ID;
        public string status;
        public Elevator bestElevator1;
        public int bestScore1;
        public int referenceGap1;
        public List<int> servedFloors;
        public bool isBasement;
        public List<Elevator> elevatorList;
        public List<CallButton> callButtonList;
        //Constructor
        public Column(int _id, int _amountOfElevators, int _servedFloors, bool _isBasement)
        {
            this.ID = _id;
            this.status = "built";
            this.servedFloors = new List<int>();
            this.isBasement = _isBasement;
            elevatorList = new List<Elevator>();
            callButtonList = new List<CallButton>();
            //Elevator Creator
            for (int i = 0; i < _amountOfElevators; i++)
            {
                var elevator = new Elevator(i);
                elevatorList.Add(elevator);
            }
            if (_servedFloors == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    var upCallButtonCreator = new CallButton(i, -6 - i, "up");
                    callButtonList.Add(upCallButtonCreator);
                }
            }
            if (_isBasement == false)
            {
                for (int i = 0; i < _servedFloors; i++)
                {
                    var downCallButtonCreator = new CallButton(i, _servedFloors + 1, "down");
                    callButtonList.Add(downCallButtonCreator);
                }
            }
        }
        public Elevator requestElevator(int _requestedFloor, string _direction)
        {
            bestElevator1 = null;
            bestScore1 = 5;
            referenceGap1 = 100000;
            foreach (Elevator elevator in this.elevatorList)
            {
                if (_requestedFloor > -7 && _requestedFloor < 1)
                {
                    //Column A

                    if (elevator.currentFloor == _requestedFloor && elevator.direction == "up")
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 1;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }
                    if (elevator.floorRequestList.Contains(1) && elevator.direction == "up" && elevator.currentFloor < _requestedFloor)
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 2;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }
                    if (elevator.status == "idle")
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 3;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }

                }
                if (_requestedFloor > 1 && _requestedFloor < 61)
                {
                    if (elevator.currentFloor == _requestedFloor && elevator.direction == "down")
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 1;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }
                    if (elevator.floorRequestList.Contains(1) && elevator.direction == "down" && elevator.currentFloor > _requestedFloor && Math.Abs(elevator.currentFloor - _requestedFloor) < referenceGap1)
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 2;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }
                    if (elevator.status == "idle")
                    {
                        bestElevator1 = elevator;
                        bestScore1 = 3;
                        referenceGap1 = Math.Abs(elevator.currentFloor - _requestedFloor);
                    }
            }
            }
            while(bestElevator1.currentFloor > 1){
                while(bestElevator1.currentFloor == _requestedFloor){
                Console.WriteLine("*DING* Elevator doors are open, please enter");
                break;
            }
                bestElevator1.currentFloor--;
                bestElevator1.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator1.currentFloor);
            }
            while(bestElevator1.currentFloor < -1){
                bestElevator1.currentFloor++;
                bestElevator1.status = "moving";
                Console.WriteLine("Elevator is on floor " + bestElevator1.currentFloor);
            }
            while(bestElevator1.currentFloor == -1){
                bestElevator1.currentFloor++;
            }
            while(bestElevator1.currentFloor == 0){
                bestElevator1.currentFloor++;
            }
            while(bestElevator1.currentFloor == 1){
                bestElevator1.status = "moving";
                Console.WriteLine("*DING* Elevator has arrived at Lobby.");
                break;
            }
            //return Elevator object.
            return bestElevator1;
                
                
        }
    }
    class Elevator
    {
        //Attributes
        public int ID;
        public string status;
        public int currentFloor;
        public string direction;
        public object door;
        public List<int> floorRequestList;
        public List<int> completedRequestsList;
        //Constructor
        public Elevator(int _id)
        {
            this.ID = _id;
            this.status = "idle";
            this.currentFloor = 1;
            this.direction = null;
            this.door = new Door(_id);
            floorRequestList = new List<int>();
            completedRequestsList = new List<int>();
        }
    }
    class CallButton
    {
        //Attributes
        public int ID;
        public string status;
        public int floor;
        public string direction;
        //Constructor
        public CallButton(int _id, int _floor, string _direction)
        {
            this.ID = _id;
            this.status = "off";
            this.floor = _floor;
            this.direction = _direction;
        }
    }
    class FloorRequestButton
    {
        //Attributes
        public int ID;
        public string status;
        public int floor;
        public string direction;
        //Constructor
        public FloorRequestButton(int _id, int _floor, string _direction)
        {
            this.ID = _id;
            this.status = "off";
            this.floor = _floor;
            this.direction = _direction;
        }
    }
    class Door
    {
        //Attributes
        public int ID;
        public string status;
        //Constructor
        public Door(int _id)
        {
            this.ID = _id;
            this.status = "closed";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Test Scenario 1
            Battery battery1 = new Battery(1, 4, 60, 6, 5);
            // battery1.findBestColumn(20);
            // battery1.columnList[1].elevatorList[0].currentFloor = 20;
            // battery1.columnList[1].elevatorList[1].currentFloor = 3;
            // battery1.columnList[1].elevatorList[2].currentFloor = 13;
            // battery1.columnList[1].elevatorList[3].currentFloor = 15;
            // battery1.columnList[1].elevatorList[4].currentFloor = 6;
            // battery1.columnList[1].elevatorList[0].floorRequestList.Add(5);
            // battery1.columnList[1].elevatorList[1].floorRequestList.Add(15);
            // battery1.columnList[1].elevatorList[2].floorRequestList.Add(1);
            // battery1.columnList[1].elevatorList[3].floorRequestList.Add(2);
            // battery1.columnList[1].elevatorList[4].floorRequestList.Add(1);
            // battery1.columnList[1].elevatorList[0].status = "moving";
            // battery1.columnList[1].elevatorList[1].status = "moving";
            // battery1.columnList[1].elevatorList[2].status = "moving";
            // battery1.columnList[1].elevatorList[3].status = "moving";
            // battery1.columnList[1].elevatorList[4].status = "moving";
            // battery1.columnList[1].elevatorList[0].direction = "down";
            // battery1.columnList[1].elevatorList[1].direction = "up";
            // battery1.columnList[1].elevatorList[2].direction = "down";
            // battery1.columnList[1].elevatorList[3].direction = "down";
            // battery1.columnList[1].elevatorList[4].direction = "down";
            // battery1.assignElevator(20, "up");
            // Console.WriteLine("Elevator B5 is aka array[4] below");
            // Console.WriteLine(battery1.bestElevator.ID);
            // Console.WriteLine("Elevator B5 currentFloor below");
            // Console.WriteLine(battery1.bestElevator.currentFloor);

            //Test Scenario 2
            // Battery battery2 = new Battery(2, 4, 60, 6, 5);
            // battery2.findBestColumn(36);
            // battery2.columnList[2].elevatorList[0].currentFloor = 1;
            // battery2.columnList[2].elevatorList[1].currentFloor = 23;
            // battery2.columnList[2].elevatorList[2].currentFloor = 33;
            // battery2.columnList[2].elevatorList[3].currentFloor = 40;
            // battery2.columnList[2].elevatorList[4].currentFloor = 39;
            // battery2.columnList[2].elevatorList[0].floorRequestList.Add(21);
            // battery2.columnList[2].elevatorList[1].floorRequestList.Add(28);
            // battery2.columnList[2].elevatorList[2].floorRequestList.Add(1);
            // battery2.columnList[2].elevatorList[3].floorRequestList.Add(24);
            // battery2.columnList[2].elevatorList[4].floorRequestList.Add(39);
            // battery2.columnList[2].elevatorList[0].status = "stopped";
            // battery2.columnList[2].elevatorList[1].status = "moving";
            // battery2.columnList[2].elevatorList[2].status = "moving";
            // battery2.columnList[2].elevatorList[3].status = "moving";
            // battery2.columnList[2].elevatorList[4].status = "moving";
            // battery2.columnList[2].elevatorList[0].direction = null;
            // battery2.columnList[2].elevatorList[1].direction = "up";
            // battery2.columnList[2].elevatorList[2].direction = "down";
            // battery2.columnList[2].elevatorList[3].direction = "down";
            // battery2.columnList[2].elevatorList[4].direction = "down";
            // battery2.assignElevator(36, "up");
            // Console.WriteLine("Elevator C1 is aka array[0] below");
            // Console.WriteLine(battery2.bestElevator.ID);
            // Console.WriteLine("Elevator C1 currentFloor below");
            // Console.WriteLine(battery2.bestElevator.currentFloor);

            //Test Scenario 3
            // Battery battery3 = new Battery(2, 4, 60, 6, 5);
            // battery3.columnList[3].elevatorList[0].currentFloor = 58;
            // battery3.columnList[3].elevatorList[1].currentFloor = 50;
            // battery3.columnList[3].elevatorList[2].currentFloor = 46;
            // battery3.columnList[3].elevatorList[3].currentFloor = 1;
            // battery3.columnList[3].elevatorList[4].currentFloor = 60;
            // battery3.columnList[3].elevatorList[0].status = "moving";
            // battery3.columnList[3].elevatorList[1].status = "moving";
            // battery3.columnList[3].elevatorList[2].status = "moving";
            // battery3.columnList[3].elevatorList[3].status = "moving";
            // battery3.columnList[3].elevatorList[4].status = "moving";
            // battery3.columnList[3].elevatorList[0].floorRequestList.Add(1);
            // battery3.columnList[3].elevatorList[1].floorRequestList.Add(60);
            // battery3.columnList[3].elevatorList[2].floorRequestList.Add(58);
            // battery3.columnList[3].elevatorList[3].floorRequestList.Add(54);
            // battery3.columnList[3].elevatorList[4].floorRequestList.Add(1);
            // battery3.columnList[3].elevatorList[0].direction = "down";
            // battery3.columnList[3].elevatorList[1].direction = "up";
            // battery3.columnList[3].elevatorList[2].direction = "up";
            // battery3.columnList[3].elevatorList[3].direction = "up";
            // battery3.columnList[3].elevatorList[4].direction = "down";
            // battery3.columnList[3].requestElevator(54, "down");
            // Console.WriteLine("Elevator D1 is aka array[0] below");
            // Console.WriteLine(battery3.columnList[3].bestElevator1.ID);
            // Console.WriteLine("Elevator D1 currentFloor below");
            // Console.WriteLine(battery3.columnList[3].bestElevator1.currentFloor);

            //Test Scenario 4
            // Battery battery4 = new Battery(2, 4, 60, 6, 5);
            // battery4.columnList[0].elevatorList[0].currentFloor = -4;
            // battery4.columnList[0].elevatorList[1].currentFloor = 1;
            // battery4.columnList[0].elevatorList[2].currentFloor = -3;
            // battery4.columnList[0].elevatorList[3].currentFloor = -6;
            // battery4.columnList[0].elevatorList[4].currentFloor = -1;
            // battery4.columnList[0].elevatorList[0].status = "idle";
            // battery4.columnList[0].elevatorList[1].status = "idle";
            // battery4.columnList[0].elevatorList[2].status = "moving";
            // battery4.columnList[0].elevatorList[3].status = "moving";
            // battery4.columnList[0].elevatorList[4].status = "moving";
            // battery4.columnList[0].elevatorList[2].floorRequestList.Add(-5);
            // battery4.columnList[0].elevatorList[3].floorRequestList.Add(1);
            // battery4.columnList[0].elevatorList[4].floorRequestList.Add(-6);
            // battery4.columnList[0].elevatorList[0].direction = null;
            // battery4.columnList[0].elevatorList[1].direction = null;
            // battery4.columnList[0].elevatorList[2].direction = "down";
            // battery4.columnList[0].elevatorList[3].direction = "up";
            // battery4.columnList[0].elevatorList[4].direction =  "down";
            // battery4.columnList[0].requestElevator(-3, "up");
            // Console.WriteLine("Elevator A4 is aka array[3] below");
            // Console.WriteLine(battery4.columnList[0].bestElevator1.ID);
            // Console.WriteLine("Elevator A4 currentFloor below");
            // Console.WriteLine(battery4.columnList[0].bestElevator1.currentFloor);
        }
    }
}