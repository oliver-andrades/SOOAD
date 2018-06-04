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
using Attendence_Counter_ALPHA.Classes;

namespace Attendence_Counter_ALPHA
{
    /// <summary>
    /// Interaction logic for addUserWindow.xaml
    /// </summary>
    public partial class addUserWindow : Window
    {
        public bool userIdOk = false, userNameOk = false; /*passwordOk = false*/


        public addUserWindow()
        {
            InitializeComponent();
            addUserButton.IsEnabled = false;
        }

        private void EnableSubmit()
        {
            if (userIdOk && userNameOk)
                addUserButton.IsEnabled = true;
            else
                addUserButton.IsEnabled = false;
        }

        private void NewIdBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string Id = newIdBox.Text;
            if (Verifier.IdTaken(Id))
            {
                teacherIdLabel.Content = "UserID taken";
                userIdOk = false;
            }
            else
            {
                teacherIdLabel.Content = "";
                userIdOk = true;
            }
            EnableSubmit();
        }
        

        private void NewNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (newFirstNameBox.Text == "" || newLastNameBox.Text == "")
            {
                nameErrorLabel.Content = "Please Enter Full Name";
                userNameOk = false;
            }
            else if (Verifier.SameNameExists(newFirstNameBox.Text, newLastNameBox.Text))
            {
                nameErrorLabel.Content = "User already Exists";
                userNameOk = false;
            }
            else
            {
                nameErrorLabel.Content = "";
                userNameOk = true;
            }
            EnableSubmit();
        }
        

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();
            string salt = Functions.CreateSalt(10);
            string pswd = newPasswordBox.Text;
            string hashedSaltedPassword = Classes.Functions.GenerateSHA256Hash(pswd, salt);
            bool isHod = false;
            if (isAdmin.IsChecked == true)
                isHod = true;
            if (classButtonLabel.Text == "-Class-")
                classButtonLabel.Text = null;
            db.AddUser(newIdBox.Text, newFirstNameBox.Text, newLastNameBox.Text, db.GetNoBySubject("SE", seSubjectBox.Text), db.GetNoBySubject("TE", teSubjectBox.Text), db.GetNoBySubject("BE", beSubjectBox.Text), salt, hashedSaltedPassword, isHod, classButtonLabel.Text);
            Close();
            
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }
        private void NA_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = null;
        }
        private void SE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "SE";
        }
        private void TE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "TE";
        }
        private void BE_Click(object sender, RoutedEventArgs e)
        {
            classButtonLabel.Text = "BE";
        }
        
    }
}
