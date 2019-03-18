using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class FlatViewImage
    {
        public string OwnerName { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IList<Day> Days { get; set; }
        public IFormFile Image { get; set; }
    }
}
