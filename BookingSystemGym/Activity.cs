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

        public Activity(int sessionLength, DateTime scheduledTime, int maxParticipants, string type, string room, int id)
        {
            SessionLength = sessionLength;
            ScheduledTime = scheduledTime;
            MaxParticipants = maxParticipants;
            BookedParticipants = 0;
            Type = type;
            Room = room;
            Id = id;
        }


        public void UpdateActivity(int PropToChange)
        {

            switch (PropToChange)
            {
                case 1:
                    int newSessionLength = int.Parse(Console.ReadLine());
                    this.SessionLength = newSessionLength;
                    break;
                case 2:
                    DateTime newScheduledTime = DateTime.Parse(Console.ReadLine());
                    this.ScheduledTime = newScheduledTime;
                    break;
                case 3:
                    int newMaxParticipants = int.Parse(Console.ReadLine());
                    this.MaxParticipants = newMaxParticipants;
                    break;
                case 4:
                    int newBookedParticipants = int.Parse(Console.ReadLine());
                    this.BookedParticipants = newBookedParticipants;
                    break;
                case 5:
                    string newType = Console.ReadLine();
                    this.Type = newType;
                    break;
                case 6:
                    string newRoom = Console.ReadLine();
                    this.Room = newRoom;
                    break;
                case 7:
                    int newId = int.Parse(Console.ReadLine());
                    this.Id = newId;
                    break;
            }

            
        }
    }
}
