﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSimulator
{

    enum eCarState
    {
        Waiting,
        Moving,
        OutOfService
    }

    enum eDirection
    {
        Up,
        Down
    }


    public class Elevator
    {
        public int CurrentFloor { get; set; }
        private int RequestedFloor { get; set; }

        private int CarNumber;
        private int NumTrips;
        private eCarState CarState;
        private eDirection Direction;

        private long ServiceTime = 0;
        private long MovingTime = 0;
        private long MaxTimeForService = 100;
        private long CarSpeedTicksPerFloor = 10;

        Queue<int> FloorQueue = new Queue<int>();

        public Elevator()
        {
            Console.Out.WriteLine($"Creating Elevator -> Floor is {CurrentFloor}, CarNumber is {CarNumber}");
            NumTrips = 0;
           
            CarState = eCarState.Waiting;
        }

        public Elevator(int beginFloor, int carNum) : this()
        {
            
            CurrentFloor = beginFloor;
            CarNumber = carNum;
            Console.Out.WriteLine($"Creating Elevator -> Setting Floor to {CurrentFloor} Setting CarNum to {CarNumber}");
        }

        public void AcceptCallToFloor(int toFloor)
        {
            FloorQueue.Enqueue(toFloor);
            Console.Out.WriteLine($"Car {CarNumber} Accepting call from floor {CurrentFloor} to floor {toFloor}");

          
        }

        public void PlaceBackInService()
        {
            ServiceTime = 0;
            NumTrips = 0;
            SetService(true);
        }

        private void SetService(bool inService)
        {
            CarState = inService ? eCarState.Waiting : eCarState.OutOfService;

            Console.Out.WriteLine(inService ? $"Car {CarNumber} coming Into Service at floor {CurrentFloor}": $"Car {CarNumber} going out of service at floor {CurrentFloor} after {NumTrips} runs." );
        }

        private void DoService(long ticks)
        {
            ServiceTime += ticks;
            if(ServiceTime >= MaxTimeForService )
            {

                PlaceBackInService();

            }
        }

        private void Move(long ticks)
        {
            Console.Out.WriteLine($"Car {CarNumber} Moving from floor {CurrentFloor} to {RequestedFloor}");
            MovingTime += ticks;
            if(MovingTime >= CarSpeedTicksPerFloor)
            {
                MovingTime = 0;

                if (CurrentFloor != RequestedFloor)
                {
                    CurrentFloor = Direction == eDirection.Up ? CurrentFloor + 1 : CurrentFloor - 1;
                }
                
                if(CurrentFloor == RequestedFloor || (FloorQueue.Any()&& FloorQueue.Peek() == CurrentFloor))
                {
                    if (FloorQueue.Any() && FloorQueue.Peek() == CurrentFloor)
                    {
                        Console.Out.WriteLine($"Car {CarNumber} Arrived at floor {CurrentFloor}, Waiting ");
                        FloorQueue.Dequeue();
                    }
                    else
                    {
                        Console.Out.WriteLine($"Car {CarNumber} Arrived at floor {RequestedFloor}, Waiting ");
                    }
                    

                    NumTrips++;
                    if (NumTrips >= 100)
                    {
                        SetService(false);
                    }
                    else
                    {
                        GetNextFloorInQueue();
                    }

                }
                else
                {
                    Console.Out.WriteLine($"Car {CarNumber} passing floor {CurrentFloor}  enroute to floor {RequestedFloor} ");
                    CarState = eCarState.Moving;
                }
            }


        }

        void GetNextFloorInQueue()
        {
            if (FloorQueue.Any())
            {
                RequestedFloor = FloorQueue.Dequeue();
                Direction = CurrentFloor < RequestedFloor ? eDirection.Up : eDirection.Down;
                CarState = eCarState.Moving;
            }
            else
            {
                CarState = eCarState.Waiting;
            }
        }

        public bool WillAnswerCallToFloor(int floor)
        {
            switch(CarState)
            {
                case eCarState.OutOfService:
                    {
                        return false;
                    }
                case eCarState.Waiting:
                    {
                        GetNextFloorInQueue();
                        return true;
                    }
                case eCarState.Moving:
                    {                 
                        return WillPassFloor(floor);
                    }

            }
            return true;
        }

        public int NumofFloorsToRequestedFloor(int floor)
        {
            return Math.Abs(CurrentFloor - RequestedFloor);
        }

        bool WillPassFloor(int floor)
        {
            switch(Direction)
            {
                case eDirection.Up:
                  return (floor > CurrentFloor && floor < RequestedFloor);
                    
                case eDirection.Down:
                    return (floor < CurrentFloor && floor > RequestedFloor);

            }

            throw new Exception("Invalid Direction");
           
        }

        public void Think(long tickCounter )
        {
            switch(CarState)
            {
                case eCarState.OutOfService:
                    DoService(tickCounter);
                    break;
                case eCarState.Moving:
                    {
                        if (CurrentFloor == RequestedFloor) CarState = eCarState.Waiting;
                        Move(tickCounter);
                        break;
                    }
                case eCarState.Waiting:
                    {
                        break;
                    }

            }

            
            
        }
    }
}
