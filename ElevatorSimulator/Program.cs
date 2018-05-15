using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    public class Elevator
    {
        public int Floor { get; set; }
        private bool InService;
        private int CarNumber;
        private int NumTrips;
        
        public Elevator()
        {
            Console.Out.WriteLine($"Creating Elevator -> Floor is {Floor}, CarNumber is {CarNumber}");
            NumTrips = 0;
            InService = true;
        }

        public Elevator(int beginFloor, int carNum) : this()
        {
            
            Floor = beginFloor;
            CarNumber = carNum;
            Console.Out.WriteLine($"Creating Elevator -> Setting Floor to {Floor} Setting CarNum to {CarNumber}");
        }

        public void MoveToFloor(int toFloor)
        {
            Console.Out.WriteLine($"Moving from floor {Floor} to floor {toFloor}");
            NumTrips++;
            if(NumTrips >= 100)
            {
                SetService(false);
            }
        }

        public void Service()
        {
            NumTrips = 0;
            SetService(true);
        }

        private void SetService(bool inService)
        {
            InService = inService;
            Console.Out.WriteLine(InService? $"Coming Into Service at floor {Floor}": $"Car {CarNumber} going out of service at floor {Floor} after {NumTrips} runs." );
        }

        private bool WillAnswerCallToFloor(int floor)
        {
            return false;
        }

        public void Think(float deltaTime )
        {

        }
    }
}
