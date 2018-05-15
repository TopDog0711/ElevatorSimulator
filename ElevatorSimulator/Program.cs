using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller elevatorController = new Controller();

            elevatorController.Run();
          
        }
    }
}
