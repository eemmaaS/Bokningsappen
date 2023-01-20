using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift_Bokningsappen.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string ? DayName { get; set; }
        public ICollection<Room> Rooms { get; set; }

    }
    //    Room[] monday = { };
    //    Room[] teusday = { };
    //    Room[] wednesday = { };
    //    Room[] thursday = { };
    //    Room[] friday = { };

    //    string[,] rooms =
    //    {
    //        {monday},
    //        {tuesday},
    //        {wednesday},
    //        {thursday},
    //        {friday}
    //    };
}
