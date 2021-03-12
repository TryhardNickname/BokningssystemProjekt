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

        public string scheduleFile = (@"../../scheduleFile.txt");

        public BookingSystem()
        {
            //get list of equipments?
            //Equipments = new List<Equipment>();
            //string[] eq = File.ReadAllLines(@"../../equipments.txt");

            //foreach (string s in eq)
            //{
            //    string[] splitLine = s.Split(';');
            //    Equipments.Add(new Equipment(splitLine));
            //}

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

            //get schedule
            foreach (string line in scheduleFromFile)
            {
                string[] activity = line.Split(';');
                Schedule.Add(new Activity(activity));
            }
            //LogIn();
        }

        /// <summary>
        /// Create a new schedule
        /// </summary>
        public void CreateSchedule() //NYI in the program
        {
            Schedule = new List<Activity>();
            Activity act = new Activity(1, new DateTime(2021, 03, 10), 20, "Gym Training", "big room", 1337, "Anna Anderson");
            Schedule.Add(act);
        }

        /// <summary>
        /// Save schedule 
        /// </summary>
        public void SaveSchedule() //NYI in the program 
        {
            List<string> Save = new List<string>();
            foreach (var activity in Schedule)
            {
                Save.Add($"{activity.Type};{activity.ScheduledTime};{activity.Room};{activity.Trainer}"); 
            }
            File.WriteAllLines(scheduleFile, Save);
        }

        /// <summary>
        /// Add an activity to the schedule
        /// </summary>
        /// <param name="a"></param>
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

        /// <summary>
        /// Remove a aktivity from the schedule
        /// </summary>
        /// <param name="a"></param>
        public void RemoveFromSchedule(Activity a) //NYI in the program
        {
            if (CurrentUser.Role == "Admin")
            {
                if (Schedule.Remove(a))
                {
                    //send a message to comferm removed aktivity
                }
            }
        }

        /// <summary>
        /// Fetch schedule and sort it after given type
        /// </summary>
        /// <param name="op"></param>
        /// <returns>List</returns>
        public List<Activity> ShowType(string op)
        {
            List<Activity> sortedList = new List<Activity>();

            //training by your own - gym
            if (op == "1")
            {
                var sort = Schedule.Where(s1 => s1.Type == "Gym Training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //training in a group
            else if (op == "2")
            {
                var sort = Schedule.Where(s1 => s1.Type == "Group Training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //training with PT
            else if (op == "3")
            {
                var sort = Schedule.Where(s1 => s1.Type == "PT Training");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            //consulting with PT
            else if (op == "4")
            {
                var sort = Schedule.Where(s1 => s1.Type == "PT Consulting");
                foreach (var item in sort)
                {
                    sortedList.Add(item);
                }
            }
            return sortedList.ToList();
        }
        /// <summary>
        /// Fetch schedule and sort it after given trainer name
        /// </summary>
        /// <param name="op"></param>
        /// <returns>List</returns>
        public List<Activity> ShowTrainer(string op)
        {
            List<Activity> sortedList = new List<Activity>();
            var sort = Schedule.Where(s => s.Trainer == op);

            foreach (var item in sort)
            {
                sortedList.Add(item);
            }

            return sortedList.ToList();
        }

        /// <summary>
        /// Fetch schedule and sort it after a given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns>List</returns>
        public List<Activity> ShowTime(string time) //MM/DD/YYYY HH:MM
        {
            List<Activity> sortedList = new List<Activity>();
            string hej = Schedule[0].ScheduledTime.ToString("yyyy-MM-dd HH:mm");

            var sort = Schedule.Where(s => s.ScheduledTime.ToString("yyyy-MM-dd HH:mm") == time);
            foreach (var item in sort)
            {
                if (item.ScheduledTime.ToString("yyyy-MM-dd HH:mm") == time)
                {
                    sortedList.Add(item);
                }
            }
            return sortedList.ToList();
        }

        /// <summary>
        /// Fetch all activitys in the schedule
        /// </summary>
        /// <returns>list</returns>
        public List<Activity> ShowAllInSchedule()
        {
            return Schedule.OrderBy(s => s.ScheduledTime.ToString()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ShowBrokenEquip()
        {
            string res = "";
            int index = 1;
            foreach (Equipment e in Equipments)
            {
                if (e.Broken)
                {
                    res += index + ". " + e.Name + "\n";
                    index++;
                }
            }
            return res;
        }

        //variant 1
        public void ChangeEquipmentStatus(User user, Equipment equipment)
        {
            if (user.Role == "Employee" || user.Role == "Admin")
            {
                equipment.Broken = !equipment.Broken;
            }
        }

        //variant 2
        public void ChangeEquipmentStatus(User user)
        {
            if (user.Role == "Employee" || user.Role == "Admin")
            {
                for (int i = 0; i < Equipments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Equipments[i].Name}");
                }

                int userInput = int.Parse(Console.ReadLine()) - 1;
                Equipments[userInput].Broken = !Equipments[userInput].Broken;
            }
        }

        /// <summary>
        /// Check if users role are Admin or Emoloyee. Show all activity and let the user select which activity to change   
        /// </summary>
        /// <param name="user"></param>
        public void ChangeActivity(User user)
        {
            if (user.Role == "Employee" || user.Role == "Admin")
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
                int userInputWhichProp = int.Parse(Console.ReadLine());

                

                // skriver ut värdet på propertyn man ändrade
                // gör instruktioner
                switch (userInputWhichProp)
                {
                    case 1:
                        Console.WriteLine("Ange den nya längden på passet");
                        int newSessionLength = int.Parse(Console.ReadLine());
                        Schedule[index].UpdateActivity(userInputWhichProp, newSessionLength);
                        Console.WriteLine($"New Session Length: {Schedule[index].SessionLength}");
                        break;
                    case 2:
                        Console.WriteLine("Ange den nya tidpunkten för passet");
                        DateTime newTime = DateTime.Parse(Console.ReadLine());
                        Schedule[index].UpdateActivity(userInputWhichProp, newTime);
                        Console.WriteLine($"New Scheduled Time: {Schedule[index].ScheduledTime}");
                        break;
                    case 3:
                        Console.WriteLine("Ange den nya maxgränsen för deltagare");
                        int newMaxParticipants = int.Parse(Console.ReadLine());
                        Schedule[index].UpdateActivity(userInputWhichProp, newMaxParticipants);
                        Console.WriteLine($"New Max Participants: {Schedule[index].MaxParticipants}");
                        break;
                    case 4:
                        Console.WriteLine("Ange det nya antalet deltagare");
                        int newBookedParticipants = int.Parse(Console.ReadLine());
                        Schedule[index].UpdateActivity(userInputWhichProp, newBookedParticipants);
                        Console.WriteLine($"New Booked Participants: {Schedule[index].BookedParticipants}");
                        break;
                    case 5:
                        Console.WriteLine("Ange den nya typen av träning");
                        string newType = Console.ReadLine();
                        Schedule[index].UpdateActivity(userInputWhichProp, newType);
                        Console.WriteLine($"New Type: {Schedule[index].Type}");
                        break;
                    case 6:
                        Console.WriteLine("Ange det nya rummet för passet");
                        string newRoom = Console.ReadLine();
                        Schedule[index].UpdateActivity(userInputWhichProp, newRoom);
                        Console.WriteLine($"New Room: {Schedule[index].Room}");
                        break;
                    case 7:
                        Console.WriteLine("Ange det nya ID:t för passet");
                        int newId = int.Parse(Console.ReadLine());
                        Schedule[index].UpdateActivity(userInputWhichProp, newId);
                        Console.WriteLine($"New Id: {Schedule[index].Id}");
                        break;
                    case 8:
                        Console.WriteLine("Ange den nya tränaren för passet");
                        string newTrainer = Console.ReadLine();
                        Schedule[index].UpdateActivity(userInputWhichProp, newTrainer);
                        Console.WriteLine($"New Trainer: {Schedule[index].Trainer}");
                        break;
                }
                Console.WriteLine("\n");
            }
        }
        /// <summary>
        /// Send a sms to all participant in a canceled activity
        /// </summary>
        /// <returns>string</returns>
        public string SendMassage ()
        {
            //NYI
            return "Ditt pass har blivit inställt";
        }

        /// <summary>
        /// Check if input id exist in user class
        /// </summary>
        /// <param id="inputId"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Rester a nu user
        /// </summary>
        /// <param id="inputId"></param>
        /// <param role="role"></param>
        /// <param name="name"></param>
        /// <returns>bool</returns>
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
