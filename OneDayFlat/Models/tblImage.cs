using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Models
{
    public class tblImage
    {
        public int ImageID { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public byte[] ImageData  { get; set; }

        //public int FlatID { get; set; }
        //public virtual Flat Flat { get; set; }
    }
}
