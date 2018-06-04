using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendence_Counter_ALPHA.Classes
{

    class Functions
    {
        public static string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
    class Verifier
    {
        public static DataAccess db = new DataAccess();

        public static bool SameNameExists(string firstName, string lastName)
        {
            bool x = false;
            List<Teacher> t1 = new List<Teacher>();
            t1 = db.GetTeachersByFirstName(firstName);
            foreach (var teacher in t1)
            {
                if (teacher.LastName == lastName)
                    x = true;
            }
            return x;
        }
        public static bool IdTaken(string id)
        {
            bool x = false;
            List<Teacher> t1 = new List<Teacher>();
            t1 = db.CheckID(id);
            foreach (var teacher in t1)
            {
                if (teacher.TeacherId == id)
                    x = true;
            }
            return x;
        }
    }

    class Calc
    {
        public static string GetPercent(decimal p, decimal a)
        {
            decimal percent;
            string pcnt;
            if (p + a == 0)
                percent = 0;
            else
                percent = 100 * p / (p + a);

            if (percent < 75)
                pcnt = "-" + Decimal.Round(percent,2) + "-";
            else
                pcnt = Decimal.Round(percent, 2).ToString();

            return pcnt;
        }
        //public static int SubSelector(string batch, Teacher teacher)
        //{
        //    int sub = 0;
        //    if (batch=="SE")


        //        return sub;
        //}
    }

    class Converter
    {
        public static int BatchToSem(string batch)
        {
            int i;
            if (batch == "SE")
                i = 3 + MainWindow.evenSem;
            else if (batch == "TE")
                i = 5 + MainWindow.evenSem;
            else
                i = 7 + MainWindow.evenSem;
            return i;
        }

        public static Stdnt StudentToStdnt(Student student)
        {
            Stdnt a = new Stdnt();
            a.Id = student.RollNo;
            a.FirstName = student.FirstName;
            a.LastName = student.LastName;
            a.Present[0] = student.Present1;
            a.Present[1] = student.Present2;
            a.Present[2] = student.Present3;
            a.Present[3] = student.Present4;
            a.Present[4] = student.Present5;
            a.Present[5] = student.Present6;
            a.Absent[0] = student.Absent1;
            a.Absent[1] = student.Absent2;
            a.Absent[2] = student.Absent3;
            a.Absent[3] = student.Absent4;
            a.Absent[4] = student.Absent5;
            a.Absent[5] = student.Absent6;
            
            return a;
        }
        public static Student StdntToStudent(Stdnt stdnt)
        {
            Student a = new Student();
            a.RollNo = stdnt.Id;
            a.FirstName = stdnt.FirstName;
            a.LastName = stdnt.LastName;
            a.Present1 = stdnt.Present[0];
            a.Present2 = stdnt.Present[1];
            a.Present3 = stdnt.Present[2];
            a.Present4 = stdnt.Present[3];
            a.Present5 = stdnt.Present[4];
            a.Present6 = stdnt.Present[5];
            a.Absent1 = stdnt.Absent[0];
            a.Absent2 = stdnt.Absent[1];
            a.Absent3 = stdnt.Absent[2];
            a.Absent4 = stdnt.Absent[3];
            a.Absent5 = stdnt.Absent[4];
            a.Absent6 = stdnt.Absent[5];

            return a;
        }
        public static Sbjct SubjectToSbjct(List<string> subject, int i)
        {
            Sbjct s = new Sbjct();
            int j = 0;
            foreach(var item in subject)
            {
                
                s.Sub[j] = item;
                j++;
            }
            return s;
        }
        public static string GetSubject(List<string> list)
        {
            string a = "";
            foreach (var item in list)
                if (item != null)
                    a = item;
            return a;
        }
        public static int GetSubjectId(List<int> list)
        {
            int a = 0;
            foreach (var item in list)
                if (item != 0)
                    a = item;
            return a;
        }
    }

    class Display
    {
        public static int z;

        public static StudentRow DisplayStudentHeader(string batch)
        {
            StudentRow header = new StudentRow();

            header.idLabel.Content = "ID";
            header.nameLabel.Content = "Name";
            
            if (batch == "Common")
            {
                for (int i = 1; i < 7; i++)
                {
                    StudentLabel studentLabel = new StudentLabel();
                    studentLabel.pLabel.Content = "Subject " + i;
                    studentLabel.percentLabel.Content = "%";
                    header.attStk.Children.Add(studentLabel);
                }
                z = 6;
            }
            else 
            {
                DataAccess db = new DataAccess();
                Sbjct subs = new Sbjct();
                subs = db.GetSubjects(batch);
                z = 0;
                for(int i = 0; i < 6; i++)
                {
                    if (subs.Sub[i] != null)
                        z++;
                }
                for(int i = 0; i < z; i++)
                {
                    StudentLabel studentLabel = new StudentLabel();
                    studentLabel.pLabel.Content = subs.Sub[i];
                    studentLabel.percentLabel.Content = "%";
                    header.attStk.Children.Add(studentLabel);
                }
            }
            return header;
        }
        public static StudentEditorHeader DisplayStudentEditorHeader(string batch)
        {
            StudentEditorHeader header = new StudentEditorHeader();

            header.rollNoLabel.Content = "Roll No.";
            header.nameLabel.Content = "Name";
            
            DataAccess db = new DataAccess();
            Sbjct subs = new Sbjct();
            subs = db.GetSubjects(batch);
            z = 0;
            for (int i = 0; i < 6; i++)
            {
                if (subs.Sub[i] != null)
                    z++;
            }
            for (int i = 0; i < z; i++)
            {
                StudentEditorHeaderLabel subjectLabel = new StudentEditorHeaderLabel();
                subjectLabel.subLabel.Content = subs.Sub[i];
                header.attStk.Children.Add(subjectLabel);
            }
        return header;
        }
        public static StudentRow DisplayStudent(Stdnt s)
        {
            int x = 0, y = 0;
            StudentRow a = new StudentRow();
            a.idLabel.Content = s.Id;
            a.nameLabel.Content = s.FirstName + " " + s.LastName;

            for(int i = 0; i < z; i++)
            {
                StudentLabel studentLabel = new StudentLabel();
                studentLabel.pLabel.Content = s.Present[i];
                studentLabel.percentLabel.Content = Calc.GetPercent(s.Present[i], s.Absent[i]);
                a.attStk.Children.Add(studentLabel);
                x = x + s.Present[i];
                y = y + s.Absent[i];
            }
            a.percentTotalLabel.Content = Calc.GetPercent(x, y);

            return a;
        }
        public static StudentEditorRow DisplayStudentEditor(Stdnt s)
        {
            StudentEditorRow a = new StudentEditorRow();
            a.rollNoBox.Text = s.Id.ToString();
            a.nameBox.Text = s.FirstName + " " + s.LastName;
            for (int i = 0; i < z; i++)
            {
                StudentEditorBox studentBox = new StudentEditorBox();
                studentBox.pBox.Text = s.Present[i].ToString();
                studentBox.aBox.Text = s.Absent[i].ToString();
                a.edtStk.Children.Add(studentBox);
            }
            return a;
        }
    }
}
