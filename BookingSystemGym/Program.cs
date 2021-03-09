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
            string userSel = "";
            if ( userSel == "2")
            {
                string schedule = bs.ShowSchedule();
                Console.WriteLine(schedule);
            }


        }
    }
}
