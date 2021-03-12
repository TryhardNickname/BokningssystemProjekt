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
                if(userInput == "2")
                {
                    RegisterNewUser(bs);
                    break;
                }
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
                    Console.Clear();
                    Console.WriteLine("*Bokningsschemat*");
                    Console.WriteLine("1. Sök efter pass\n2. Sök efter tid/dag?\n3. Sök tränare\n4. Visa alla pass");
                    Console.Write(">");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("*Pass-sökning*");
                        Console.WriteLine("1. Sök på gym\n2. Sök på gruppträning\n3. Sök på träning med PT\n4. Sök på konsultation med PT");
                        Console.Write(">");
                        input = Console.ReadLine().ToLower();
                        Console.WriteLine();

                        List<Activity> ST = bs.ShowType(input);
                        int activityCount = 1;
                        foreach (var item in ST)
                        {
                            Console.WriteLine($"{activityCount}. Passets längd: {item.SessionLength} timmar\nStarttid: " +
                                              $"{item.ScheduledTime}\nMax antal deltagare: {item.MaxParticipants}\n" +
                                              $"Platser kvar: {item.MaxParticipants - item.BookedParticipants}\n" +
                                              $"Typ av träning: {item.Type}\nPlats: {item.Room}\nTränare: {item.Trainer}");
                            Console.WriteLine("------------------");
                            Console.WriteLine();
                            activityCount++;
                        }

                        Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
                        input = Console.ReadLine();
                        if (input == "0")
                        {
                            continue;
                        }
                        else
                        {
                            bs.CurrentUser.MakeReservation(ST[int.Parse(input)-1]);
                        }
                        

                    }
                    //string schedule = bs.ShowSchedule();
                    //Console.WriteLine(schedule);
                    //välj dag? / tid? pass?
                }
                //2. Se trasiga maskiner
                if (userInput == "2")
                {
                    string bi = bs.ShowBrokenEquip();
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
                    Console.WriteLine(bs.ShowBrokenEquip());
                    Console.WriteLine();
                    Console.WriteLine("Ange vilken maskin du vill ändra[index]: ");
                    userInput = Console.ReadLine();

                    bs.ChangeEquipmentStatus(bs.CurrentUser, bs.Equipments[int.Parse(userInput)]);
                }
                //5. Gör ändring i bokningsschemat
                if (userInput == "5")
                {
                    //välj activitet och sen:
                    bs.ChangeActivity(bs.CurrentUser);
                }
                //6. Ladda upp nytt bokningsschema
                if(userInput == "6")
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

        static void PrintLogInMenu()
        {
            Console.WriteLine("**Hej Och välkomna till BokningsSystemet**");
            Console.WriteLine("1. Logga in");
            Console.WriteLine("2. Registrera ny användare");
            Console.WriteLine("0. Exit");
        }

        //felhantering basic
        static string GetMenuInput(int amountOfChoices)
        {
            Console.Write(">");
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

        static void RegisterNewUser(BookingSystem bs)
        {
           
            Console.WriteLine("Skriv ditt namn: ");
            Console.Write(">");
            string name = Console.ReadLine();

            // Här kan vi göra på ett bättre sätt
            //Console.WriteLine("Skriv din roll[Employee/GymUser/Admin]: "); 
            //string role = Console.ReadLine();

            Console.Write(">");
            Console.WriteLine("välj ditt medlems-id[nnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
            string newId = Console.ReadLine();

            //new user is added to UserList, and is set as CurrentUser
            bs.Register(newId, "GymUser", name);
            //gå till nästa meny
        }

        static int PrintMenu(BookingSystem bs)
        {
            Console.WriteLine($"**Välkommen till bokningen {bs.CurrentUser.Name}!**");
            Console.WriteLine("1. Se Bokningsschemat och boka pass");
            Console.WriteLine("2. Se trasiga maskiner");
            Console.WriteLine("3. Boka Pass,PT...");
            if (bs.CurrentUser.Role != "user")
            {
                Console.WriteLine("4. Ange trasig maskin");
                Console.WriteLine("5. Gör ändring i bokningsschemat");

                if(bs.CurrentUser.Role == "admin")
                {
                    Console.WriteLine("6. Ladda upp bokningsschema");
                    Console.WriteLine("0. Logga ut");
                    return 6;
                }
                Console.WriteLine("0. Logga ut");
                return 5;
            }
            Console.WriteLine("0. Logga ut");
            return 4;
        }
    }
}
