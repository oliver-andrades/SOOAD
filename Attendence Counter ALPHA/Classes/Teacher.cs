using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence_Counter_ALPHA
{
    public class Teacher
    {
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SubjectSE { get; set; }

        public int SubjectTE { get; set; }

        public int SubjectBE { get; set; }
        
        public string Salt { get; set; }

        public string SaltedHashedPassword { get; set; }

        public bool IsHod { get; set; }

        public string IsClassTeacher { get; set; }

        //public string FullInfo
        //{
        //    get
        //    {
        //        return $"{ FirstName } { LastName } ({ Salt })";
        //    }
        //}
    }
}
