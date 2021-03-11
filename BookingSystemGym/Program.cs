using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    string id = Console.ReadLine(); //lägg till felhantering
                    

                    if (bs.LogIn(id))
                    {
                        //next menu bokningen 
                        break;
                    }
                    else
                    {
                        Console.WriteLine("medlems id saknas, vill du registrera dig?");
                        string yesorno = Console.ReadLine(); // lägg till felhantering
                        if (yesorno == "yes")
                        {
                            //namn
                            Console.WriteLine("skriv ditt namn: ");
                            string name = Console.ReadLine();
                            //roll
                            Console.WriteLine("skriv din roll: "); //??rimlig fråga?
                            string role = Console.ReadLine();
                            Console.WriteLine("välj ditt medlems-id[nnnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
                            string newId = Console.ReadLine();

                            //new user is added to UserList, and is set as CurrentUser
                            bs.Register(newId, role, name);
                            //gå till nästa meny
                        }
                    }

                    if(userInput == "2")
                    {
                        //namn
                        Console.WriteLine("skriv ditt namn: ");
                        string name = Console.ReadLine();
                        //roll
                        Console.WriteLine("skriv din roll: "); //??rimlig fråga?
                        string role = Console.ReadLine();
                        Console.WriteLine("välj ditt medlems-id[nnnnn]: "); //ge random? kolla igenom userlist och ge nästa? välja själv? felhantering?
                        string newId = Console.ReadLine();

                        //new user is added to UserList, and is set as CurrentUser
                        bs.Register(newId, role, name);
                        //gå till nästa meny


                    }
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
                    Console.WriteLine("1. Sök efter pass\n2. Sök efter tid/dag?\n3. Sök tränare\n4. Visa alla pass");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.WriteLine("1. Sök på gym\n2. Sök på gruppträning\n3. Sök på träning med PT\n4. Sök på konsultation med PT");
                        input = Console.ReadLine().ToLower(); 

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
                        if (input == 0.ToString())
                        {
                            break;
                        }
                        else
                        {
                            bs.CurrentUser.MakeReservation(ST[activityCount - 1]);
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
                    // vilket sorts pass vill du boka?
                    //<<PTpass
                    //lista över pt pass, välj ett
                    //bs.CurrentUser.MakeReservation()
                }
                //4. Ange trasig maskin
                if (userInput == "4")
                {
                    //
                }
                //5. Gör ändring i bokningsschemat
                if (userInput == "5")
                {
                    //välj activitet och sen:
                    //changeactivity()?
                    //
                }
                //6. Skapa nytt bokningsschema
                if(userInput == "6")
                {
                    //load new textfile ?
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

        static void RegisterNewUser()
        {
            
        }

        static int PrintMenu(BookingSystem bs)
        {
            Console.WriteLine("**Välkommen till bokningen!**");
            Console.WriteLine("1. Se Bokningsschemat");
            Console.WriteLine("2. Se trasiga maskiner");
            Console.WriteLine("3. Boka Pass,PT...");
            if (bs.CurrentUser.Role != "user")
            {
                Console.WriteLine("4. Ange trasig maskin");
                Console.WriteLine("5. Gör ändring i bokningsschemat");

                if(bs.CurrentUser.Role == "admin")
                {
                    Console.WriteLine("6. Skapa nytt bokningsschema");
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
