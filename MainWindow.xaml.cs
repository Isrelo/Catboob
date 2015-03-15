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
        private bool is_app_exiting;
        private String working_dir;
        private SoundManager sound_manager;
        private HotkeyManager hotkey_manager;
        private BindingList<OverlayItem> overlay_items;
        private KeyboardListener global_keyboard_listner;

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

            // Setup Bound Display List
            overlay_items = new BindingList<OverlayItem>();

            // Get Applicaion Directory
            working_dir = Directory.GetCurrentDirectory();

            // Keep Applicaion Open
            is_app_exiting = false;

            // Setup the global keyboard listner.
            global_keyboard_listner = new KeyboardListener();
            global_keyboard_listner.KeyDown += Global_Keyboard_KeyDown;
            global_keyboard_listner.KeyUp += Global_Keyboard_KeyUp;

            // Setup the user selected hotkey.
            hotkey_manager = new HotkeyManager();

            // Setup sound manager;
            sound_manager = new SoundManager();

            // Setup the system tray icon.
            SetupSystemTray();

            //TODO: Replace with config file.
            OverlayItem temp_item;

            // Add GGButton
            temp_item = new OverlayItem();
            temp_item.HotKey = "GGButton";
            temp_item.ImagePath = working_dir + "\\Images\\GGButton.png";
            temp_item.SoundPath = working_dir + "\\Sounds\\gg.mp3";
            OverlayItems.Add(temp_item);

            // Add EasyButton
            temp_item = new OverlayItem();
            temp_item.HotKey = "EasyButton";
            temp_item.ImagePath = working_dir + "\\Images\\EasyButton.jpg";
            temp_item.SoundPath = working_dir + "\\Sounds\\that_was_easy.mp3";
            OverlayItems.Add(temp_item);
        }

        private void SetupSystemTray()
        {
            // System Tray
            System.Windows.Forms.ContextMenu tray_menu = new System.Windows.Forms.ContextMenu();

            System.Windows.Forms.MenuItem maximize_menu_item = new System.Windows.Forms.MenuItem();
            maximize_menu_item.Text = "Settings";
            maximize_menu_item.Click += SystemTrayIcon_Settings_Click;
            tray_menu.MenuItems.Add(maximize_menu_item);

            System.Windows.Forms.MenuItem exit_menu_item = new System.Windows.Forms.MenuItem();
            exit_menu_item.Text = "Exit";
            exit_menu_item.Click += Close_Application;
            tray_menu.MenuItems.Add(exit_menu_item);

            EventHandler tray_double_click = new EventHandler(SystemTrayIcon_Double_Click);
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(working_dir + "\\Catboob.ico");
            ni.Visible = true;
            ni.ContextMenu = tray_menu;
            ni.DoubleClick += tray_double_click;
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

        private bool IsHotKeyDialogVisible()
        {
            if (add_overlay_item_container.Visibility == System.Windows.Visibility.Visible)
                return true;

            return false;
        }

        private void ResetAddItemForm()
        {
            // Reset Hotkey
            hotkey_tb.Text = "";

            // Reset Image Path
            image_path_tb.Text = "";

            // Reset Sound Path
            sound_path_tb.Text = "";
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Show the add item screen.
            DisplayAddItemScreen();

            // Reset the add item form.
            ResetAddItemForm();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the home screen.
            DisplayStartingScreen();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the home screen.
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

            // Add user overlay item.
            OverlayItems.Add(temp_item);

            // Show the home screen.
            DisplayStartingScreen();
        }

        private void ImagePath_Click(object sender, RoutedEventArgs e)
        {
            // Get the users chosen image.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Image";
            open_file_dialog.Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;|All Files (*.*)|*.*"; 
            if (open_file_dialog.ShowDialog() == true)
                image_path_tb.Text = open_file_dialog.FileName;
        }

        private void SoundPath_Click(object sender, RoutedEventArgs e)
        {
            // Get the users chosen sound.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Sound";
            open_file_dialog.Filter = "Sound Files  (*.mp3, *.wav)|*.mp3; *.wav;|All Files (*.*)|*.*";
            if (open_file_dialog.ShowDialog() == true)
                sound_path_tb.Text = open_file_dialog.FileName;
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

        private void Global_Keyboard_KeyDown(object sender, RawKeyEventArgs raw_key_event_args)
        {
            if (IsHotKeyDialogVisible())
            {
                // Add pressed keys to the hotkey manager.
                hotkey_manager.AddPressedKey(raw_key_event_args.Key.ToString());

                // Display the list of key in a hotkey.
                pressed_key_tb.Text = hotkey_manager.GetPressedKeysString();
            }
        }

        private void Global_Keyboard_KeyUp(object sender, RawKeyEventArgs raw_key_event_args)
        {
            if (IsHotKeyDialogVisible())
            {
                // Remove key from hotkey manager.
                hotkey_manager.RemovePressedKey(raw_key_event_args.Key.ToString());
            }
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            sound_manager.PlaySound(tempOverlayItem.SoundPath);
        }

        private void StopSound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempResult = (OverlayItem)tempButton.DataContext;
        }

        private void Minimize_Applicatoin()
        {
            this.Show();
            this.Activate();
            this.BringIntoView();
            this.WindowState = WindowState.Normal;
        }

        private void SystemTrayIcon_Settings_Click(object sender, EventArgs args)
        {
            Minimize_Applicatoin();
        }

        private void SystemTrayIcon_Double_Click(object sender, EventArgs args)
        {
            Minimize_Applicatoin();
        }

        // Application Exit Point
        private void Close_Application(object sender, EventArgs args)
        {
            is_app_exiting = true;

            // Clean up after the keyboard listner.
            global_keyboard_listner.Dispose();

            this.Close();
        }

        // Final Close Event
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //TODO: Uncomment after debugging.
            //if (!is_app_exiting)
            //    e.Cancel = true;
            
            //// Hide the application don't close it.
            //this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
