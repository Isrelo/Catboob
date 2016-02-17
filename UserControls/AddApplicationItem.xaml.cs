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

using Microsoft.Win32;

namespace CatboobGGStream.UserControls
{
    /// <summary>
    /// Interaction logic for AddApplicationItem.xaml
    /// </summary>
    public partial class AddApplicationItem : UserControl
    {
        private String app_image_path;

        public AddApplicationItem()
        {
            InitializeComponent();
        }

        public void SetDialogTitle(String title_p)
        {
            AppTitle_txt.Text = title_p;
        }

        private void MenuCancel_Click(object sender, RoutedEventArgs e)
        {
            // Take the user back to the applicaiton list.
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO: User wants the changes made to be applied to the selected or added item.
            AppListBoxItem temp_list_box_item = (AppListBoxItem)this.DataContext;

            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AppFileDialog_Click(object sender, RoutedEventArgs e)
        {
            String app_path = "";
            // Get the users chosen application.
            OpenFileDialog open_file_dialog = new OpenFileDialog();
            open_file_dialog.Title = "Choose Executable";
            open_file_dialog.Filter = "Executable Files (*.exe) | *.exe;|All Files (*.*)|*.*";

            if (open_file_dialog.ShowDialog() == true)
                app_path = open_file_dialog.FileName;

            //TODO: Save the path for later reterival.
            //OverlayItem temp_item = (OverlayItem)this.DataContext;
            //temp_item.ImagePath = app_path;

            // Show the changed value.
            this.app_path_tb.Text = app_path;
        }
    }
}
