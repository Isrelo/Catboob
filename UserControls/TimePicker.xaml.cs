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

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl
    {
        public delegate void SavePickedTime(TimeSpan duration_to_use);
        public SavePickedTime savePickedTime;

        public delegate void ClosePickedTime();
        public ClosePickedTime closePickedTime;

        public TimePicker()
        {
            InitializeComponent();
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            closePickedTime();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Days, Hours, Minutes, Seconds, Milliseconds
            TimeSpan duration_to_use = new TimeSpan(0, 0, 0, 3, 0);

            savePickedTime(duration_to_use);
        }
    }
}
