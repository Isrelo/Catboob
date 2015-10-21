﻿using System;
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
using System.Windows.Media.Animation;
using System.Timers;
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
        private String app_data_dir;
        private SoundManager sound_manager;
        private GlobalHotkeyListener global_hotkey_listner;               
        private SettingsManager settings_manager;
        private EventTimer overlay_event_timer;
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

            // Get Applicaion Directory.
            working_dir = Directory.GetCurrentDirectory();

            // Get the %AppData% Directory.
            app_data_dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            app_data_dir += "\\CatboobGGStream";

            // Set navigation drawer open width.
            AppNavDrawer.SetDarwerWidth(this.Width - System.Convert.ToDouble(App_Container.RowDefinitions[0].Height.ToString()));

            // Setup ShowColorPickerDialog
            AppNavDrawer.showColorPickerDialog = DisplayColorPickerDialog;
            color_picker.closePickedColor = DiscardPickedColor;
            color_picker.savePickedColor = SavePickedColor;

            // Setup ShowResolutionPicker
            AppNavDrawer.showResolutionPickerDialog = DisplayResolutionPickerDialog;
            resolution_picker.closePickedResolution = DiscardPickedResolution;
            resolution_picker.savePickedResolution = SavePickedResolution;

            // Set ShowHotkeyDialog display event.
            add_overlay_item_container.showHotkeyDialog = DisplayHotkeyDialog;
            hotkey_picker.closePickedHotkey = DiscardHotkeyDialog;
            hotkey_picker.savePickedHotkey = SaveHotkeyDialog;

            // Set ShowTimePickerDialog display event.
            add_overlay_item_container.showTimePickerDialog = DisplayTimePickerDialog;
            time_picker.savePickedTime = SaveTimePicker;
            time_picker.closePickedTime = DiscardTimePicker;

            // Keep Applicaion Open
            is_app_exiting = false;

            // Setup the overlay display timer.
            overlay_event_timer = new EventTimer();

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
            settings_manager = new SettingsManager(app_data_dir);
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

            // Hide the resolution picker dialog.
            resolution_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the hotkey diallog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the TimePicker dialog.
            time_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

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

        private void DiscardPickedResolution()
        {
            // Hide the resolution picker dialog.
            resolution_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Hide the navigation drawer.
            AppNavDrawer.CloseDrawer();
        }

        private void SavePickedResolution(int width, int height)
        {
            // Hide the resolution picker dialog.
            resolution_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            overlay_window.SetWindowWidthHeight(width, height);

            // Hide the navigation drawer.
            AppNavDrawer.CloseDrawer();
        }

        private void DisplayResolutionPickerDialog()
        {
            // Show the resolution picker dialog.
            resolution_picker_dialog.Visibility = System.Windows.Visibility.Visible;
        }

        private void DisplayHotkeyDialog(String hotkey)
        {
            // Show the hotkey dialog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Visible;

            // Reset the hotkey dialog.
            hotkey_picker.ResetHotkeyDialog();
            hotkey_picker.SetControlHotkey(hotkey);

            Keyboard.Focus(hotkey_picker);
        }

        private void DiscardHotkeyDialog()
        {
            // Hide the hotkey dialog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SaveHotkeyDialog(String selected_hotkey)
        {
            // Hide the hotkey diallog.
            hotkey_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Save the enterted hotkey.
            if (!String.IsNullOrEmpty(selected_hotkey))
            {
                OverlayItem temp_item = (OverlayItem)add_overlay_item_container.DataContext;
                temp_item.HotKey = selected_hotkey;
            }
        }

        private void DiscardTimePicker()
        {
            // Hide the TimePicker dialog.
            time_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SaveTimePicker(TimeSpan duration_to_use)
        {
            // Hide the TimePicker dialog.
            time_picker_dialog.Visibility = System.Windows.Visibility.Collapsed;

            // Save the selected time.
            OverlayItem temp_item = (OverlayItem)add_overlay_item_container.DataContext;
            temp_item.DisplayDuration = duration_to_use;
        }

        private void DisplayTimePickerDialog()
        {
            // Show the hotkey dialog.
            time_picker_dialog.Visibility = System.Windows.Visibility.Visible;
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
                    if (temp_overlay_item.DisplayDuration.Seconds > 0)
                    {
                        overlay_event_timer.StartTimer(temp_overlay_item.DisplayDuration, OverlayItem_TimerElapsed);
                    }

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

        private void ResetOverlayItems()
        {
            // Hide the displayed overlay.
            overlay_window.HideOverlay();

            // Stop Playing the selected sound.
            sound_manager.StopSound();

            foreach (OverlayItem temp_overlay_item in OverlayItems)
            {
                // Show play button.
                DisplayPlay(temp_overlay_item);
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
                temp_item.DisplayDuration = overlay_item.DisplayDuration;

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

        private void PlaySound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            // Perfrom the same action as the pressed hotkey.
            ExecuteOverlayItem(tempOverlayItem.HotKeyID);
        }

        private void StopSound_Click(object sender, RoutedEventArgs e)
        {
            Button tempButton = (Button)sender;
            OverlayItem tempOverlayItem = (OverlayItem)tempButton.DataContext;

            // Perfrom the same action for reseting the overlay.
            ResetOverlayItems();
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
            DisplayHotkeyDialog("");
        }

        private void OverlayItem_TimerElapsed(Object source, EventArgs e)
        {
            // Hide the current OverlayItem.
            ResetOverlayItems();

            // Make sure the timer doesn't endlessly trigger.
            overlay_event_timer.StopTimer();
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
