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

using CatboobGGStream.ViewModel;

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel main_window_view_model_m;

        public MainWindowView()
        {
            InitializeComponent();

            // Keep track of the main view model that makes up the initial
            // state of the application.
            main_window_view_model_m = new MainWindowViewModel();
            this.DataContext = main_window_view_model_m;
        }

        private void ApplicationToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Set up the inital tool bar's ViewModel. The MainWindowToolBar
            // is responsible for maintaning the state of the ToolBar's ViewModel.
            // This is here to properly bind to the toolbar for the main applicaion.
            ApplicationToolBar.DataContext = main_window_view_model_m.MainToolBarViewModel;
        }

        private void OverlayItemsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Set up the inital OveralyItemList's ViewModel. The MainWindowToolBar
            // is responsible for maintaning the state of the OverlayItemList's ViewModel.
            // This is here to properly bind to the OverlayItemList.
            OverlayItemsListBox.DataContext = main_window_view_model_m.OverlayListViewModel;
        }

        private void ModifyOverlayItemsForm_Loaded(object sender, RoutedEventArgs e)
        {
            ModifyOverlayItemsForm.DataContext = main_window_view_model_m.CurrentModifyOverlayItemViewModel;
        }
    }
}
