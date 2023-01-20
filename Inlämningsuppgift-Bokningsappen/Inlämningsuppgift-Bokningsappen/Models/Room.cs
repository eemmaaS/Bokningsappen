using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift_Bokningsappen.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Beds { get; set; }
        public bool Balcony { get; set; }
        public bool Booked { get; set; }
        public int? GuestId { get; set; }
        public int? DayId { get; set; }

    }
}
