﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }

        public User()
        {

        }

        public void MakeReservation(Activity a)
        {
            a.BookedParticipants += 1;
        }

    }
}
