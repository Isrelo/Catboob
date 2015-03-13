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
using System.Drawing;
using System.IO;
using Microsoft.Win32;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String pressed_hotkey;
        private String working_dir;
        private HotkeyManager hotkey_manager;
        private BindingList<OverlayItem> overlay_items;

        public BindingList<OverlayItem> OverlayItems 
        { 
            get{return overlay_items;} 
            set{overlay_items = value;}
        }

        public MainWindow()
        {
            InitializeComponent();
            
            // Bind to the main application to receive events.
            this.DataContext = this;

            overlay_items = new BindingList<OverlayItem>();

            // Get Applicaion Directory
            working_dir = Directory.GetCurrentDirectory();

            hotkey_manager = new HotkeyManager();

            //TODO: Replace with config file.
            OverlayItem temp_item;

            // Add GGButton
            temp_item = new OverlayItem();
            temp_item.HotKey = "GGButton";
            temp_item.ImagePath = working_dir + "\\Images\\GGButton.png";
            OverlayItems.Add(temp_item);

            // Add EasyButton
            temp_item = new OverlayItem();
            temp_item.HotKey = "EasyButton";
            temp_item.ImagePath = working_dir + "\\Images\\EasyButton.jpg";
            OverlayItems.Add(temp_item);
        }

        private void DisplayStartingScreen()
        {
            // Change the title.
            AppTitle_txt.Text = "Catboob";

            // Show the starting screen.
            main_container.Visibility = System.Windows.Visibility.Visible;

            // Hide the add item container.
            add_overlay_item_container.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the back navigation button.
            if (Back_btn.Visibility == System.Windows.Visibility.Visible)
                Back_btn.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the cancel button.
            if (Cancel_btn.Visibility == System.Windows.Visibility.Visible)
                Cancel_btn.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the save button.
            if (Save_btn.Visibility == System.Windows.Visibility.Visible)
                Save_btn.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DisplayAddItemScreen()
        {
            // Change the title.
            AppTitle_txt.Text = "Add Item";

            // Hide the starting screen.
            main_container.Visibility = System.Windows.Visibility.Collapsed;

            // Show the add item container.
            add_overlay_item_container.Visibility = System.Windows.Visibility.Visible;

            // Show the cancel button.
            Cancel_btn.Visibility = System.Windows.Visibility.Visible;

            // Show the save item button.
            Save_btn.Visibility = System.Windows.Visibility.Visible;

            // Hide the hotkey diallog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DisplayHotkeyDialog()
        {
            // Show the hotkey dialog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Visible;

            hotkey_manager = new HotkeyManager();
            pressed_key_tb.Text = "";
            pressed_key_tb.Focus();
        }

        private void ResetAddItemForm()
        {
            // Reset add item form.
            hotkey_tb.Text = "";
            image_path_tb.Text = "";
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddItemScreen();
            ResetAddItemForm();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayStartingScreen();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayStartingScreen();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            OverlayItem temp_item = new OverlayItem();

            // Get the hotkey to assign.
            if (!String.IsNullOrEmpty(hotkey_tb.Text))
                temp_item.HotKey = hotkey_tb.Text;
            
            // Get the selected image path.
            if(!String.IsNullOrEmpty(image_path_tb.Text))
                temp_item.ImagePath = image_path_tb.Text;

            OverlayItems.Add(temp_item);

            DisplayStartingScreen();
        }

        private void ImagePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            if (open_file_dialog.ShowDialog() == true)
                image_path_tb.Text = open_file_dialog.FileName;
        }

        private void HotKey_Click(object sender, RoutedEventArgs e)
        {
            DisplayHotkeyDialog();
        }

        private void HotkeyDiscard_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddItemScreen();
        }

        private void HotkeySave_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddItemScreen();

            // Save the enterted hotkey.
            String tempHotkey = pressed_key_tb.Text;
            if(!String.IsNullOrEmpty(tempHotkey))
                hotkey_tb.Text = tempHotkey;
        }

        private void Hotkey_KeyUp(object sender, KeyEventArgs e)
        {
            // Remove key from hotkey manager.
            hotkey_manager.RemovePressedKey(e.Key.ToString());                
        }

        private void Hotkey_Down(object sender, KeyEventArgs e)
        {
            // Add pressed keys to the hotkey manager.
            hotkey_manager.AddPressedKey(e.Key.ToString());

            // Display the list of key in a hotkey.
            pressed_key_tb.Text = hotkey_manager.GetPressedKeysString();
        }

        private void Hotkey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Ignore repeated keys.
            if (e.IsRepeat)
                e.Handled = true;
        }
    }
}
