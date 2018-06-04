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
    /// <summary>
    /// Interaction logic for TeacherEditor.xaml
    /// </summary>
    public partial class TeacherEditor : UserControl
    {

        DataAccess db = new DataAccess();
        public TeacherEditor()
        {
            InitializeComponent();
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
            classTeacherLabel.Text = null;
        }
        private void SE_Click(object sender, RoutedEventArgs e)
        {
            classTeacherLabel.Text = "SE";
        }
        private void TE_Click(object sender, RoutedEventArgs e)
        {
            classTeacherLabel.Text = "TE";
        }
        private void BE_Click(object sender, RoutedEventArgs e)
        {
            classTeacherLabel.Text = "BE";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool isHod = false;
            if (hodLabel.IsChecked == true)
                isHod = true;
            if (classTeacherLabel.Text == "-Class-")
                classTeacherLabel.Text = null;
            db.UpdateUser(int.Parse(id.Text), idLabel.Text, firstNameLabel.Text, lastNameLabel.Text, db.GetNoBySubject("SE",seLabel.Text), db.GetNoBySubject("TE", teLabel.Text), db.GetNoBySubject("BE", beLabel.Text), isHod, classTeacherLabel.Text);
            TextBlock tx = new TextBlock();
            tx.Text = "DataBase updated";
            submitPanel.Children.Add(tx);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tx = new TextBlock();
            string salt = Classes.Functions.CreateSalt(10);
            string pswd = newPasswordBox.Text;
            string hashedSaltedPassword = Classes.Functions.GenerateSHA256Hash(pswd, salt);
            db.UpdatePassword(int.Parse(id.Text), salt, hashedSaltedPassword);
            tx.Text = "Password updated";
        }

        private void NewPasswordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBlock tx = new TextBlock();
            if (newPasswordBox.Text.Length > 1)
            {
                changePassword.IsEnabled = true;
                if (passwordPanel.Children.Count > 1)
                    passwordPanel.Children.RemoveAt(passwordPanel.Children.Count-1);
            }
            else
            {
                tx.Text = "Password too short";
                if (passwordPanel.Children.Count<2)
                    passwordPanel.Children.Add(tx);
                
            }
        }
    }
}
