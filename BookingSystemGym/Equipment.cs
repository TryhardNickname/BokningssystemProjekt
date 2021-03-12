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
        public string Id { get; set; }
        public string Name { get; set; }


        public Equipment(bool broken, string id, string name)
        {
            Broken = broken;
            Id = id;
            Name = name;
        }

        public Equipment(string[] equipmentInfo)
        {
            Broken = bool.Parse(equipmentInfo[0]);
            Id = equipmentInfo[1];
            Name = equipmentInfo[2];
        }

        public void ChangeStatus()
        {
            Broken = !Broken;
        }
    }
}
