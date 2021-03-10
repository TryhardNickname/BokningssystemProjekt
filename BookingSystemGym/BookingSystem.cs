using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BookingSystemGym
{
    class BookingSystem
    {

        public List<Equipment> Equipments { get; set; }
        public List<Activity> Schedule { get; set; }
        public List<User> UserList { get; set; }
        public User CurrentUser { get; set; }

        public BookingSystem()
        {
            //get list of equipments?
            Equipments = new List<Equipment>();
            string[] eq = File.ReadAllLines(@"../../equipments.txt");
            
            foreach (string s in eq)
            {
                Equipments.Add(new Equipment(true, 1, s));
            }


            //get registerd UserList
            UserList = new List<User>();
            string[] ul = File.ReadAllLines(@"../../userlist.txt");

            foreach (string s in eq)
            {
                UserList.Add(new User());
            }


            //get schedule

            LogIn();
        }

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
            string res = "";
            foreach (Equipment e in Equipments)
            {
                if (e.Broken)
                    res += e.Name + "\n";
            }
            return res;
        }

        public void ChangeEquipmentStatus(User user, Equipment equipment)
        {
            if (user.Role == "emp" || user.Role == "admin")
            {
                equipment.Broken = !equipment.Broken;
            }
        }

        public void LogIn()
        {

        }

        public void Register()
        {

        }


    }
}
