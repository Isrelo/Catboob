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

using System.IO;
using System.ComponentModel;

namespace CatboobGGStream.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationItemList.xaml
    /// </summary>
    public partial class ApplicationItemList : UserControl
    {
        private BindingList<AppListBoxItem> applicatoin_items_l;
        private AppSettingsManager app_settings_manager_l;

        private BindingList<AppListBoxItem> ApplicatoinItems
        {
            get { return applicatoin_items_l; }
            set { applicatoin_items_l = value; }
        }

        public ApplicationItemList()
        {
            InitializeComponent();

            applicatoin_items_l = new BindingList<AppListBoxItem>();

            // Set the itemsource to the binding list in order to have items update in the listbox.
            application_lv.ItemsSource = ApplicatoinItems;
        }

        public void LoadApplications(String working_dir_p)
        {
            app_settings_manager_l = new AppSettingsManager(working_dir_p, ApplicatoinItems);
            app_settings_manager_l.LoadAppItems();

            for (int count = 0; count < app_settings_manager_l.AppItems.Count; count++)
            {
                AppItem temp_app_item = app_settings_manager_l.AppItems[count];

                AddApplicationItemToList(temp_app_item);                
            }

            foreach(AppListBoxItem app_list_box_item in ApplicatoinItems)
            { 
                LaunchApplication(app_list_box_item.AppPath, app_list_box_item.AppItemData.AppArgs);
            }
        }

        private void AddApplicationItemToList(AppItem item_to_add_p)
        {
            // Add a new application to the startup list.
            AppListBoxItem temp_app_list_box_item = new AppListBoxItem();
            temp_app_list_box_item.AppTitle = item_to_add_p.AppTitle;
            temp_app_list_box_item.AppPath = item_to_add_p.AppPath;
            temp_app_list_box_item.AppIcon = Utility.GetImageFromAppIcon(item_to_add_p.AppPath);
            temp_app_list_box_item.AppItemData = item_to_add_p;

            applicatoin_items_l.Add(temp_app_list_box_item);            
        }

        private void LaunchApplication(String app_path_p, String app_args_p)
        {
            Utility.ExecuteCommand(app_path_p, app_args_p);
        }

        private void ApplicationItems_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mouse_pt = e.GetPosition(this);

            // Show and hide the edit and delete buttons.
            if (application_lv.SelectedItems.Count > 0)
                RightAction_sp.Visibility = System.Windows.Visibility.Visible;
            else
                RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ApplicationItems_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clear all the selected ListBoxItems (Used to detect empty list click).
            application_lv.UnselectAll();
        }

        private void AddApplication_Click(object sender, RoutedEventArgs e)
        {
            // Add a new application to the startup list.
            AppListBoxItem temp_app_list_box_item = new AppListBoxItem();
            applicatoin_items_l.Add(temp_app_list_box_item);

            app_settings_dialog.ResetFrom();
            app_settings_dialog.DataContext = temp_app_list_box_item;
            app_settings_dialog.AppSettingsManager = app_settings_manager_l;
            app_settings_dialog.SetDialogTitle("Add Application");
            app_settings_dialog.Visibility = System.Windows.Visibility.Visible;
        }

        private void MenuBack_Click(object sender, RoutedEventArgs e)
        {
            // Reset and hide the application settings form.
            app_settings_dialog.ResetFrom();

            // Hide the edit and delete buttons.
            this.RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;

            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            // Remove the selected AppListBoxItem.
            AppListBoxItem temp_app_list_box_item = (AppListBoxItem)application_lv.SelectedItem;

            ApplicatoinItems.Remove(temp_app_list_box_item);

            // Resave all of the application items.
            app_settings_manager_l.SaveAppItems();

            // Hide the edit and delete buttons.
            if (application_lv.SelectedItems.Count <= 0)
               RightAction_sp.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            // Edit selected application settings.
            AppListBoxItem temp_app_list_box_item = (AppListBoxItem)application_lv.SelectedItem; ;

            app_settings_dialog.PopulateForm(temp_app_list_box_item);
            app_settings_dialog.DataContext = temp_app_list_box_item;
            app_settings_dialog.AppSettingsManager = app_settings_manager_l;
            app_settings_dialog.SetDialogTitle("Edit Application");
            app_settings_dialog.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
