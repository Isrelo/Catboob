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

        public MainWindow()
        {
            InitializeComponent();

            // Get Applicaion Directory
            working_dir = Directory.GetCurrentDirectory();

            // Keep Applicaion Open
            is_exiting = false;

            // Actions
            List<Action> test_items = new List<Action>();

            Action test_action = new Action();
            test_action.ImageSource = working_dir + "\\Images\\GGButton.png";
            test_action.ShortCut = "Ctrl + F4";
            test_items.Add(test_action);

            test_action = new Action();
            test_action.ImageSource = working_dir + "\\Images\\GGButton.png";
            test_action.ShortCut = "Ctrl + F3";
            test_items.Add(test_action);

            test_action = new Action();
            test_action.ImageSource = working_dir + "\\Images\\GGButton.png";
            test_action.ShortCut = "Ctrl + F2";
            test_items.Add(test_action);

            test_action = new Action();
            test_action.ImageSource = working_dir + "\\Images\\GGButton.png";
            test_action.ShortCut = "Ctrl + F1";
            test_items.Add(test_action);

            actions_listview.ItemsSource = test_items; 

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
    }
}
