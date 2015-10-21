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

        private Button previous_time_button;

        public TimePicker()
        {
            InitializeComponent();

            previous_time_button = Time_btn_0;
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

        private void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform time_selector_rotation = new RotateTransform();

            Button temp_button = (Button)sender;

            if (temp_button == previous_time_button)
                return;

            if (temp_button != null)
            {
                // Set the angle of the time selector. 
                // Get the angle from the button name.
                String[] button_name_parts = temp_button.Name.Split('_');

                double temp_angle = 0;
                double.TryParse(button_name_parts[2], out temp_angle);

                time_selector_rotation.Angle = temp_angle;
                TimeSelector.RenderTransform = time_selector_rotation;

                // Set the color of the selected time button's text to the selected color.
                temp_button.Foreground = Brushes.White;

                // Set the color of the previous time button's text to default.                
                previous_time_button.Foreground = Brushes.Black;
                previous_time_button = temp_button;
            }
        }
    }
}
