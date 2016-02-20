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
        public AppSettingsManager AppSettingsManager { get; set; }

        public AddApplicationItem()
        {
            InitializeComponent();
        }

        public void SetDialogTitle(String title_p)
        {
            AppTitle_txt.Text = title_p;
        }

        public void ResetFrom()
        {
            app_name_tb.Text = "";
            app_path_tb.Text = "";
            app_args_tb.Text = "";
        }

        public void PopulateForm(AppListBoxItem temp_app_list_box_item_p)
        {
            app_name_tb.Text = temp_app_list_box_item_p.AppTitle;
            app_path_tb.Text = temp_app_list_box_item_p.AppPath;
            app_args_tb.Text = temp_app_list_box_item_p.AppItemData.AppArgs;
        }

        private void MenuCancel_Click(object sender, RoutedEventArgs e)
        {
            // Take the user back to the applicaiton list.
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            // Apply the changes to the selected AppListBoxItem.
            AppListBoxItem temp_list_box_item = (AppListBoxItem)this.DataContext;
            temp_list_box_item.AppTitle = app_name_tb.Text;
            temp_list_box_item.AppPath = app_path_tb.Text;
            temp_list_box_item.AppIcon = Utility.GetImageFromAppIcon(app_path_tb.Text);
            temp_list_box_item.AppItemData.AppArgs = app_args_tb.Text;

            AppSettingsManager.SaveAppItems();

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

            // Show the changed value.
            this.app_path_tb.Text = app_path;
        }
    }
}
