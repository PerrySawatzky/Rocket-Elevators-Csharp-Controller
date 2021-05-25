using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Csharp_Controller
{
    class Battery {
        //Attributes
        public int ID;
        public string status;
        public List<Column> columnList;
        public List<FloorRequestButton> floorRequestButtonsList;
        //A fun way to solve a dumb _isBasement problem. First column is dedicated to basement, rest are not.
        public bool trueFalseinator(int i)
            {
                if(i == 0){
                    return true;
                }else{
                    return false;
                }
            }
        //Constructor 
        public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            this.ID = _id;
            this.status = "charged";
            columnList = new List<Column>();
            floorRequestButtonsList = new List<FloorRequestButton>();
            //Column creator
            for(int i = 0; i < _amountOfColumns; i++) {
                var columnCreator = new Column(i, _amountOfElevatorPerColumn, i, trueFalseinator(i));
                columnList.Add(columnCreator);
            }
            //Lobby only
            for(int i = 0; i < 1; i++) {
                var upFloorRequestButtonCreator = new FloorRequestButton(i, i+1, "up");
                floorRequestButtonsList.Add(upFloorRequestButtonCreator);
            }
            for(int i = 0; i < 1; i++) {
                var downFloorRequestButtonCreator = new FloorRequestButton(i, i+1, "down");
                floorRequestButtonsList.Add(downFloorRequestButtonCreator);
            }
            
        }
    }
    class Column {
        //Attributes
        public int ID;
        public string status;
        public int servedFloors;
        public bool isBasement;
        public List<Elevator> elevatorList;
        public List<CallButton> callButtonList;
        //Constructor
        public Column(int _id, int _amountOfElevators, int _servedFloors, bool _isBasement)
        {
            this.ID = _id;
            this.status = "built";
            this.servedFloors = _servedFloors;
            this.isBasement = _isBasement;
            elevatorList = new List<Elevator>();
            callButtonList = new List<CallButton>();
            //Elevator Creator
            for(int i = 0; i < _amountOfElevators; i++) {
                var current_elevator = new Elevator(i);
                elevatorList.Add(current_elevator);
            }
            if(_servedFloors == 0) {
                for(int i = 0; i < 6; i++) {
                var upCallButtonCreator = new CallButton(i, -6-i, "up");
                callButtonList.Add(upCallButtonCreator);
            }
            }if(_isBasement == false){
                for(int i = 0; i < _servedFloors; i++){
                    var downCallButtonCreator = new CallButton(i, _servedFloors+1, "down");
                    callButtonList.Add(downCallButtonCreator);
                }
            }
            
        }
    }
    class Elevator {
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
            this.door = new Door(_id); //figure this cookie out later
            floorRequestList =  new List<int>();
            completedRequestsList = new List<int>();
        }
    }
    class CallButton {
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
    class FloorRequestButton {
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
    class Door {
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
            Battery battery1 = new Battery(1, 4, 66, 6, 5);
            battery1.columnList.ToArray();
            Console.WriteLine("should be true below");
            Console.WriteLine(battery1.columnList[0].isBasement);
            Console.WriteLine("should be false below");
            Console.WriteLine(battery1.columnList[1].isBasement);
            Console.WriteLine("should be false below");
            Console.WriteLine(battery1.columnList[2].isBasement);
        }
    }
}
