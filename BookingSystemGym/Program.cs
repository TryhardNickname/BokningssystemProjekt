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

                    }
                }

            }


            
            
            
            //while (userInput != "0")
            //{
            //    Console.WriteLine("input choice: ");
            //    userInput = Console.ReadLine();

            //    if (userInput == "2")
            //    {
            //        string schedule = bs.ShowSchedule();
            //        Console.WriteLine(schedule);
            //    }
            //    if (userInput == "3")
            //    {
            //        string bi = bs.ShowBrokenEquip();
            //        Console.WriteLine(bi);
            //    }

                
            //}



            Console.ReadKey();

        }

        static void PrintLogInMenu()
        {
            Console.WriteLine("**Hej Och välkomna till BokningsSystemet**");
            Console.WriteLine("1 Logga in");
            Console.WriteLine("2 Registrera ny användare");
            Console.WriteLine("0 Exit");
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
            }
        }

        static void RegisterNewUser()
        {

        }
    }
}
