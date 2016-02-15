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

namespace CatboobGGStream.UserControls
{
    /// <summary>
    /// Interaction logic for AddApplicationItem.xaml
    /// </summary>
    public partial class AddApplicationItem : UserControl
    {
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
        }

        private void AppFileDialog_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Display a file open dialog for .exe files.
        }
    }
}
