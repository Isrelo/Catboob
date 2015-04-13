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

using Microsoft.Win32;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for AddOverlayItem.xaml
    /// </summary>
    public partial class AddOverlayItem : UserControl
    {
        private String imagePath;
        private String soundPath;
        private String hotkey;

        public delegate void ShowHotkeyDialog();
        public ShowHotkeyDialog showHotkeyDialog;

        public double SoundVolume
        {
            get 
            {
                return Volume_ctrl.VolumeLevel;
            }

            set
            {
                Volume_ctrl.VolumeLevel = value;
            }
        }

        public String ImagePath 
        {
            get
            {
                return imagePath;
            }

            set
            {
                image_path_tb.Text = value;
                imagePath = value;
            }
        }

        public String SoundPath 
        {
            get
            {
                return soundPath;
            }

            set
            {
                sound_path_tb.Text = value;
                soundPath = value;
            }
        }

        public String Hotkey
        {
            get
            {
                return hotkey;
            }

            set
            {
                hotkey_tb.Text = value;
                hotkey = value;
            }
        }

        public AddOverlayItem()
        {
            InitializeComponent();
        }

        public void ResetAddItemForm()
        {
            // Reset Hotkey
            hotkey_tb.Text = "";

            // Reset Image Path
            image_path_tb.Text = "";

            // Reset Sound Path
            sound_path_tb.Text = "";

            // Reset Sound Volume
            Volume_ctrl.VolumeLevel = 0.5;
        }

        private void ImagePath_Click(object sender, RoutedEventArgs e)
        {
            // Get the users chosen image.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Image";
            open_file_dialog.Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;|All Files (*.*)|*.*";
            if (open_file_dialog.ShowDialog() == true)
                ImagePath = open_file_dialog.FileName;

            image_path_tb.Text = ImagePath;
        }

        private void SoundPath_Click(object sender, RoutedEventArgs e)
        {
            // Get the users chosen sound.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Sound";
            open_file_dialog.Filter = "Sound Files  (*.mp3, *.wav)|*.mp3; *.wav;|All Files (*.*)|*.*";
            if (open_file_dialog.ShowDialog() == true)
                SoundPath = open_file_dialog.FileName;

            sound_path_tb.Text = SoundPath;
        }

        private void HotKey_Click(object sender, RoutedEventArgs e)
        {
            showHotkeyDialog();
        }
    }
}
