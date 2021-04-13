using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notes
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public float Note { get; set; }
    }
}
