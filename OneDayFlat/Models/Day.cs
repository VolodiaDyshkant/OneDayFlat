using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class Day
    {
        [Key]
        public int DayID { get; set; }

        public bool Booked { get; set; }
        
        //[ForeignKey("Calendar")]
        public int? CalendarID { get; set; }
        public Calendar Calendar { get; set; }

        //[ForeignKey("User")]
        public int? UserID { get; set; }
        public virtual User User { get; set; }

    }
}
