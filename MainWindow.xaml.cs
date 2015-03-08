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

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String working_dir;
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

            // Show the back navigation button.
            Cancel_btn.Visibility = System.Windows.Visibility.Visible;

            // Show the save item button.
            Save_btn.Visibility = System.Windows.Visibility.Visible;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            DisplayAddItemScreen();
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
            // Testing Items
            OverlayItem temp_item;

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F5";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";
            OverlayItems.Add(temp_item);

            DisplayStartingScreen();
        }
    }
}
