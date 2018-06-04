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

namespace Attendence_Counter_ALPHA
{
    public partial class AttendanceTaker : UserControl
    {
        public bool isWaiting = true;
        DataAccess db = new DataAccess();
        public AttendanceTaker()
        {
            InitializeComponent();
        }

        public void PresentButton_Click(object sender, RoutedEventArgs e)
        {
            markedRollNoLabel.Content = rollNoLabel.Content;
            markedNameLabel.Content = nameLabel.Content;
            markedPresentLabel.Content = (int.Parse(presentLabel.Content.ToString()) + 1);
            markedAbsentLabel.Content = absentLabel.Content.ToString();
            postLoginWindow.x++;
            db.UpdateAttendance(int.Parse(markedRollNoLabel.Content.ToString()), int.Parse(markedPresentLabel.Content.ToString()), int.Parse(markedAbsentLabel.Content.ToString()), postLoginWindow.y, postLoginWindow.z);

            UpdateAttendanceBox();
        }

        public void AbsentButton_Click(object sender, RoutedEventArgs e)
        {
            markedRollNoLabel.Content = rollNoLabel.Content;
            markedNameLabel.Content = nameLabel.Content;
            markedPresentLabel.Content = presentLabel.Content.ToString();
            markedAbsentLabel.Content = (int.Parse(absentLabel.Content.ToString()) + 1);
            postLoginWindow.x++;
            db.UpdateAttendance(int.Parse(markedRollNoLabel.Content.ToString()), int.Parse(markedPresentLabel.Content.ToString()), int.Parse(markedAbsentLabel.Content.ToString()), postLoginWindow.y, postLoginWindow.z);
            
            UpdateAttendanceBox();
            
        }
        public void UpdateAttendanceBox()
        {
            if (postLoginWindow.x < postLoginWindow.stdnt.Count())
            {
                rollNoLabel.Content = postLoginWindow.stdnt[postLoginWindow.x].Id;
                nameLabel.Content = postLoginWindow.stdnt[postLoginWindow.x].FirstName + " " + postLoginWindow.stdnt[postLoginWindow.x].LastName;
                presentLabel.Content = postLoginWindow.stdnt[postLoginWindow.x].Present[postLoginWindow.y];
                absentLabel.Content = postLoginWindow.stdnt[postLoginWindow.x].Absent[postLoginWindow.y];
            }
            else
            {
                rollNoLabel.Content = "-";
                nameLabel.Content = "--";
                presentLabel.Content = "--";
                absentLabel.Content = "-";

                presentButton.IsEnabled = false;
                absentButton.IsEnabled = false;
            }
            
        }
    }
}
