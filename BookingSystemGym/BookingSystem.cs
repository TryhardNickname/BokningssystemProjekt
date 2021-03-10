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

        public void CreateSchedule()
        {
            List<Activity> Schedule = new List<Activity>();
            //Activity act = new Activity(inputType, inputTime, inputRoom, inputTrainer);
            //Schedule.Add(act);
        }

        public void SaveSchedule()
        {
            List<string> Save = new List<string>();
            foreach (var activity in Schedule)
            {
                Save.Add($"{activity.Type};{activity.ScheduledTime};{activity.Room};"); //{ activity.Trainer}
            }
            //File.WriteAllLines(scheduleFile, Save);
        }

        public void AddToSchedule(Activity a)
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

        public void LogIn()
        {

        }

        public void Register()
        {

        }


    }
}
