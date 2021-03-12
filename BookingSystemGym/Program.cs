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
                    Console.Write("Ange ditt medlems-id[nnnn]: ");
                    string id = Console.ReadLine(); //lägg till felhantering


                    if (bs.LogIn(id))
                    {
                        //next menu bokningen 
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Medlems-Id saknas, vill du registrera dig?");
                        string yesorno = Console.ReadLine(); // lägg till felhantering
                        if (yesorno == "yes")
                        {

                            RegisterNewUser(bs);

                        }
                    }
                }
                else if (userInput == "2")
                {
                    RegisterNewUser(bs);
                    break;
                }
            }


            //next menu!!!
            while (true)
            {

                int choices = PrintMenu(bs);

                Console.Write("Ange val: ");
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
                    Console.WriteLine("==========================================");
                    Console.Write("Ange val: ");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.WriteLine("==========================================");
                        Console.WriteLine("= 1. Sök på gym                          =");
                        Console.WriteLine("= 2. Sök på gruppträning                 =");
                        Console.WriteLine("= 3. Sök på träning med PT               =");
                        Console.WriteLine("= 4. Sök på konsultation med PT          =");
                        Console.WriteLine("==========================================");
                        Console.Write("Ange val: ");

                        input = Console.ReadLine().ToLower();

                        List<Activity> ST = bs.ShowType(input);
                        int activityCount = 1;
                        Console.WriteLine($"Typ av träning {input}");
                        foreach (var item in ST)
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

                    else if (input == "2")
                    {

                    }


                    else if (input == "3")
                    {

                    }


                    else if (input == "4")
                    {

                    }

                    else
                    {
                        Console.WriteLine("Du måste ange en siffra mellan 1 - 4. ");
                    }

                    Console.WriteLine("Ange vilket pass du vill boka en plats i: (0 för att gå tillbaks)");
                        input = Console.ReadLine();
                        if (input == "0")
                        {
                            break; //Break program
                        }
                        else
                        {
                            bs.CurrentUser.MakeReservation(ST[int.Parse(input) - 1]); 
                            Console.WriteLine("Din bokning har genomförts! Välkommen! "); 
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
                    //
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

        //felhantering basic
        static string GetMenuInput(int amoutOfChoices)
        {
            string userInput = Console.ReadLine();

            while (true)
            {
                if (int.Parse(userInput) < amoutOfChoices)
                {
                    return userInput;
                }
                Console.WriteLine("Fel input, försök igen: ");
                userInput = Console.ReadLine();
            }
        }

        static void RegisterNewUser(BookingSystem bs)
        {

            Console.WriteLine("Skriv ditt namn: ");
            string name = Console.ReadLine();

            // Här kan vi göra på ett bättre sätt
            Console.WriteLine("Skriv din roll[Employee/GymUser/Admin ]: ");
            string role = Console.ReadLine();
            Console.WriteLine("välj ditt medlems-id[nnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
            string newId = Console.ReadLine();

            //new user is added to UserList, and is set as CurrentUser
            bs.Register(newId, role, name);
            //gå till nästa meny
        }

        static int PrintMenu(BookingSystem bs)
        {
            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("= Välkommen till bokningen!              =");
            Console.WriteLine("= 1. Se Bokningsschemat och boka pass    =");
            Console.WriteLine("= 2. Se trasiga maskiner                 =");
            Console.WriteLine("= 3. Boka Pass,PT...                     =");
            if (bs.CurrentUser.Role != "user")
            {
                Console.WriteLine("= 4. Ange trasig maskin              =");
                Console.WriteLine("= 5. Gör ändring i bokningsschemat   =");

                if (bs.CurrentUser.Role == "admin")
                {
                    Console.WriteLine("= 6. Ladda upp bokningsschema    =");
                    Console.WriteLine("= 0. Logga ut                    =");
                    return 6;
                }
                Console.WriteLine("= 0. Logga ut                        =");
                return 5;
            }
            Console.WriteLine("= 0. Logga ut                            =");
            Console.WriteLine("==========================================");
            return 4;
        }
    }
}
