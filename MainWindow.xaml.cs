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

            // Testing Items
            OverlayItem temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F5";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F6";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F7";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F8";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F9";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F10";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F11";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);

            temp_item = new OverlayItem();
            temp_item.HotKey = "CTRL + F12";
            temp_item.ImagePath = working_dir + "\\Images\\100_0105.jpg";

            OverlayItems.Add(temp_item);
        }
    }
}
