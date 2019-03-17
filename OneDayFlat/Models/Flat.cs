using Microsoft.AspNetCore.Mvc;
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
        //public string City { get; set; }

        [Required(ErrorMessage = "Please enter the owner name")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Please enter the number of owner")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public string ImageMimeType { get; set; }

        //[ForeignKey("Calendar")]
        //public int CalendarID { get; set; }
        //public virtual Calendar Calendar { get; set; }

        public List<UserFlat> UserFlat { get; set; }
        public IList<Day> Days { get; set; }
    }
}