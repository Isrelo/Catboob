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

using System.Windows.Media.Animation;
using System.ComponentModel;
using WpfAnimatedGif;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        public WindowUserSettings OverlayWindowUserSettings;

        public bool WindowsIsCloseing { get; set; }

        public OverlayWindow()
        {
            InitializeComponent();

            WindowsIsCloseing = false;

            OverlayWindowUserSettings = new WindowUserSettings();
        }

        public void SetBackgroundColor(Color background_color)
        {
            OverlayWindowUserSettings.WindowColor = background_color;
            this.Background = new SolidColorBrush(background_color);
        }

        public void SetWindowWidthHeight(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Color GetWindowColor()
        {
            return OverlayWindowUserSettings.WindowColor;
        }

        public void ResetOverlay()
        {
            overlay_display.Source = null;            
        }

        public void DisplayOverlay(String image_path)
        {
            // Restore the window to previous state if minimized.
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = OverlayWindowUserSettings.WindowState;
                this.Topmost = false;
            }

            overlay_display.Visibility = System.Windows.Visibility.Visible;
            ImageBehavior.SetAnimatedSource(overlay_display, new BitmapImage(new Uri(image_path, UriKind.RelativeOrAbsolute)));

            Storyboard stbFadeIn = (Storyboard)FindResource("FadeIn");

            //string temp_test = "00:30:00";
            //string[] temp_time_parts = temp_test.Split(':');
            //int minutes;
            //int.TryParse(temp_time_parts[0], out minutes);
            //int seconds;
            //int.TryParse(temp_time_parts[1], out seconds);
            //int miliseconds;
            //int.TryParse(temp_time_parts[2], out miliseconds);
            //TimeSpan temp_duration = new TimeSpan(0, 0, minutes, seconds, miliseconds);

            //stbFadeIn.Duration = new Duration(temp_duration);
            stbFadeIn.Begin();
        }

        public void HideOverlay()
        {
            Storyboard stbFadeOut = (Storyboard)FindResource("FadeOut");

            //string temp_test = "00:30:00";
            //string[] temp_time_parts = temp_test.Split(':');
            //int minutes;
            //int.TryParse(temp_time_parts[0], out minutes);
            //int seconds;
            //int.TryParse(temp_time_parts[1], out seconds);
            //int miliseconds;
            //int.TryParse(temp_time_parts[2], out miliseconds);
            //TimeSpan temp_duration = new TimeSpan(0, 0, minutes, seconds, miliseconds);

            //stbFadeOut.Duration = new Duration(temp_duration);
            stbFadeOut.Begin();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Save the last known position of the OverlayWindow.
            OverlayWindowUserSettings.WindowHeight = this.Height;
            OverlayWindowUserSettings.WindowWidth = this.Width;
            OverlayWindowUserSettings.WindowTop = this.Top;
            OverlayWindowUserSettings.WindowLeft = this.Left;
            OverlayWindowUserSettings.WindowState = this.WindowState;

            OverlayWindowUserSettings.SaveWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the last know position of the OverlayWindow.
            this.Height = OverlayWindowUserSettings.WindowHeight;
            this.Width = OverlayWindowUserSettings.WindowWidth;
            this.Top = OverlayWindowUserSettings.WindowTop;
            this.Left = OverlayWindowUserSettings.WindowLeft;
            this.WindowState = OverlayWindowUserSettings.WindowState;
            this.Background = new SolidColorBrush(OverlayWindowUserSettings.WindowColor);
        }

        private void OverlayWindow_StateChanged(object sender, EventArgs e)
        {
            switch(this.WindowState)
            {
                case WindowState.Maximized:
                    OverlayWindowUserSettings.WindowState = this.WindowState;
                    break;
                case WindowState.Normal:
                    OverlayWindowUserSettings.WindowState = this.WindowState;
                    break;
            }
        }

        // Final Close Event
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!WindowsIsCloseing)
                e.Cancel = true;
        }
    }
}
