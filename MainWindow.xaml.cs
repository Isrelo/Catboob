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
using System.Windows.Interop;
using Microsoft.Win32;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool is_app_exiting;
        private IntPtr hwnd_handle;
        private HwndSource hwnd_source;
        private String working_dir;
        private SoundManager sound_manager;
        private GlobalHotkeyListener global_hotkey_listner;
        private HotkeyManager hotkey_manager;        
        private SettingsManager settings_manager;
        private BindingList<OverlayItem> overlay_items;

        private OverlayWindow overlay_window;

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

            // Set ShowColorPickerDialog display event.
            AppNavDrawer.showColorPickerDialog = DisplayColorPickerDialog;

            // Set Discard Color event.
            color_picker.closePickColor = DiscardPickedColor;

            // Set Save Color event.
            color_picker.savePickedColor = SavePickedColor;

            // Set ShowHotkeyDialog display event.
            add_overlay_item_container.showHotkeyDialog = DisplayHotkeyDialog;

            // Keep Applicaion Open
            is_app_exiting = false;

            // Setup the user selected hotkey managment.
            hotkey_manager = new HotkeyManager();

            // Setup sound manager;
            sound_manager = new SoundManager();
            sound_manager.SoundMediaPlayer.MediaEnded += StopAllSounds_MediaEnded;

            // Setup the system tray icon.
            SetupSystemTray();

            overlay_window = new OverlayWindow();
            overlay_window.Show();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the current window handle.
            var window_interop_helper = new WindowInteropHelper(this);
            hwnd_handle = window_interop_helper.Handle;
            
            // Setup the global hotkey listner.
            global_hotkey_listner = new GlobalHotkeyListener(hwnd_handle);
            global_hotkey_listner.executeOverlayItem = ExecuteOverlayItem;

            // Add the hotkey listner.
            hwnd_source = HwndSource.FromHwnd(hwnd_handle);
            hwnd_source.AddHook(global_hotkey_listner.HwndHook);            

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

            // Hide the color picker dialog.
            color_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the hotkey diallog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the edit and delete buttons.
            RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the menu button.
            Menu_btn.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DiscardPickedColor()
        {
            // Hide the color picker dialog.
            color_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the navigation drawer.
            AppNavDrawer.CloseDrawer();
        }

        private void SavePickedColor()
        {
            // Hide the color picker dialog.
            color_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Set the OverlayWindow background color.
            overlay_window.SetBackgroundColor(color_picker.BackgroundColor);

            // Hide the navigation drawer.
            AppNavDrawer.CloseDrawer();
        }

        private void DisplayColorPickerDialog()
        {
            // Show the color picker dialog.
            color_picker_dialog.Visibility = System.Windows.Visibility.Visible;

            color_picker.SetControlColor(overlay_window.OverlayWindowUserSettings.WindowColor);
        }

        private void DisplayHotkeyDialog()
        {
            // Show the hotkey dialog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Visible;

            // Reset the hotkey dialog.
            hotkey_manager.Reset();
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

        private void AddOverlayItem(OverlayItem overlay_item)
        {
            // Register the hotkey with the global listner.
            global_hotkey_listner.RegisterGlobalHotkey(overlay_item);

            // Add the OverlayItem to the list of displayed items.
            OverlayItems.Add(overlay_item);            

            // Show Play Button
            DisplayPlay(overlay_item);
        }

        private void ExecuteOverlayItem(int hotkey_id)
        {
            // Check to see if the user pressed a hotkey.                
            foreach (OverlayItem temp_overlay_item in OverlayItems)
            {
                if (temp_overlay_item.HotKeyID == hotkey_id)
                {
                    if (File.Exists(temp_overlay_item.ImagePath))
                    {
                        Debug.WriteLine("ImagePath: " + temp_overlay_item.ImagePath);

                        // Display the selected image.
                        overlay_window.DisplayOverlay(temp_overlay_item.ImagePath);
                    }

                    if (File.Exists(temp_overlay_item.SoundPath))
                    {
                        Debug.WriteLine("SoundVolume: " + temp_overlay_item.SoundVolume);

                        // Set the soud's volume level.
                        sound_manager.SetVolume(temp_overlay_item.SoundVolume);

                        // Play Overlay Sound
                        sound_manager.PlaySound(temp_overlay_item.SoundPath);

                        // Show stop button.
                        DisplayStop(temp_overlay_item);
                    }
                }
            }
        }

        private void SaveOverlayItem(OverlayItem overlay_item, bool editing_item)
        {
            if (!editing_item)
            {
                OverlayItem temp_item = new OverlayItem();
                temp_item.HotKey = overlay_item.HotKey;
                temp_item.ImagePath = overlay_item.ImagePath;
                temp_item.PlayVisible = overlay_item.PlayVisible;
                temp_item.SoundPath = overlay_item.SoundPath;
                temp_item.SoundVolume = overlay_item.SoundVolume;
                temp_item.StopVisible = overlay_item.StopVisible;

                // Save the overlay item to the xml file.
                settings_manager.SaveOverlayItem(temp_item);

                // Add the new OverlayItem.
                AddOverlayItem(temp_item);
            }
            else
            {
                // Remove previous hotkey bindings.
                global_hotkey_listner.UnRegisterGlobalHotkeys();

                // Resave the overlay list.
                settings_manager.SaveOverlayItems(OverlayItems);

                // Rebind hotkeys.
                for (int count = 0; count < OverlayItems.Count; count++)
                {
                    OverlayItem temp_item = OverlayItems[count];

                    global_hotkey_listner.RegisterGlobalHotkey(temp_item);
                }
            }

            // Show the home screen.
            DisplayStartingScreen();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Reset the add item form.
            ResetAddItemForm();

            // Show the add item screen.
            DisplayItemScreen("Add Item");
        }

        private void ResetAddItemForm()
        {
            add_overlay_item_container.ResetAddItemForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the home screen.
            DisplayStartingScreen();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppTitle_txt.Text == "Edit Item")
                SaveOverlayItem((OverlayItem)add_overlay_item_container.DataContext, true);
            else
                SaveOverlayItem((OverlayItem)add_overlay_item_container.DataContext, false);
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

            add_overlay_item_container.DataContext = overlay_lv.SelectedItem;

            // Show the add item screen.
            DisplayItemScreen("Edit Item");
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
            if (!String.IsNullOrEmpty(tempHotkey))
            {
                OverlayItem temp_item = (OverlayItem)add_overlay_item_container.DataContext;
                temp_item.HotKey = tempHotkey;
            }
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            // Set sound volume.
            sound_manager.SetVolume(tempOverlayItem.SoundVolume);

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

            overlay_window.HideOverlay();

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
            this.ShowInTaskbar = true;
        }

        private void SystemTrayIcon_Settings_Click(object sender, EventArgs args)
        {
            Maximize_Applicatoin();
        }

        private void SystemTrayIcon_Double_Click(object sender, EventArgs args)
        {
            Maximize_Applicatoin();
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

        private void Applicatoin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Set navigation drawer open width.
            AppNavDrawer.SetDarwerWidth(this.Width - System.Convert.ToDouble(App_Container.RowDefinitions[0].Height.ToString()));
        }

        private void HotKey_Click(object sender, RoutedEventArgs e)
        {
            DisplayHotkeyDialog();
        }

        private void HotKey_KeyDown(object sender, KeyEventArgs e)
        {
            // Add pressed keys to the hotkey manager.
            hotkey_manager.AddPressedKey(e.Key.ToString());

            // Display the list of key in a hotkey.
            pressed_key_tb.Text = hotkey_manager.GetPressedKeysString();
        }

        private void HotKey_KeyUP(object sender, KeyEventArgs e)
        {
            // Remove key from hotkey manager.
            hotkey_manager.RemovePressedKey(e.Key.ToString());
        }

        // Application Exit Point
        private void Close_Application(object sender, EventArgs args)
        {
            // Signal that the progam is closeing.
            is_app_exiting = true;

            // Allow the overlay window to close.
            overlay_window.WindowsIsCloseing = true;
            overlay_window.Close();

            // Cleanup registerd hotkeys.
            global_hotkey_listner.UnRegisterGlobalHotkeys();

            // Cleanup the application hook.
            hwnd_source.RemoveHook(global_hotkey_listner.HwndHook);
            hwnd_source = null;

            this.Close();
        }

        // Final Close Event
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Only close the application from the exit right click menu.
            if (!is_app_exiting)
                e.Cancel = true;

            // Hide the application don't close it.
            this.Hide();
        }
    }
}
