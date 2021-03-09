using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class BookingSystem
    {

        public List<Equipment> Equipments { get; set; }
        public List<Activity> Schedule { get; set; }
        public List<User> UserList { get; set; }
        public User CurrentUser { get; set; }


        public void AddToSchedule()
        {
            if (CurrentUser.Role != "Admin")
            {
                return;
            }

        }

        public void RemoveFromSchedule(Activity a)
        {
            foreach (Activity b in Schedule)
            {
                if (a.Id == b.Id)
                {
                    Schedule.Remove(b);
                }
            }
        }
        public string ShowSchedule()
        {
            return "";
        }

        public string ShowBrokenEquip()
        {
            return "";
        }

        public void LogIn()
        {

        }

        public void Register()
        {

        }


    }
}
