
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Attendence_Counter_ALPHA
{
    public class DataAccess
    {
        public Sbjct GetSubjects(string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string sem = "SELECT Sem" + Classes.Converter.BatchToSem(batch) + " FROM Subjects";
                return Classes.Converter.SubjectToSbjct(connection.Query<string>(sem).ToList(), 8);

                //List<string> op = new List<string>();
                //if (MainWindow.evenSem == 0)
                //{
                //    if (batch == "SE")
                //        op = connection.Query<string>($"select Sem3 from Subjects").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<string>($"select Sem5 from Subjects").ToList();
                //    else
                //        op = connection.Query<string>($"select Sem7 from Subjects").ToList();
                //}
                //else
                //{
                //    if (batch == "SE")
                //        op = connection.Query<string>($"select Sem4 from Subjects").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<string>($"select Sem6 from Subjects").ToList();
                //    else
                //        op = connection.Query<string>($"select Sem8 from Subjects").ToList();
                //}
                //return Classes.Converter.SubjectToSbjct(op, 8);
            }
        }
        public string GetSubjectByNo(string batch, int subId)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "SELECT Sem" + Classes.Converter.BatchToSem(batch) + " FROM Subjects WHERE Id = @subId";
                return Classes.Converter.GetSubject(connection.Query<string>(query, new { subId }).ToList());

                //if (MainWindow.evenSem == 0)
                //{
                //    if (batch == "SE")
                //        op = connection.Query<string>($"select Sem3 from Subjects where Id = { teacher.SubjectSE }").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<string>($"select Sem5 from Subjects where Id = { teacher.SubjectTE }").ToList();
                //    else
                //        op = connection.Query<string>($"select Sem7 from Subjects where Id = { teacher.SubjectBE }").ToList();
                //}
                //else
                //{
                //    if (batch == "SE")
                //        op = connection.Query<string>($"select Sem4 from Subjects where Id = { teacher.SubjectSE }").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<string>($"select Sem6 from Subjects where Id = { teacher.SubjectTE }").ToList();
                //    else
                //        op = connection.Query<string>($"select Sem8 from Subjects where Id = { teacher.SubjectBE }").ToList();
                //}
            }
        }
        public int GetNoBySubject(string batch, string subject)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "SELECT Id FROM Subjects WHERE Sem" + Classes.Converter.BatchToSem(batch) + " = @subject";
                return Classes.Converter.GetSubjectId(connection.Query<int>(query, new { subject }).ToList());

                //if (MainWindow.evenSem == 0)
                //{
                //    if (batch == "SE")
                //        op = connection.Query<int>($"select Id from Subjects where Sem3 = '{ subject }'").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<int>($"select Id from Subjects where Sem5 = '{ subject }'").ToList();
                //    else
                //        op = connection.Query<int>($"select Id from Subjects where Sem7 = '{ subject }'").ToList();
                //}
                //else
                //{
                //    if (batch == "SE")
                //        op = connection.Query<int>($"select Id from Subjects where Sem4 = '{ subject }'").ToList();
                //    else if (batch == "TE")
                //        op = connection.Query<int>($"select Id from Subjects where Sem6 = '{ subject }'").ToList();
                //    else
                //        op = connection.Query<int>($"select Id from Subjects where Sem8 = '{ subject }'").ToList();
                //}
                //a = Classes.Converter.GetSubjectId(op);
            }
        }
        public List<Teacher> CheckID(string id)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
                return connection.Query<Teacher>($"select TeacherId from Teacher where TeacherId = '{ id }'").ToList();
        }
        public List<Teacher> GetTeachersByFirstName(string firstName)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
                return connection.Query<Teacher>($"select * from Teacher where FirstName = '{ firstName }'").ToList();
        }

        public List<Student> GetStudentsByFirstName(string firstName, string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "SELECT * FROM " + batch + "Comps WHERE FirstName = @firstname";
                return connection.Query<Student>(query, new { firstName }).ToList();

                //List<Student> op = new List<Student>();

                //if (batch == "SE")
                //    op = connection.Query<Student>($"select * from SeComps where FirstName = '{ firstName }'").ToList();
                //else if (batch == "TE")
                //    op = connection.Query<Student>($"select * from TeComps where FirstName = '{ firstName }'").ToList();
                //else
                //    op = connection.Query<Student>($"select * from BeComps where FirstName = '{ firstName }'").ToList();

                //return op;
            }
        }
        public List<Student> GetStudentsByLastName(string lastName, string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "SELECT * FROM " + batch + "Comps WHERE LastName = @lastName";
                return connection.Query<Student>(query, new { lastName }).ToList();

                //List<Student> op = new List<Student>();
                //if (batch == "SE")
                //    op = connection.Query<Student>($"select * from SeComps where LastName = '{ lastName }'").ToList();
                //else if (batch == "TE")
                //    op = connection.Query<Student>($"select * from TeComps where LastName = '{ lastName }'").ToList();
                //else
                //    op = connection.Query<Student>($"select * from BeComps where LastName = '{ lastName }'").ToList();
                
                //return op;
            }
        }
        public List<Student> GetStudentsByFullName(string firstName, string lastName, string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "SELECT * FROM " + batch + "Comps WHERE FirstName = @firstName AND LastName = @lastName";
                return connection.Query<Student>(query, new { firstName, lastName }).ToList();

                //List<Student> op = new List<Student>();
                //if (batch == "SE")
                //    op = connection.Query<Student>($"select * from SeComps where FirstName = '{ firstName }' and LastName = '{ lastName }'").ToList();
                //else if (batch == "TE")
                //    op = connection.Query<Student>($"select * from TeComps where FirstName = '{ firstName }' and LastName = '{ lastName }'").ToList();
                //else
                //    op = connection.Query<Student>($"select * from BeComps where FirstName = '{ firstName }' and LastName = '{ lastName }'").ToList();

                //return op;
            }
        }
        public List<Student> GetStudents(string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                string query = "select * from " + batch + "Comps";
                return connection.Query<Student>(query).ToList();

                //List<Student> op = new List<Student>();

                //if (batch == "SE")
                //    op = connection.Query<Student>($"select * from SeComps").ToList();
                //else if (batch == "TE")
                //    op = connection.Query<Student>($"select * from TeComps").ToList();
                //else if (batch == "BE")
                //    op = connection.Query<Student>($"select * from BeComps").ToList();

                //return op;
            }
        }
        

        public void AddUser(string teacherId, string firstName, string lastName, int subjectSE, int subjectTE, int subjectBE, string salt, string saltedHashedPassword, bool isHod, string isClassTeacher)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                var newTeacher = new Teacher { TeacherId = teacherId, FirstName = firstName, LastName = lastName, SubjectSE=subjectSE, SubjectTE=subjectTE, SubjectBE=subjectBE, Salt = salt, SaltedHashedPassword = saltedHashedPassword, IsHod=isHod, IsClassTeacher=isClassTeacher};

                connection.Execute(@"INSERT INTO [dbo].[Teacher] ([TeacherId], [FirstName], [LastName], [SubjectSE], [SubjectTE], [SubjectBE], [Salt], [SaltedHashedPassword], [IsHod], [IsClassTeacher]) VALUES (@TeacherId, @FirstName, @LastName, @subjectSE, @subjectTE, @subjectBE, @Salt, @SaltedHashedPassword, @IsHod, @IsClassTeacher)", newTeacher );
            }
        }
        public void UpdateUser(int id, string teacherId, string firstName, string lastName, int subjectSE, int subjectTE, int subjectBE, bool isHod, string isClassTeacher)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                var newTeacher = new Teacher { TeacherId = teacherId, FirstName = firstName, LastName = lastName, SubjectSE = subjectSE, SubjectTE = subjectTE, SubjectBE = subjectBE, IsHod = isHod, IsClassTeacher = isClassTeacher };

                connection.Execute($"update [Teacher] SET [TeacherId] = TeacherId, [FirstName] = @FirstName, [LastName] = @LastName, [SubjectSE] = @subjectSE, [SubjectTE] = @subjectTE, [SubjectBE] = @subjectBE, [IsHod] = @IsHod, [IsClassTeacher] = @IsClassTeacher where Id = '{ id }'", newTeacher);
            }
        }
        public void UpdatePassword(int id, string salt, string saltedHashedPassword)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                var newTeacher = new Teacher { Salt = salt, SaltedHashedPassword = saltedHashedPassword };

                connection.Execute($"update [Teacher] SET  [Salt] = @Salt, [SaltedHashedPassword] = @SaltedHashedPassword  where Id = '{ id }'", newTeacher);
            }
        }
        public void UpdateAttendance(int id, int present, int absent, int sub, string batch)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
            {
                //string presentNo = "Present" + (sub + 1);
                //string absentNo = "Absent" + (sub + 1);

                string query = "UPDATE [" + batch + "Comps] SET Present" + (sub + 1) + " = @present, Absent" + (sub + 1) + " = @absent WHERE RollNo = " + id;
                connection.Execute(query, new { present, absent });
            }
        }

        public List<Teacher> GetTeachers()
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
                return connection.Query<Teacher>($"select * from Teacher").ToList();
        }
        public List<Teacher> Validate(string teacherId)
        {
            using (IDbConnection connection = new SqlConnection(Helper.CnnVal("Records")))
                return connection.Query<Teacher>($"select * from Teacher where TeacherId = '{ teacherId }'").ToList();
        }

    }
}
