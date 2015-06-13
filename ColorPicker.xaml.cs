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
        public delegate void SavePickedColor();
        public SavePickedColor savePickedColor;

        public delegate void ClosePickColor();
        public ClosePickColor closePickColor;

        public Color BackgroundColor { get; set; }

        public ColorPicker()
        {
            InitializeComponent();
        }

        public void SetControlColor(double red, double green, double blue)
        {
            Red_slider.Value = red;
            Green_slider.Value = green;
            Blue_slider.Value = blue;
            BackgroundColor = Color.FromRgb((byte)red, (byte)green, (byte)blue);
            DisplayColor.Background = new SolidColorBrush(BackgroundColor);
        }

        public void SetControlColor(Color color_to_set)
        {
            SetControlColor((double)color_to_set.R, (double)color_to_set.G, (double)color_to_set.B);
        }

        private void Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetControlColor(Red_slider.Value, Green_slider.Value, Blue_slider.Value);
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            closePickColor();            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            savePickedColor();
        }
    }
}
