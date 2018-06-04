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
    /// Interaction logic for StudentLabel.xaml
    /// </summary>
    public partial class StudentLabel : UserControl
    {
        private static StudentLabel _instance;
        public static StudentLabel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StudentLabel();
                return _instance;

            }
        }
        public StudentLabel()
        {
            InitializeComponent();
        }
    }
}
