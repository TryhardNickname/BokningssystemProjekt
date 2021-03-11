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
                    Console.WriteLine("Ange ditt medlems-id[nnnnn]: ");
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

                    if(userInput == "2")
                    {
                        RegisterNewUser(bs);


                    }
                }
            }


            //next menu!!!
            while (true)
            {

                int choices = PrintMenu(bs);

                Console.WriteLine("input choice: ");
                userInput = GetMenuInput(choices);

                //exit
                if (userInput == "0")
                    break;

                //1. Se Bokningsschemat
                if (userInput == "1")
                {
                    string schedule = bs.ShowSchedule();
                    Console.WriteLine(schedule);
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
                    string bi = bs.ShowBrokenEquip();
                    Console.WriteLine(bi);
                    Console.WriteLine("Ange vilken maskin du vill ändra[index]: ");
                    userInput = Console.ReadLine();

                    bs.ChangeEquipmentStatus(bs.CurrentUser, bs.Equipments[int.Parse(userInput)]);
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
