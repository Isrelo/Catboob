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

using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace CatboobGGStream.View
{
    /// <summary>
    /// Interaction logic for OverlayListView.xaml
    /// </summary>
    public partial class OverlayListView : UserControl
    {
        public OverlayListView()
        {
            InitializeComponent();
        }

        private void OverlayListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Adds the ability to click on a empty item
            // do deslect the list.
            OverlayListBox.UnselectAll();
        }

        private void OverlayListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(e.VerticalChange > 0)
            {
                // Hide the Add OverlayItem button.
                AddOverlayItemButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Show the Add OverlayItem button.
                AddOverlayItemButton.Visibility = Visibility.Visible;
            }

        }
    }
}
