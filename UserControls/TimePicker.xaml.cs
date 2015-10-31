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
        public delegate void SavePickedTime(String duration_to_use);
        public SavePickedTime savePickedTime;

        public delegate void ClosePickedTime();
        public ClosePickedTime closePickedTime;

        private Button previous_time_button;
        private TextBox current_editor;
        private SolidColorBrush action_color;

        public TimePicker()
        {
            InitializeComponent();

            // Store the orange action color.
            action_color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF9800"));

            // Store the first time button for later.
            previous_time_button = Time_btn_0;

            // Store the first time editor for later.
            current_editor = minute_selection;
            current_editor.Foreground = action_color;
        }

        public void SetControlTime(int minutes, int seconds)
        {
            minute_selection.Text = minutes.ToString("X2");
            secod_selection.Text = seconds.ToString("X2");
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            closePickedTime();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            String temp_time_duration = String.Format("{0}:{1}:00", minute_selection.Text, secod_selection.Text);

            savePickedTime(temp_time_duration);
        }

        private void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform time_selector_rotation = new RotateTransform();

            Button temp_button = (Button)sender;

            if (temp_button != null)
            {
                // Get the selected time.
                current_editor.Text = temp_button.Content.ToString();

                if (temp_button == previous_time_button)
                    return;

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

        private void Time_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            TextBox temp_text_box = (TextBox)sender;

            if (temp_text_box != null)
                current_editor = temp_text_box;
        }

        private void TimePicker_Loaded(object sender, RoutedEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox temp_text_box = (TextBox)sender;

            if (temp_text_box != null)
            {
                ChangeTextBoxFontColor(secod_selection);
                ChangeTextBoxFontColor(minute_selection);
            }
        }

        private void ChangeTextBoxFontColor(TextBox box_to_change_color)
        {
            if (box_to_change_color != null)
            {
                if (box_to_change_color.Foreground == Brushes.White)
                    box_to_change_color.Foreground = action_color;
                else
                    box_to_change_color.Foreground = Brushes.White;
            }
        }
    }
}
