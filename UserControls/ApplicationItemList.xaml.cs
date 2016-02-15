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

namespace CatboobGGStream.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationItemList.xaml
    /// </summary>
    public partial class ApplicationItemList : UserControl
    {
        private BindingList<LIstBoxItem> applicatoin_items_l;

        private BindingList<LIstBoxItem> ApplicatoinItems
        {
            get { return applicatoin_items_l; }
            set { applicatoin_items_l = value; }
        }

        public ApplicationItemList()
        {
            InitializeComponent();
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

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Figure out what to do when a action for a item in the list is clicked.
        }

        private void AddApplication_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add a new application to the startup list.

            System.Diagnostics.Debug.WriteLine("Clicked on add new applicaiton to startup list!");
        }

        private void MenuBack_Click(object sender, RoutedEventArgs e)
        {
            //User is done adding or editing applications. Take the user back to the main application screen.
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MenuCancel_Click(object sender, RoutedEventArgs e)
        {
            //TODO: User deosn't want to change the application settings or doesn't want to add a applcation.
            // Take the user back to the applicaiton list.
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            //TODO: User wants the selected item in the list removed.
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            //TODO: User wants the changes made to be applied to the selected or added item.
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: User wants to edit an exsiting item.
        }
    }
}
