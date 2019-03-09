using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        public string Number { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        [ForeignKey("Day")]
        public int? DayForeignKey { get; set; }
        public virtual Day Day { get; set; }
        public IList<UserFlat> UserFlat { get; set; } = new List<UserFlat>();
    }
}
