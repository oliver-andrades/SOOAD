using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence_Counter_ALPHA
{
    public class Person
    {
        public int Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public int Present { get; set; }

        public int Absent { get; set; }

        public string FullInfo
        {
            get
            {
                return $"{ FirstName } { LastName } { Present } { Absent }";
            }
        }
    }
}
