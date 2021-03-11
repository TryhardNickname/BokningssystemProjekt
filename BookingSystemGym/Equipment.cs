using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemGym
{
    class Equipment
    {

        public bool Broken { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }


        public Equipment(bool broken, int id, string name)
        {
            Broken = broken;
            Id = id;
            Name = name;
        }

        public void ChangeStatus()
        {
            Broken = !Broken;
        }
    }
}
