using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class Activity
    {
        public int SessionLength { get; private set;  }
        public DateTime ScheduledTime{ get; private set; }
        public int MaxParticipants { get; private set; }
        public int BookedParticipants { get; set; }
        public string Type { get; private set; } //PT / 
        public string Room { get; private set; }
        public int Id { get; set; }
        public string Trainer { get; set; }

        public Activity(int sessionLength, DateTime scheduledTime, int maxParticipants, string type, string room, int id, string trainer)
        {
            SessionLength = sessionLength;
            ScheduledTime = scheduledTime;
            MaxParticipants = maxParticipants;
            BookedParticipants = 0;
            Type = type;
            Room = room;
            Id = id;
            Trainer = trainer;
        }


        public void UpdateActivity(int PropToChange)
        {

            switch (PropToChange)
            {
                case 1:
                    int newSessionLength = int.Parse(Console.ReadLine());
                    this.SessionLength = newSessionLength;
                    break;  // In Bookingsystem, print the new value of SessionLength
                case 2:
                    DateTime newScheduledTime = DateTime.Parse(Console.ReadLine());
                    this.ScheduledTime = newScheduledTime;
                    break;  // In Bookingsystem, print the new value of ScheduledTime
                case 3:
                    int newMaxParticipants = int.Parse(Console.ReadLine());
                    this.MaxParticipants = newMaxParticipants;
                    break;  // In Bookingsystem, print the new value of MaxParticipants
                case 4:
                    int newBookedParticipants = int.Parse(Console.ReadLine());
                    this.BookedParticipants = newBookedParticipants;
                    break;  // In Bookingsystem, print the new value of BookedParticipants
                case 5:
                    string newType = Console.ReadLine();
                    this.Type = newType;
                    break;  // In Bookingsystem, print the new value of Type
                case 6:
                    string newRoom = Console.ReadLine();
                    this.Room = newRoom;
                    break;  // In Bookingsystem, print the new value of Room
                case 7:
                    int newId = int.Parse(Console.ReadLine());
                    this.Id = newId;
                    break;  // In Bookingsystem, print the new value of Id
                case 8:
                    string newTrainer = Console.ReadLine();
                    this.Trainer = newTrainer;
                    break;  // In Bookingsystem, print the new value of Trainer
            }

            
        }
    }
}
