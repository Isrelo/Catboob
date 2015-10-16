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

using System.ComponentModel;
using Microsoft.Win32;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for AddOverlayItem.xaml
    /// </summary>
    public partial class AddOverlayItem : UserControl
    {
        public delegate void ShowHotkeyDialog();
        public ShowHotkeyDialog showHotkeyDialog;

        public delegate void ShowTimePickerDialog();
        public ShowTimePickerDialog showTimePickerDialog;

        public AddOverlayItem()
        {
            InitializeComponent();
        }

        public void ResetAddItemForm()
        {
            // Reset
            this.DataContext = new OverlayItem();
        }

        private void ImagePath_Click(object sender, RoutedEventArgs e)
        {
            String image_path = "";
            // Get the users chosen image.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Image";
            open_file_dialog.Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;|All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
                image_path = open_file_dialog.FileName;

            OverlayItem temp_item = (OverlayItem)this.DataContext;
            temp_item.ImagePath = image_path;
        }

        private void SoundPath_Click(object sender, RoutedEventArgs e)
        {
            String sound_path = "";
            // Get the users chosen sound.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Sound";
            open_file_dialog.Filter = "Sound Files  (*.mp3, *.wav)|*.mp3; *.wav;|All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
                sound_path = open_file_dialog.FileName;

            OverlayItem temp_item = (OverlayItem)this.DataContext;
            temp_item.SoundPath = sound_path;
        }

        private void DisplayDuration_Click(object sender, RoutedEventArgs e)
        {
            showTimePickerDialog();
        }

        private void HotKey_Click(object sender, RoutedEventArgs e)
        {
            showHotkeyDialog();
        }

        private void VolumeImageToDisplay(double slider_value)
        {
            if (slider_value <= 0)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Visible;
                VolumeDown_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeUp_img.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (slider_value > 0 && slider_value <= 0.5)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeDown_img.Visibility = System.Windows.Visibility.Visible;
                VolumeUp_img.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (slider_value > 0.5 && slider_value < 1.0)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeDown_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeUp_img.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider volume_slider = (Slider)sender;
            VolumeImageToDisplay(volume_slider.Value);
        }
    }
}
