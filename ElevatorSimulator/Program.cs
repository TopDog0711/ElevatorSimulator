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
            Controller elevatorController = new Controller(10);

        }
    }

    public class Controller
    {
        List<Elevator> ListOfCars = new List<Elevator>();

        public Controller()
        {
            Console.Out.WriteLine($"Creating Controller with no cars");
        }

        public Controller(int numCars): this()
        {
            Console.Out.WriteLine($"Creating Controller with {numCars} cars");

            for(int i = 1; i < numCars+1; i++)
            {
                ListOfCars.Add(new Elevator(i, i));
            }
        }



    }
}
