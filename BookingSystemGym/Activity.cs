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
            Type = type;
            Room = room;
            BookedParticipants = 0;
            Id = id;
        }


        public void UpdateActivity()
        {
            
        }
    }
}
