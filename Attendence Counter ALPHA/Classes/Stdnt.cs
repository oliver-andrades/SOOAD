using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence_Counter_ALPHA
{
    public class Stdnt
    {
        public int Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public int[] Present = new int[6];

        public int[] Absent = new int[6];
    }
}
