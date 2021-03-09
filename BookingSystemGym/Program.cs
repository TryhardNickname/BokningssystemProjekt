using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class Program
    {
        static void Main(string[] args)
        {

            BookingSystem bs = new BookingSystem();

            string userInput = "";
            
            while (userInput != "0")
            {
                Console.WriteLine("input choice: ");
                userInput = Console.ReadLine();

                if (userInput == "2")
                {
                    string schedule = bs.ShowSchedule();
                    Console.WriteLine(schedule);
                }
                if (userInput == "3")
                {
                    string bi = bs.ShowBrokenEquip();
                    Console.WriteLine(bi);
                }

                
            }



            Console.ReadKey();

        }
    }
}
