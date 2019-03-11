using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int FlatID { get; set; }
        public virtual Flat Flat { get; set; }
    }
}
