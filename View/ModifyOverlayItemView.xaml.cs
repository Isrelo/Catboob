using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using Microsoft.Win32;
using CatboobGGStream.ViewModel;

namespace CatboobGGStream.View
{
    /// <summary>
    /// Interaction logic for ModifyOverlayItemView.xaml
    /// </summary>
    public partial class ModifyOverlayItemView : UserControl
    {
        public ModifyOverlayItemView()
        {
            InitializeComponent();
        }

        private void ImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Cliked get image path button!");

            String temp_image_path = "";
            // Get the users chosen image.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Image";
            open_file_dialog.Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;|All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
                temp_image_path = open_file_dialog.FileName;

            this.image_path_tb.Text = temp_image_path;
        }

        private void SoundPathButton_Click(object sender, RoutedEventArgs e)
        {
            String temp_sound_path = "";
            // Get the users chosen sound.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Sound";
            open_file_dialog.Filter = "Sound Files  (*.mp3, *.wav)|*.mp3; *.wav;|All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
                temp_sound_path = open_file_dialog.FileName;

            this.sound_path_tb.Text = temp_sound_path;
        }

        private void ModifyOverlayItemToolbar_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ModifyOverlayItemViewModel temp_modify_overlay_item_view_model = (ModifyOverlayItemViewModel)this.DataContext;
                ModifyOverlayItemToolbar.DataContext = temp_modify_overlay_item_view_model.ModifyOverlayItemToolbarViewModel;
            }
        }
    }
}
