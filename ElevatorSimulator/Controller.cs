using System;
using System.Collections.Generic;
using System.Threading;

namespace ElevatorSimulator
{
    public class Controller
    {
        List<Elevator> ListOfCars = new List<Elevator>();
        Random RandomNumberGenerator;
        int NumFloors = 10;
        int NumCars =2;
        long tickCounter = 0;

        public Controller()
        {
            Console.Out.WriteLine($"Creating Controller with {NumCars} cars and {NumFloors} floors");
            RandomNumberGenerator = new Random(DateTime.Now.Millisecond);
            
        }

        public Controller(int numCars, int numFloors): this()
        {
            NumFloors = numFloors;
            NumCars = numCars;
            Console.Out.WriteLine($"Creating Controller with {NumCars} cars and {NumFloors} floors");
            


        }


        public void InitCars()
        {
            for (int i = 1; i < NumCars + 1; i++)
            {
                ListOfCars.Add(new Elevator(i, i));
            }
        }

        public int GenerateCall()
        {
            int floor = RandomNumberGenerator.Next(1, NumFloors);
            Console.Out.WriteLine($"Received call to floor {floor}");
            return floor;
        }

        public bool HandleCall(int floor, long tickCounter)
        {

            int maxFloorsTotravel = NumFloors;
            Elevator closestCar = null;
            foreach (Elevator car in ListOfCars)
            {

                car.Think(tickCounter);
                if (car.WillAnswerCallToFloor(floor))
                {
                    if (closestCar == null)
                    {
                        closestCar = car;
                        maxFloorsTotravel = car.NumofFloorsToRequestedFloor(floor);
                    }
                    else
                    {
                        if (car.NumofFloorsToRequestedFloor(floor) < maxFloorsTotravel)
                        {
                            closestCar = car;
                        }
                    }


                }

                if (closestCar != null)
                {
                    closestCar.AcceptCallToFloor(floor);
                }
            }

            return closestCar != null;
        }

        public void Run()
        {
            bool running = true;
            InitCars();
            DateTime start = DateTime.Now;
            try
            {
                bool callHandled = true;
                int floor = GenerateCall();
                do
                {
                    if(callHandled)
                    {
                        floor = GenerateCall();
                    }

                    callHandled = HandleCall(floor, tickCounter++);
                   
                   
                    Thread.Sleep(1000);

                } while (running);
            }catch(Exception ex)
            {

            }
           

        }

    }
}
