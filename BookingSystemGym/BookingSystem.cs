using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BookingSystemGym
{
    class BookingSystem
    {

        public List<Equipment> Equipments { get; set; }
        public List<Activity> Schedule { get; set; }
        public List<User> UserList { get; set; }
        public User CurrentUser { get; set; }
        string scheduleFile = (@"../../scheduleFile.txt");

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

            Schedule = new List<Activity>();
            string[] scheduleFromFile = File.ReadAllLines(scheduleFile);

            foreach (string line in scheduleFromFile)
            {
                string[] activity = line.Split(';');
                Schedule.Add(new Activity(int.Parse(activity[0]), DateTime.Parse(activity[1]), int.Parse(activity[2]), 
                                                    activity[3], activity[4], int.Parse(activity[5]), activity[6]));
            }

            //get schedule

            //LogIn();
        }

        public void CreateSchedule()
        {
            Schedule = new List<Activity>();
            Activity act = new Activity(1, new DateTime(2021, 03, 10), 20, "gym", "big room", 1337, "Anna Anderson");
            Schedule.Add(act);
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
        
        public List<Activity> ShowType(string op)
        {
            List<Activity> sortedList = new List<Activity>();

            //training by your own - gym
            if (op == "1")
            {
                var sort = Schedule.Where(s1 => s1.Type == "gym training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //training in a group
            else if (op == "2")
            {
                var sort = Schedule.Where(s1 => s1.Type == "group training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //training with PT
            else if (op == "3")
            {
                var sort = Schedule.Where(s1 => s1.Type == "pt training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //consulting with PT
            else if (op == "4")
            {
                var sort = Schedule.Where(s1 => s1.Type == "pt consulting");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            return sortedList.ToList();
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

        //variant 1
        public void ChangeEquipmentStatus(User user, Equipment equipment)
        {
            if (user.Role == "emp" || user.Role == "admin")
            {
                equipment.Broken = !equipment.Broken;
            }
        }

        //variant 2
        public void ChangeEquipmentStatus(User user)
        {
            if (user.Role == "emp" || user.Role == "admin")
            {
                for (int i = 0; i < Equipments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Equipments[i].Name}");
                }

                int userInput = int.Parse(Console.ReadLine()) - 1;
                Equipments[userInput].Broken = !Equipments[userInput].Broken;
            }
        }

        public void ChangeActivity(User user)
        {
            if (user.Role == "emp" || user.Role == "admin")
            {

                // skriv ut aktivitetens id och typ
                for (int i = 0; i < Schedule.Count; i++)
                { 
                    Console.WriteLine($"{Schedule[i].Id} - {Schedule[i].Type}");
                }
                int userInputWhichActivity = int.Parse(Console.ReadLine());

                // om man hittar en ID bland alla IDs, breaka och använd indexet
                int index = 0;
                foreach (Activity a in Schedule)
                {          
                    if (userInputWhichActivity == a.Id)
                    {
                        break;
                    }
                    index++;
                }

                // välj vilken property du vill ändra
                Console.WriteLine($"1. Session Length: {Schedule[index].SessionLength}");
                Console.WriteLine($"2. Scheduled Time: {Schedule[index].ScheduledTime}");
                Console.WriteLine($"3. Max Participants: {Schedule[index].MaxParticipants}");
                Console.WriteLine($"4. Booked Participants: {Schedule[index].BookedParticipants}");
                Console.WriteLine($"5. Type: {Schedule[index].Type}");
                Console.WriteLine($"6. Room: {Schedule[index].Room}");
                Console.WriteLine($"7. Id: {Schedule[index].Id}");
                Console.WriteLine($"8. Trainer: {Schedule[index].Trainer}");
                int userInputWhichProp = int.Parse(Console.ReadLine()) - 1;

                Schedule[index].UpdateActivity(userInputWhichProp);

                // skriver ut värdet på propertyn man ändrade
                // gör instruktioner
                switch (userInputWhichProp)
                {
                    case 1:
                        Console.WriteLine($"New Session Length: {Schedule[index].SessionLength}");
                        break;
                    case 2:
                        Console.WriteLine($"New Scheduled Time: {Schedule[index].ScheduledTime}");
                        break;
                    case 3:
                        Console.WriteLine($"New Max Participants: {Schedule[index].ScheduledTime}");
                        break;
                    case 4:
                        Console.WriteLine($"New Booked Participants: {Schedule[index].ScheduledTime}");
                        break;
                    case 5:
                        Console.WriteLine($"New Type: {Schedule[index].Type}");
                        break;
                    case 6:
                        Console.WriteLine($"New Room: {Schedule[index].Room}");
                        break;
                    case 7:
                        Console.WriteLine($"New Id: {Schedule[index].Id}");
                        break;
                    case 8:
                        Console.WriteLine($"New Trainer: {Schedule[index].Trainer}");
                        break;
                }
            }
            
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
