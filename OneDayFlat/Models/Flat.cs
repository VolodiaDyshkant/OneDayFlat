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

        public List<UserFlat> UserFlat { get; set; }
        public IList<Day> Days { get; set; }
    }
}