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

using System.ComponentModel;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        private WindowUserSettings window_user_settings;

        public bool WindowsIsCloseing { get; set; }

        public OverlayWindow()
        {
            InitializeComponent();

            WindowsIsCloseing = false;

            window_user_settings = new WindowUserSettings();
        }

        public void DisplayOverlay(String image_path)
        {
            // Restore the window to previous state if minimized.
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = window_user_settings.WindowState;
                this.Topmost = false;
            }

            overlay_display.Visibility = System.Windows.Visibility.Visible;
            overlay_display.Source = new BitmapImage(new Uri(image_path, UriKind.RelativeOrAbsolute));
        }

        public void HideOverlay()
        {
            overlay_display.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Save the last known position of the OverlayWindow.
            window_user_settings.WindowHeight = this.Height;
            window_user_settings.WindowWidth = this.Width;
            window_user_settings.WindowTop = this.Top;
            window_user_settings.WindowLeft = this.Left;
            window_user_settings.WindowState = this.WindowState;

            window_user_settings.SaveWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the last know position of the OverlayWindow.
            this.Height = window_user_settings.WindowHeight;
            this.Width = window_user_settings.WindowWidth;
            this.Top = window_user_settings.WindowTop;
            this.Left = window_user_settings.WindowLeft;
            this.WindowState = window_user_settings.WindowState;
        }

        private void OverlayWindow_StateChanged(object sender, EventArgs e)
        {
            switch(this.WindowState)
            {
                case WindowState.Maximized:
                    window_user_settings.WindowState = this.WindowState;
                    break;
                case WindowState.Normal:
                    window_user_settings.WindowState = this.WindowState;
                    break;
            }
        }

        // Final Close Event
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!WindowsIsCloseing)
                e.Cancel = true;

            //MessageBox.Show("Overlay can not be closed manually.");
        }
    }
}
