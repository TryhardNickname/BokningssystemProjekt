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

            foreach (string line in ul)
            {
                string[] usr = line.Split(',');
                UserList.Add(new User(usr[0], usr[1], usr[2]));
            }


            //get schedule

            LogIn();
        }

        public void AddToSchedule(Activity a)
        {
            if (CurrentUser.Role == "Admin")
            {
                Schedule.Add(a);
            }
            else
            {
                //"you dont have premission"
            }


        }

        public void RemoveFromSchedule(Activity a)
        {
            if (CurrentUser.Role == "Admin")
            {
                if (Schedule.Remove(a))
                {
                    //skicka meddelande "a är borttaget"
                }
                //foreach (Activity b in Schedule)
                //{
                //    if (a.Id == b.Id)
                //    {
                //        Schedule.Remove(b);
                //    }
                //}
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

        //returns true if succesful
        public bool LogIn(string inputId)
        {
            foreach (User u in UserList)
            {
                if (u.Id == inputId)
                {
                    //User exists
                    CurrentUser = u;
                    return true;
                }
            }
            //if no user is found
            //"please register to log in"
            //Register(inputId);
            return false;
        }

        //returns true if succesful
        public bool Register(string inputId, string role, string name)
        {
            foreach (User u in UserList)
            {
                if (u.Id == inputId)
                {
                    //User already exists
                    return false;
                }
            }

            //new user - sets as currentUser
            CurrentUser = new User(inputId, role, name);
            UserList.Add(CurrentUser);
            return true;
        }


    }
}
