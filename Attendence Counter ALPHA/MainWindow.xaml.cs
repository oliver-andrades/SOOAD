using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;


namespace Attendence_Counter_ALPHA
{
    
    public partial class MainWindow : Window
    {
        DataAccess db = new DataAccess();
        public static int evenSem = 0;
        List<Student> people = new List<Student>();
        
        public MainWindow()
        {
            InitializeComponent();
            DateTime today = DateTime.Now;
            if (today.Month < 7)
                evenSem = 1;
            
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            subjectIndexStk.Visibility = Visibility.Visible;
            stk.Children.Clear();

            if(searchLastName.Text=="" && searchFirstName.Text=="")
            {
                if(stk.Children.Count==0)
                {
                    Label temp = new Label();
                    temp.Content = "Pleast enter a term to search.";
                    stk.Children.Add(temp);
                }
            }
            else if(searchLastName.Text=="")
            {
                stk.Children.Add(Classes.Display.DisplayStudentHeader("Common"));

                people = db.GetStudentsByFirstName(searchFirstName.Text, "SE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByFirstName(searchFirstName.Text, "TE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByFirstName(searchFirstName.Text, "BE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
            }
            else if(searchFirstName.Text=="")
            {
                stk.Children.Add(Classes.Display.DisplayStudentHeader("Common"));

                people = db.GetStudentsByLastName(searchLastName.Text, "SE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByLastName(searchLastName.Text, "TE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByLastName(searchLastName.Text, "BE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
            }
            else
            {
                stk.Children.Add(Classes.Display.DisplayStudentHeader("Common"));

                people = db.GetStudentsByFullName(searchFirstName.Text, searchLastName.Text, "SE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByFullName(searchFirstName.Text, searchLastName.Text, "TE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
                people = db.GetStudentsByFullName(searchFirstName.Text, searchLastName.Text, "BE");
                foreach (var person in people)
                    stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
            }
            
            searchFirstName.Text = "";
            searchLastName.Text = "";
        }
        
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            List<Teacher> t1 = new List<Teacher>();
            t1 = db.Validate(userNameBox.Text);
            string pswd = passwordBox.Password;
            
            foreach (var item in t1)
            {
                string salt = item.Salt;
                string sltdHshdPsswrd = item.SaltedHashedPassword;
                string tchrId = item.TeacherId.ToString();
                string temp = Classes.Functions.GenerateSHA256Hash(pswd, salt);

                userNameBox.Text = "";
                passwordBox.Password = "";

                if (sltdHshdPsswrd == temp )
                {
                   // this.Hide();
                    postLoginWindow pstlgn = new postLoginWindow(item);
                    pstlgn.Show();
                }
                else
                    MessageBox.Show("Wrong UserName or Password");
            }
        }

        private void DisplayTeachers_Click(object sender, RoutedEventArgs e)
        {
            stk.Children.Clear();
            List<Teacher> teacher = new List<Teacher>();
            teacher = db.GetTeachers();

            TeacherRow header = new TeacherRow();
            header.idLabel.Content = "ID";
            header.firstNameLabel.Content = "First Name";
            header.lastNameLabel.Content = "Last Name";
            header.seLabel.Content = "SE";
            header.teLabel.Content = "TE";
            header.beLabel.Content = "BE";
            header.saltLabel.Content = "Salt";
            header.SaltedHashedPasswordLabel.Content = "SaltedHashedPassword";
            header.hodLabel.Content = "IsHOD";
            header.classTeacherLabel.Content = "Classteacher";

            stk.Children.Add(header);
          

            foreach (var person in teacher)
            {
                TeacherRow a = new TeacherRow();
                a.idLabel.Content = person.Id;
                a.firstNameLabel.Content = person.FirstName;
                a.lastNameLabel.Content = person.LastName;
                a.seLabel.Content = person.SubjectSE;
                a.teLabel.Content = person.SubjectTE;
                a.beLabel.Content = person.SubjectBE;
                a.saltLabel.Content = person.Salt;
                a.SaltedHashedPasswordLabel.Content = person.SaltedHashedPassword;
                a.hodLabel.Content = person.IsHod;
                a.classTeacherLabel.Content = person.IsClassTeacher;
                
                stk.Children.Add(a);
            }
        }
    }
}
