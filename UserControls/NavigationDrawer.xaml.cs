using System;
using System.Collections.Generic;
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

using System.Windows.Media.Animation;

namespace CatboobGGStream
{
	/// <summary>
	/// Interaction logic for NavigationDrawer.xaml
	/// </summary>
	public partial class NavigationDrawer : UserControl
	{
        public delegate void ShowColorPickerDialog();
        public ShowColorPickerDialog showColorPickerDialog;

        public delegate void ShowResolutionPickerDialog();
        public ShowResolutionPickerDialog showResolutionPickerDialog;

        public delegate void ShowAutoAppLaunchDialog();
        public ShowAutoAppLaunchDialog showAutoAppLaunchDialog;

        public NavigationDrawer()
		{
			this.InitializeComponent();
		}

        public void OpenDrawer()
        {
            this.Visibility = System.Windows.Visibility.Visible;

            Storyboard open_drawer = (Storyboard)this.Resources["OpenDrawer"];
            open_drawer.Begin();
        }

        public void CloseDrawer()
        {
            Storyboard close_drawer = (Storyboard)this.Resources["CloseDrawer"];
            close_drawer.Completed += Close_Drawer_Completed;
            close_drawer.Begin();
        }

        private void Close_Drawer_Completed(object sender, EventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void SetDarwerWidth(double width_to_set)
        {
            NavigationDrawer_Container.Width = width_to_set;
        }

        private void NavigationOverlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.CloseDrawer();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseDrawer();
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            showColorPickerDialog();
        }

        private void ResolutionPicker_Click(object sender, RoutedEventArgs e)
        {
            showResolutionPickerDialog();
        }

        private void AutoLoadAppSetting_Click(object sender, RoutedEventArgs e)
        {
            showAutoAppLaunchDialog();
            this.CloseDrawer();
        }
    }
}