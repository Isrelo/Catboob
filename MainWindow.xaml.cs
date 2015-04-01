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

using System.Diagnostics;
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
        private bool has_hotkey_been_pressed;
        private bool is_app_exiting;
        private String working_dir;
        private SoundManager sound_manager;
        private HotkeyManager hotkey_manager;        
        private KeyboardListener global_keyboard_listner;
        private SettingsManager settings_manager;
        private OverlayItem item_to_edit;
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

            // Setup Bound Display List
            overlay_items = new BindingList<OverlayItem>();

            // Get Applicaion Directory
            working_dir = Directory.GetCurrentDirectory();

            // Set navigation drawer open width.
            AppNavDrawer.SetDarwerWidth(this.Width - System.Convert.ToDouble(App_Container.RowDefinitions[0].Height.ToString()));

            // Keep Applicaion Open
            is_app_exiting = false;

            // Make sure no hot keys have been pressed yet.
            has_hotkey_been_pressed = false;

            // Setup the global keyboard listner.
            global_keyboard_listner = new KeyboardListener();
            global_keyboard_listner.KeyDown += Global_Keyboard_KeyDown;
            global_keyboard_listner.KeyUp += Global_Keyboard_KeyUp;

            // Setup the user selected hotkey.
            hotkey_manager = new HotkeyManager();

            // Setup sound manager;
            sound_manager = new SoundManager();
            sound_manager.SoundMediaPlayer.MediaEnded += StopAllSounds_MediaEnded;

            // Setup the system tray icon.
            SetupSystemTray();

            // Load saved overlay items.
            settings_manager = new SettingsManager(working_dir);
            settings_manager.LoadOverlayItems();
            for (int count = 0; count < settings_manager.OverlayItems.Count; count++)
            {
                AddOverlayItem(settings_manager.OverlayItems[count]);                
            }
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

            // Show the menu button.
            Menu_btn.Visibility = System.Windows.Visibility.Visible;

            // Hide the cancel button.
            if (Cancel_btn.Visibility == System.Windows.Visibility.Visible)
                Cancel_btn.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the save button.
            if (Save_btn.Visibility == System.Windows.Visibility.Visible)
                Save_btn.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DisplayItemScreen(String item_title)
        {
            // Change the title.
            AppTitle_txt.Text = item_title;

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

            // Hide the edit and delete buttons.
            RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the menu button.
            Menu_btn.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DisplayHotkeyDialog()
        {
            // Show the hotkey dialog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Visible;

            hotkey_manager = new HotkeyManager();
            pressed_key_tb.Text = "";
            pressed_key_tb.Focus();
        }

        private void DisplayPlay(OverlayItem overlay_item)
        {
            // Show the play button.
            overlay_item.PlayVisible = Visibility.Visible;

            // Hide the stop button.
            overlay_item.StopVisible = Visibility.Collapsed;
        }

        private void DisplayStop(OverlayItem overlay_item)
        {
            // Hide the play button.
            overlay_item.PlayVisible = Visibility.Collapsed;

            // Show the stop button.
            overlay_item.StopVisible = Visibility.Visible;
        }

        private bool IsHotKeyDialogVisible()
        {
            if (add_overlay_item_container.Visibility == System.Windows.Visibility.Visible)
                return true;

            return false;
        }

        private void SetEditItemForm(OverlayItem overlay_item)
        {
            // Edit Item
            item_to_edit = overlay_item;

            // Reset Hotkey
            hotkey_tb.Text = overlay_item.HotKey;

            // Reset Image Path
            image_path_tb.Text = overlay_item.ImagePath;

            // Reset Sound Path
            sound_path_tb.Text = overlay_item.SoundPath;            
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

        private void AddOverlayItem(OverlayItem overlay_item)
        {
            // Add the OverlayItem to the list of displayed items.
            OverlayItems.Add(overlay_item);

            // Show Play Button
            DisplayPlay(overlay_item);
        }

        private void ExecuteOverlayItem()
        {
            for (int count = 0; count < OverlayItems.Count; count++)
            {
                // Check to see if the user pressed a hotkey.
                OverlayItem temp_overlay_item = OverlayItems[count];
                if (hotkey_manager.CheckForPressedHotkey(temp_overlay_item.HotKey) && !has_hotkey_been_pressed)
                {
                    // Play Overlay Sound
                    sound_manager.PlaySound(temp_overlay_item.SoundPath);
                    
                    // Show stop button.
                    DisplayStop(temp_overlay_item);

                    // Prevent the hotkey from being detected on heald keys.
                    has_hotkey_been_pressed = true;

                    //TODO: Remove latter - debugging if a hotkey was pressed or not.
                    Debug.WriteLine(String.Format("Andy - Hotkey: {0}", temp_overlay_item.HotKey));

                    return;
                }
            }
        }

        private void SaveOverlayItem(OverlayItem overlay_item)
        {
            bool editing_item = false;

            if (overlay_item == null)
                overlay_item = new OverlayItem();
            else
                editing_item = true;

            // Get the OverlayItem values.
            overlay_item.HotKey = hotkey_tb.Text;
            overlay_item.ImagePath = image_path_tb.Text;
            overlay_item.SoundPath = sound_path_tb.Text;
            
            if (!editing_item)
            {
                // Save the overlay item to the xml file.
                settings_manager.SaveOverlayItem(overlay_item);

                // Add the new OverlayItem.
                AddOverlayItem(overlay_item);
            }
            else
            {
                // Resave the overlay list.
                settings_manager.SaveOverlayItems(OverlayItems);
            }

            // Show the home screen.
            DisplayStartingScreen();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Show the add item screen.
            DisplayItemScreen("Add Item");

            // Reset the add item form.
            ResetAddItemForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the home screen.
            DisplayStartingScreen();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppTitle_txt.Text == "Edit Item")
                SaveOverlayItem(item_to_edit);
            else
                SaveOverlayItem(null);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected OverlayItem.
            OverlayItem temp_overlay_item = (OverlayItem)overlay_lv.SelectedItem;

            // Remove OverlayItem
            OverlayItems.Remove(temp_overlay_item);

            // Resave the overlay list.
            settings_manager.SaveOverlayItems(OverlayItems);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected OverlayItem.
            OverlayItem temp_overlay_item = (OverlayItem)overlay_lv.SelectedItem;

            // Show the add item screen.
            DisplayItemScreen("Edit Item");

            // Populate edit form.
            SetEditItemForm(temp_overlay_item);
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
            if (AppTitle_txt.Text == "Add Item")
                DisplayItemScreen("Add Item");
            else
                DisplayItemScreen("Edit Item");
        }

        private void HotkeySave_Click(object sender, RoutedEventArgs e)
        {
            if (AppTitle_txt.Text == "Add Item")
                DisplayItemScreen("Add Item");
            else
                DisplayItemScreen("Edit Item");

            // Save the enterted hotkey.
            String tempHotkey = pressed_key_tb.Text;
            if(!String.IsNullOrEmpty(tempHotkey))
                hotkey_tb.Text = tempHotkey;
        }

        private void Global_Keyboard_KeyDown(object sender, RawKeyEventArgs raw_key_event_args)
        {
            // Add pressed keys to the hotkey manager.
            hotkey_manager.AddPressedKey(raw_key_event_args.Key.ToString());

            if (IsHotKeyDialogVisible())
            {
                // Display the list of key in a hotkey.
                pressed_key_tb.Text = hotkey_manager.GetPressedKeysString();
                return;
            }

            // Execute overlay Aations if user pressed hotkey.
            ExecuteOverlayItem();
        }

        private void Global_Keyboard_KeyUp(object sender, RawKeyEventArgs raw_key_event_args)
        {
            // Remove key from hotkey manager.
            hotkey_manager.RemovePressedKey(raw_key_event_args.Key.ToString());

            // Hotkey has no longer been pressed.
            has_hotkey_been_pressed = false;
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            // Start Paying the selected sound.
            sound_manager.PlaySound(tempOverlayItem.SoundPath);

            // Show stop button.
            DisplayStop(tempOverlayItem);
        }

        private void StopSound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            // Stop Playing the selected sound.
            sound_manager.StopSound();

            // Show play button.
            DisplayPlay(tempOverlayItem);
        }

        private void StopAllSounds_MediaEnded(object sender, EventArgs e)
        {
            // Stop Playing the selected sound.
            sound_manager.StopSound();

            for (int count = 0; count < OverlayItems.Count; count++)
            {
                DisplayPlay(OverlayItems[count]);
            }
        }

        private void Maximize_Applicatoin()
        {
            this.Show();
            this.Activate();
            this.BringIntoView();
            this.WindowState = WindowState.Normal;
        }

        private void SystemTrayIcon_Settings_Click(object sender, EventArgs args)
        {
            Maximize_Applicatoin();
        }

        private void SystemTrayIcon_Double_Click(object sender, EventArgs args)
        {
            Maximize_Applicatoin();
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
            if (!is_app_exiting)
                e.Cancel = true;

            // Hide the application don't close it.
            this.Hide();
        }

        private void OverlayItems_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mouse_pt = e.GetPosition(this);

            if (overlay_lv.SelectedItems.Count > 0)
                RightAction_sp.Visibility = System.Windows.Visibility.Visible;
            else
                RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OverlayItems_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clear all the selected items (Used to detect empty list click).
            overlay_lv.UnselectAll();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            AppNavDrawer.OpenDrawer();
        }
    }
}
