using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller elevatorController = new Controller();

            var t = Task.Run(() => elevatorController.Run());
            t.Wait();
        }
    }

    public class Controller
    {
        List<Elevator> ListOfCars = new List<Elevator>();
        Random RandomNumberGenerator;
        int NumFloors = 10;
        int NumCars =2;

        public Controller()
        {
            Console.Out.WriteLine($"Creating Controller with no cars");
            RandomNumberGenerator = new Random(DateTime.Now.Millisecond);
        }


        public Controller(int numCars, int numFloors): this()
        {
            NumFloors = numFloors;
            NumCars = numCars;
            Console.Out.WriteLine($"Creating Controller with {NumCars} cars and {NumFloors} floors");

            for(int i = 1; i < numCars+1; i++)
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

        public async Task HandleCall(int floor, long elapsedTime)
        {
            
            await Task.Run(() =>
            {
                foreach(Elevator car in ListOfCars)
                {
                    car.Think(elapsedTime);
                    if(car.WillAnswerCallToFloor(floor))
                    {
                        car.MoveToFloor(floor);
                    }
                    
                }
            });
        }

        public async void Run()
        {
            bool running = true;
            DateTime start = DateTime.Now;
           
            do
            {
                int floor = GenerateCall();
                DateTime end = DateTime.Now;
                await HandleCall(floor , (end-start).Ticks);
                start = end;

            } while (running);

        }

    }
}
