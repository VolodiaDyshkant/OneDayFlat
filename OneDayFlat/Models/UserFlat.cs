﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class UserFlat
    {
        // [Key]
        public int UserFlatID { get; set; }
        //[ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        //[ForeignKey("Flat")]
        public int FlatID { get; set; }
        public Flat Flat { get; set; }
    }
}
