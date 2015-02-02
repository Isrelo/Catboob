using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Catboob_GGStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string working_dir;
        private bool is_exiting;
        private List<Action> action_items;

        public MainWindow()
        {
            InitializeComponent();

            // Setup the main action list.
            action_items = new List<Action>();
            actions_listview.ItemsSource = action_items; 

            // Get Applicaion Directory
            working_dir = Directory.GetCurrentDirectory();

            // Keep Applicaion Open
            is_exiting = false;

            // Setup Layout Images            
            menu_more_button.Source = new BitmapImage(new Uri(working_dir + "\\Images\\ic_more_vert_white_48dp.png"));
            add_action_button.Source = new BitmapImage(new Uri(working_dir + "\\Images\\ic_add_circle_orange_48dp.png"));

            //TODO: Load Data From XML File.
            String temp_img_path = working_dir + "\\Images\\GGButton.png";
            String temp_short_cut = "Ctrl + F4";
            AddAction(temp_img_path, temp_short_cut);

            temp_img_path = working_dir + "\\Images\\100_0105.jpg";
            temp_short_cut = "Ctrl + F3";
            AddAction(temp_img_path, temp_short_cut);

            temp_img_path = working_dir + "\\Images\\EasyButton.jpg";
            temp_short_cut = "Ctrl + F2";
            AddAction(temp_img_path, temp_short_cut);

            temp_img_path = working_dir + "\\Images\\GGButton.png";
            temp_short_cut = "Ctrl + F1";
            AddAction(temp_img_path, temp_short_cut);

            // Display Main Container
            DisplayDefaultPanel();           

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
            ni.Icon = new System.Drawing.Icon(working_dir + "\\Main.ico");
            ni.Visible = true;
            ni.ContextMenu = tray_menu;
            ni.DoubleClick += tray_double_click;

        }

        private void AddAction(String image_path, String short_cut)
        {
            Action temp_action = new Action();
            temp_action.ImageSource = image_path;
            temp_action.ShortCut = short_cut;
            temp_action.MoreImageSource = working_dir + "\\Images\\ic_more_vert_black_48dp.png";

            if (!FindAction(temp_action))
            {
                temp_action.ActionID = action_items.Count + 1;
                action_items.Add(temp_action);
            }            
        }

        private bool FindAction(Action action_to_find)
        {
            for (int count = 0; count < action_items.Count; count++)
            {
                Action temp_action = action_items[count];

                if (temp_action.ImageSource == action_to_find.ImageSource && temp_action.ShortCut == action_to_find.ShortCut)
                    return true;
            }

            return false;
        }

        private void Minimize_Applicatoin()
        {
            this.Show();
            this.Activate();
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

        private void Close_Application(object sender, EventArgs args)
        {
            is_exiting = true;
            this.Close();
        }

        // Final Close Event
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!is_exiting)
                e.Cancel = true;

            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void AddNewAction_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddActionPanel();
        }

        private void AppMenu_Clicked(object sender, RoutedEventArgs e)
        {
            if (app_title.Text == "Catboob")
            {
                DisplayAddActionPanel();
            }

            if (app_title.Text == "Add New Action")
            {
                DisplayDefaultPanel();
            }
        }

        private void DisplayDefaultPanel()
        {
            // Set the application's title.
            app_title.Text = "Catboob";

            // Display Main Menu Button
            menu_button.Source = new BitmapImage(new Uri(working_dir + "\\Images\\ic_menu_white_48dp.png"));

            main_container.Visibility = System.Windows.Visibility.Visible;
            add_action_container.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DisplayAddActionPanel()
        {
            // Set the application's title.
            app_title.Text = "Add New Action";

            // Display Back Button
            menu_button.Source = new BitmapImage(new Uri(working_dir + "\\Images\\ic_arrow_back_white_48dp.png"));

            main_container.Visibility = System.Windows.Visibility.Collapsed;
            add_action_container.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
