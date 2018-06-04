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
using System.Windows.Shapes;

namespace Attendence_Counter_ALPHA
{
    /// <summary>
    /// Interaction logic for postLoginWindow.xaml
    /// </summary>
    public partial class postLoginWindow : Window
    {

        public static int x = 0;
        public static int y = 0;
        public static string z = "";
        public static string id;
        public static Teacher t1;
        public static List<Stdnt> stdnt = new List<Stdnt>();

        DataAccess db = new DataAccess();
        public postLoginWindow(Teacher t)
        {
            InitializeComponent();
            t1 = t;
            id = t1.TeacherId;
            l.Content = id;
            if(t1.IsHod == true)
                privilageLabel.Content = "You have logged in as an Administrator";
            else if(t1.IsClassTeacher!=null)
            {
                privilageLabel.Content = "You have logged in as the Class Teacher of " + t1.IsClassTeacher + " Comps";
                addTeacherPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                privilageLabel.Visibility = Visibility.Hidden;
                addTeacherPanel.Visibility = Visibility.Hidden;
            }
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }
        private void SE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "SE";
            displayAttendence.IsEnabled = true;
            if (t1.SubjectSE != 0)
                takeAttendence.Visibility = Visibility.Visible;
            else takeAttendence.Visibility = Visibility.Hidden;

            if (t1.IsClassTeacher == classButtonLabel.Text)
                editStudents.Visibility = Visibility.Visible;
            else editStudents.Visibility = Visibility.Hidden;
        }
        private void TE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "TE";
            displayAttendence.IsEnabled = true;
            if (t1.SubjectTE != 0)
                takeAttendence.Visibility = Visibility.Visible;
            else takeAttendence.Visibility = Visibility.Hidden;

            if (t1.IsClassTeacher == classButtonLabel.Text)
                editStudents.Visibility = Visibility.Visible;
            else editStudents.Visibility = Visibility.Hidden;
        }
        private void BE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "BE";
            displayAttendence.IsEnabled = true;
            if (t1.SubjectBE != 0)
                takeAttendence.Visibility = Visibility.Visible;
            else takeAttendence.Visibility = Visibility.Hidden;

            if (t1.IsClassTeacher == classButtonLabel.Text)
                editStudents.Visibility = Visibility.Visible;
            else editStudents.Visibility = Visibility.Hidden;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow addWindow = new addUserWindow();
            addWindow.ShowDialog();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
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
            header.hodLabel.Content = "IsHOD";
            header.classTeacherLabel.Content = "Classteacher";
            header.auth.Visibility = Visibility.Hidden;
            
            stk.Children.Add(header);
          

            foreach (var person in teacher)
            {
                TeacherEditor a = new TeacherEditor();
                a.id.Text = person.Id.ToString();
                a.idLabel.Text = person.Id.ToString();
                a.firstNameLabel.Text = person.FirstName;
                a.lastNameLabel.Text = person.LastName;
                a.seLabel.Text = db.GetSubjectByNo("SE", person.SubjectSE);
                a.teLabel.Text = db.GetSubjectByNo("TE", person.SubjectTE);
                a.beLabel.Text = db.GetSubjectByNo("BE", person.SubjectBE);
                if (person.IsHod == true)
                    a.hodLabel.IsChecked = true;
                else
                    a.hodLabel.IsChecked = false;
                a.classTeacherLabel.Text = person.IsClassTeacher;
                
                stk.Children.Add(a);
            }
        }

        private void DisplayAttendence_Click(object sender, RoutedEventArgs e)
        {
            stk.Children.Clear();
            stk.Children.Add(Classes.Display.DisplayStudentHeader(classButtonLabel.Text));
            
            List<Student> students = db.GetStudents(classButtonLabel.Text);
            foreach (var person in students)
                stk.Children.Add(Classes.Display.DisplayStudent(Classes.Converter.StudentToStdnt(person)));
        }
        private void TakeAttendence_Click(object sender, RoutedEventArgs e)
        {  
            stk.Children.Clear();
            List<Student> students = new List<Student>();
            students = db.GetStudents(classButtonLabel.Text);
            AttendanceTaker attendanceTaker = new AttendanceTaker();

            x = 0;
            if (classButtonLabel.Text == "SE")
                y = t1.SubjectSE - 1;
            else if (classButtonLabel.Text == "TE")
                y = t1.SubjectTE - 1;
            else
                y = t1.SubjectBE - 1;
            z = classButtonLabel.Text;

            stdnt.Clear();
            foreach (var person in students)
                stdnt.Add(Classes.Converter.StudentToStdnt(person));

            attendanceTaker.markedRollNoLabel.Content = "-";
            attendanceTaker.markedNameLabel.Content = "--";
            attendanceTaker.markedPresentLabel.Content = "-";
            attendanceTaker.markedAbsentLabel.Content = "-";

            attendanceTaker.rollNoLabel.Content = stdnt[x].Id;
            attendanceTaker.nameLabel.Content = stdnt[x].FirstName + " " + stdnt[x].LastName;
            attendanceTaker.presentLabel.Content = stdnt[x].Present[y];
            attendanceTaker.absentLabel.Content = stdnt[x].Absent[y];

            stk.Children.Add(attendanceTaker);


            //Stdnt s = Classes.Converter.StudentToStdnt(person);
            //attendanceTaker.rollNoLabel.Content = s.Id;
            //attendanceTaker.nameLabel.Content = s.FirstName + " " + s.LastName;
            //attendanceTaker.attendanceLabel.Content = s.Present[i];
        }

        private void EditStudents_Click(object sender, RoutedEventArgs e)
        {
            stk.Children.Clear();
            stk.Children.Add(Classes.Display.DisplayStudentEditorHeader(classButtonLabel.Text));
            List<Student> students = db.GetStudents(classButtonLabel.Text);

            foreach (var person in students)
                stk.Children.Add(Classes.Display.DisplayStudentEditor(Classes.Converter.StudentToStdnt(person)));
        }
    }
}