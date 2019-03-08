using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class Flat
    {
        [Key]
        public int RoomID { get; set; }
        public string City { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey("Calendar")]
        public int CalendarID { get; set; }
        public virtual Calendar Calendar { get; set; }

        public List<UserFlat> UserFlat { get; set; }
    }
}
