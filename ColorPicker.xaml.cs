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

namespace CatboobGGStream
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        private void Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color background_color = Color.FromRgb((byte)Red_slider.Value, (byte)Green_slider.Value, (byte)Blue_slider.Value);
            DisplayColor.Background = new SolidColorBrush(background_color);
        }
    }
}
