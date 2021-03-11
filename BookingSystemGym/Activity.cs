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
        public int Id { get; private set; }
        public string Trainer { get; private set; }

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

        public Activity(string[] lineFromFile)
        {
            SessionLength = int.Parse(lineFromFile[0]);
            ScheduledTime = DateTime.Parse(lineFromFile[1]);
            MaxParticipants = int.Parse(lineFromFile[2]);
            BookedParticipants = 0;
            Type = lineFromFile[3];
            Room = lineFromFile[4];
            Id = int.Parse(lineFromFile[5]);
            Trainer = lineFromFile[6];
        }

        public void UpdateActivity(int PropToChange, string newProp)
        {
            switch (PropToChange)
            {
                case 5:
                    this.Type = newProp;
                    break;  // In Bookingsystem, print the new value of Type
                case 6:
                    this.Room = newProp;
                    break;  // In Bookingsystem, print the new value of Room
                case 8:
                    this.Trainer = newProp;
                    break;  // In Bookingsystem, print the new value of Trainer
            }
        }

        public void UpdateActivity(int PropToChange, int newProp)
        {
            switch (PropToChange)
            {
                case 1:
                    this.SessionLength = newProp;
                    break;  // In Bookingsystem, print the new value of SessionLength
                case 3:
                    this.MaxParticipants = newProp;
                    break;  // In Bookingsystem, print the new value of MaxParticipants
                case 4:
                    this.BookedParticipants = newProp;
                    break;  // In Bookingsystem, print the new value of BookedParticipants
                case 7:
                    this.Id = newProp;
                    break;  // In Bookingsystem, print the new value of Id
            }
        }

        public void UpdateActivity(int PropToChange, DateTime newProp)
        {
            this.ScheduledTime = newProp;
        }
    }
}
