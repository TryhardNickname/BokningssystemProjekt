using System;
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
                        Console.WriteLine("Medlems-Id saknas, vill du registrera dig?[y/n]: ");
                        string yesorno = Console.ReadLine();
                        if (yesorno.ToLower() == "y")
                        {
                            RegisterNewUser(bs);
                            bs.SaveUserList();
                            break;
                        }
                    }

                }
                else if (userInput == "2")
                {
                    RegisterNewUser(bs);
                    bs.SaveUserList();
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
                    if (input == "1") //Sök efter pass
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


                        // Gets new list of the certain type chosen
                        List<Activity> TypeList = bs.ShowType(input);
                        PrintActivities(input, TypeList);

                        BookFromList(bs, TypeList);

                    }
                    else if (input == "2") //Sök dag
                    {
                        Console.WriteLine("Ange datum och tid som du söker pass efter (YYYY-MM-DD HH:MM:SS)"); // ändra till dag
                        string dateInput = Console.ReadLine(); // NYI Exception-handling

                        List<Activity> TimeList = bs.ShowTime(dateInput);
                        PrintActivities(dateInput, TimeList);

                        BookFromList(bs, TimeList);


                    }
                    else if (input == "3") //Sök PT
                    {
                        Console.WriteLine("Ange namnet på tränaren");
                        input = Console.ReadLine(); // NYI Exception-handling

                        List<Activity> TrainerList = bs.ShowTrainer(input);
                        PrintActivities(input, TrainerList);

                        BookFromList(bs, TrainerList);
                    }
                    else if (input == "4") // Visa alla pass
                    {
                        PrintActivities("Alla Pass: ", bs.ShowAllInSchedule());

                        BookFromList(bs, bs.ShowAllInSchedule());
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
                    Console.WriteLine("NYI");

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
                    bs.SaveSchedule();
                }
                //6. Ladda upp nytt bokningsschema
                if (userInput == "6")
                {
                    Console.WriteLine("Vänligen ange filnamn för ny schema-fil: ");
                    string fileName = Console.ReadLine();
                    if (File.Exists(fileName))
                    {
                        bs.scheduleFile = fileName;
                        bs.LoadSchedule(fileName); 
                    }
                }
            }

            bs.SaveEquipmentList();
            bs.SaveUserList();
            bs.SaveSchedule();
            Console.WriteLine("Hej då!");
            Console.ReadKey();
        }

        static void BookFromList(BookingSystem bs, List<Activity> list)
        {
            Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
            string input = GetMenuInput(list.Count + 1);
            if (input == "0")
            {
                return;
            }
            else
            {
                if (int.TryParse(input, out int index))
                {
                    bs.CurrentUser.MakeReservation(list[index - 1]);
                    Console.WriteLine("Din bokning har genomförts! Välkommen! ");
                }
                else
                {
                    Console.WriteLine("Fel val, bokning ej genomförd.");
                }
            }
        }

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

        //basic exception-handling
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

        static void RegisterNewUser(BookingSystem bs)
        {
            while (true)
            {
                Console.Write("Skriv ditt namn: ");
                string name = Console.ReadLine();

                Console.WriteLine("välj ditt medlems-id[nnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
                string newId = Console.ReadLine();

                //new user is added to UserList, and is set as CurrentUser
                if (bs.Register(newId, "GymUser", name))
                {
                    Console.WriteLine("Du är nu registrerad");
                    break;
                }
                else
                {
                    Console.WriteLine("Användare med det id-t finns redan, skriv in nytt.");
                }
            }

        }

        static int PrintMenu(BookingSystem bs)
        {

            Console.WriteLine();
            Console.WriteLine("================================================");
            Console.WriteLine($"= Välkommen till bokningen  {bs.CurrentUser.Name}!  =");
            Console.WriteLine("= 1. Se Bokningsschemat och boka pass          =");
            Console.WriteLine("= 2. Se maskiners status                       =");
            Console.WriteLine("= 3. Boka Pass,PT...                           =");

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

        static void PrintActivities(string input, List<Activity> list)
        {

            int activityCount = 1;
            Console.WriteLine($"Typ av träning {input}");
            foreach (var item in list)
            {
                Console.WriteLine("==========================================");
                //Console.WriteLine($"{item.Type}"); // Zumba/Box/Jump/Spnning???
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
