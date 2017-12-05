using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Customer.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var program = new Program();
                program.Start();

            }
            catch (Exception e)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(e);
                Console.ForegroundColor = color;
            }
            finally
            {
                Console.WriteLine("Hit enter to exit");
                Console.ReadLine();
            }
        }

        void Start()
        {
            Console.WriteLine("Hello world");
        }
    }
}
