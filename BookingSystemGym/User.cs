using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class User
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }

        public User(string id, string role, string name)
        {
            Id = id;
            Role = role;
            Name = name;
        }

        public void MakeReservation(ref Activity a)
        {
            if (a.BookedParticipants == a.MaxParticipants)
                Console.WriteLine("could not book"); // felmeddelanden?

            a.BookedParticipants += 1;
        }

        enum EnumRole{
            admin,emp,user
        }

    }
}
