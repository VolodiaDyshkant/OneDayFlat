using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class Day
    {
        public int DayID { get; set; }
        public bool Booked { get; set; }
        
        public int CalendarID { get; set; }
        public Calendar Calendar { get; set; }
        public User User { get; set; }

    }
}
