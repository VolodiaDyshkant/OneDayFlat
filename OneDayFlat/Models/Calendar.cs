using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class Calendar
    {
        public int CalendarID { get; set; }
        public DateTime CurrentTime { get; set; }

        public virtual Flat Flat { get; set; }
        public IList<Day> Days { get; set; }
    }
}
