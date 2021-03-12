﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BookingSystemGym
{
    class Program
    {
        static void Main(string[] args)
        {

            BookingSystem bs = new BookingSystem();
            string userInput = "";

            //menu loop
            while (true)
            {
                PrintLogInMenu();
                userInput = GetMenuInput(3);

                //exit
                if (userInput == "0")
                    break;

                //logga in 
                if (userInput == "1")
                {

                    Console.WriteLine("Ange ditt medlems-id[nnnn]: ");
                    string id = GetMenuInput(9999);


                    if (bs.LogIn(id))
                    {
                        //next menu: bokningen 
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Medlems-Id saknas, vill du registrera dig?");
                        string yesorno = Console.ReadLine(); // lägg till felhantering
                        if (yesorno == "yes")
                        {
                            RegisterNewUser(bs);
                            break;
                        }
                    }

                }
                else if (userInput == "2")
                {
                    RegisterNewUser(bs);
                    break;

                }



            }
            Console.Clear();


            //booking-menu
            while (true)
            {
                int choices = PrintMenu(bs);

                Console.WriteLine("Ange val: ");
                userInput = GetMenuInput(choices);

                //exit
                if (userInput == "0")
                    break;

                //1. Se Bokningsschemat
                if (userInput == "1")
                {

                    Console.WriteLine("==========================================");
                    Console.WriteLine("= 1. Sök efter pass                      =");
                    Console.WriteLine("= 2. Sök efter tid/dag                   =");
                    Console.WriteLine("= 3. Sök efter tränare                   =");
                    Console.WriteLine("= 4. Visa alla pass                      =");
                    Console.WriteLine("= 0. Backa                               =");
                    Console.WriteLine("==========================================");
                    Console.Write("Ange val: ");
                    string input = GetMenuInput(5);
                    if (input == "1") //sök efter pass
                    {
                        Console.WriteLine("==========================================");
                        Console.WriteLine("= 1. Visa vanlig gymträning              =");
                        Console.WriteLine("= 2. Visa grupptränings-pass             =");
                        Console.WriteLine("= 3. Visa träning med PT                 =");
                        Console.WriteLine("= 4. visa konsultation med PT            =");
                        Console.WriteLine("= 0. Backa                               =");
                        Console.WriteLine("==========================================");
                        Console.Write("Ange val: ");
                        input = GetMenuInput(5);


                        // returnerar type av träning beroende på input(1-4)
                        List<Activity> TypeList = bs.ShowType(input);
                        PrintActivities(input, TypeList);


                        Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
                        input = GetMenuInput(TypeList.Count+1);
                        if (input == "0")
                        {
                            continue;
                        }
                        else // lägg till tryparse
                        {
                            bs.CurrentUser.MakeReservation(TypeList[int.Parse(input) - 1]);
                            Console.WriteLine("Din bokning har genomförts! Välkommen! ");
                        }

                    }
                    else if (input == "2") //Sök tid
                    {
                        Console.WriteLine("Ange datum och tid som du söker pass efter (YYYY-MM-DD HH:MM:SS)"); // ändra till dag
                        string dateInput = Console.ReadLine(); // felhantering

                        List<Activity> TimeList = bs.ShowTime(dateInput);
                        PrintActivities(dateInput, TimeList);

                        //foreach (var item in ST)
                        //{
                        //    Console.WriteLine($"{activityCount}. Passets längd: {item.SessionLength} timmar\nStarttid: " +
                        //                      $"{item.ScheduledTime}\nMax antal deltagare: {item.MaxParticipants}\n" +
                        //                      $"Platser kvar: {item.MaxParticipants - item.BookedParticipants}\n" +
                        //                      $"Typ av träning: {item.Type}\nPlats: {item.Room}\nTränare: {item.Trainer}");
                        //    Console.WriteLine("------------------");
                        //    Console.WriteLine();
                        //    activityCount++;
                        //}

                        Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
                        input = Console.ReadLine();
                        if (input == "0")
                        {

                        }
                        else
                        {
                            bs.CurrentUser.MakeReservation(TimeList[int.Parse(input) - 1]);
                            Console.WriteLine("Din bokning har genomförts! Välkommen! ");
                        }


                    }
                    else if (input == "3") //Sök PT
                    {
                        Console.WriteLine("Ange namnet på tränaren");
                        input = Console.ReadLine(); // felhatering

                        List<Activity> TrainerList = bs.ShowTrainer(input);
                        PrintActivities(input, TrainerList);

                        //int activityCount = 1;
                        //foreach (var item in ST)
                        //{
                        //    Console.WriteLine($"{activityCount}. Passets längd: {item.SessionLength} timmar\nStarttid: " +
                        //                      $"{item.ScheduledTime}\nMax antal deltagare: {item.MaxParticipants}\n" +
                        //                      $"Platser kvar: {item.MaxParticipants - item.BookedParticipants}\n" +
                        //                      $"Typ av träning: {item.Type}\nPlats: {item.Room}\nTränare: {item.Trainer}");
                        //    Console.WriteLine("------------------");
                        //    Console.WriteLine();
                        //    activityCount++;
                        //}

                        Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
                        input = Console.ReadLine();
                        if (input == "0")
                        {

                        }
                        else
                        {
                            bs.CurrentUser.MakeReservation(TrainerList[int.Parse(input) - 1]);
                        }
                    }
                    else if (input == "4")
                    {
                        PrintActivities("allt", bs.Schedule.ToList());
                    }


                }
                //2. Se lista på maskiner
                if (userInput == "2")
                {
                    string bi = bs.ShowEquip();
                    Console.WriteLine(bi);
                }
                //3. Boka Pass,PT...
                if (userInput == "3")
                {
                    Console.WriteLine("TBI");
                    // vilket sorts pass vill du boka?
                    //<<PTpass
                    //lista över pt pass, välj ett
                    //bs.CurrentUser.MakeReservation()
                }
                //4. Ange trasig maskin
                if (userInput == "4")
                {
                    Console.WriteLine(bs.ShowEquip());
                    Console.WriteLine();
                    Console.WriteLine("Ange vilken maskin du vill ändra: ");
                    userInput = Console.ReadLine();

                    bs.ChangeEquipmentStatus(bs.CurrentUser, bs.Equipments[int.Parse(userInput) - 1]);
                }
                //5. Gör ändring i bokningsschemat
                if (userInput == "5")
                {
                    //välj activitet och sen:
                    bs.ChangeActivity(bs.CurrentUser);
                }
                //6. Ladda upp nytt bokningsschema
                if (userInput == "6")
                {
                    Console.WriteLine("Vänligen ange filnamn för ny schema-fil: ");
                    string fileName = Console.ReadLine();
                    if (File.Exists(fileName))
                    {
                        bs.scheduleFile = fileName;
                        //bs.loadSchedule(); kopiera från konstruktor
                    }
                }

            }


            Console.WriteLine("Hej då!");
            Console.ReadKey();

        }

        /// <summary>
        /// First menu
        /// </summary>
        static void PrintLogInMenu()
        {
            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("= Hej Och välkomna till BokningsSystemet =");
            Console.WriteLine("= 1. Logga in                            =");
            Console.WriteLine("= 2. Registrera ny användare=            =");
            Console.WriteLine("= 0. Exit                                =");
            Console.WriteLine("==========================================");
            Console.Write("Ange val: ");
        }

        /// <summary>
        /// This method is used in various places to check for proper inputs 
        /// And the loop keeps going until a valid answer is given.
        /// </summary>
        /// <param name="amountOfChoices">This is what the user inputs</param>
        /// <returns></returns>
        static string GetMenuInput(int amountOfChoices)
        {
            string userInput = Console.ReadLine();

            while (true)
            {
                if (int.Parse(userInput) < amountOfChoices && int.Parse(userInput) >= 0) // && > 0
                {
                    if (amountOfChoices != 9999)
                    {
                        return userInput;
                    }
                    else
                    {
                        if (userInput.Length == 4)
                        {
                            return userInput;
                        }
                    }
                }
                Console.WriteLine("Fel input, försök igen: ");
                userInput = Console.ReadLine();
            }
        }

        /// <summary>
        /// This method is used once depending on how the user chooses to interact with the program
        /// Either if user inputs unknown ID or if the user chooses to register
        /// </summary>
        /// <param name="bs"></param>
        static void RegisterNewUser(BookingSystem bs)
        {

            Console.Write("Skriv ditt namn: ");
            string name = Console.ReadLine();

            // Här kan vi göra på ett bättre sätt


            Console.WriteLine("välj ditt medlems-id[nnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
            string newId = Console.ReadLine();

            //new user is added to UserList, and is set as CurrentUser
            bs.Register(newId, "GymUser", name);
            //gå till nästa meny
        }

        /// <summary>
        /// This method prints out the menu that comes after the login screen and is the base menu.
        /// Depending on your clearance level, you will see different things so that regular gym users
        /// cannot for example change the status of a machine.
        /// </summary>
        /// <param name="bs"></param>
        /// <returns>This method returns the amount of choices a user has, to prevent users from inputing a number that it doesnt have access to
        /// </returns>
        static int PrintMenu(BookingSystem bs)
        {

            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine($"= Välkommen till bokningen  {bs.CurrentUser.Name}!      =");
            Console.WriteLine("= 1. Se Bokningsschemat och boka pass    =");
            Console.WriteLine("= 2. Se maskiners status                 =");
            Console.WriteLine("= 3. Boka Pass,PT...                     =");

            if (bs.CurrentUser.Role != "user")
            {
                Console.WriteLine("= 4. Ange trasig maskin ");
                Console.WriteLine("= 5. Gör ändring i bokningsschemat ");

                if (bs.CurrentUser.Role == "admin")
                {
                    Console.WriteLine("= 6. Ladda upp bokningsschema ");
                    Console.WriteLine("= 0. Logga ut ");
                    Console.WriteLine("======================================================");
                    return 6;
                }
                Console.WriteLine("= 0. Logga ut ");
                Console.WriteLine("======================================================");
                return 5;
            }
            Console.WriteLine("= 0. Logga ut                                       =");
            Console.WriteLine("=====================================================");
            return 4;
        }

        /// <summary>
        /// The purpose of this method is to be able to create an activity apart from loading an activity list from a file
        /// </summary>
        /// <param name="bs"></param>
        static void CreateActivity(BookingSystem bs)
        {
            int sessionLength, maxParticipants, iD;
            string type, room, trainer;
            DateTime scheduledTime;

            Console.WriteLine("Skriv in längden på passet i timmar: ");
            sessionLength = int.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in datumet i följande format: YY/MM/DD hh:mm");
            scheduledTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in max antal deltagare: ");
            maxParticipants = int.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in typen av träning: ");
            type = Console.ReadLine();
            Console.WriteLine("Skriv in vilket rum aktiviteten ska äga rum i: ");
            room = Console.ReadLine();
            Console.WriteLine("Skriv in IDet på aktiviteten: ");
            iD = int.Parse(Console.ReadLine());
            Console.WriteLine("Skriv in vem tränaren är: ");
            trainer = Console.ReadLine();

            Activity newActivity = new Activity(sessionLength, scheduledTime, maxParticipants, type, room, iD, trainer);
            bs.AddToSchedule(newActivity);
        }

        /// <summary>
        /// This method, together with another one, print out a list of activities that the user filter for
        /// So for example, if input is 1, it filters for gym training and prints the list with only gym trainings.
        /// </summary>
        /// <param name="input">what the user inputed at line 93, represents a choice of which activities to filter for</param>
        /// <param name="list">The filtered list of activities</param>
        static void PrintActivities(string input, List<Activity> list)
        {

            int activityCount = 1;
            foreach (var item in list)
            {
                Console.WriteLine("==========================================");
                //Console.WriteLine($"{item.Name}"); // Zumba/Box/Jump/Spnning???
                Console.WriteLine($"= {activityCount} ");
                Console.WriteLine($"= Starttid: {item.ScheduledTime} ");
                Console.WriteLine($"= Passets längd: { item.SessionLength} timmar. ");
                Console.WriteLine($"= Max antal deltagare: {item.MaxParticipants} ");
                Console.WriteLine($"= Platser kvar: {item.MaxParticipants - item.BookedParticipants} ");
                Console.WriteLine($"= Typ av träning: {item.Type} ");
                Console.WriteLine($"= Plats: {item.Room} ");
                Console.WriteLine($"= Tränare: {item.Trainer} ");
                Console.WriteLine("==========================================");
                Console.WriteLine();
                activityCount++;
            }
        }
    }
}
