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
    /// Interaction logic for VolumeSlider.xaml
    /// </summary>
    public partial class VolumeSlider : UserControl
    {
        private double volume_level;

        public double VolumeLevel 
        {
            get
            {
                volume_level = Volume_slider.Value;
                return volume_level;
            }

            set
            {
                volume_level = value;
                Volume_slider.Value = value;
            }
        }

        public VolumeSlider()
        {
            InitializeComponent();
        }

        private void VolumeImageToDisplay(double slider_value)
        {
            volume_level = slider_value;

            if (slider_value <= 0)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Visible;
                VolumeDown_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeUp_img.Visibility = System.Windows.Visibility.Collapsed;
            }

            if(slider_value > 0 && slider_value <= 0.5)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeDown_img.Visibility = System.Windows.Visibility.Visible;
                VolumeUp_img.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (slider_value > 0.5 && slider_value < 1.0)
            {
                VolumeMute_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeDown_img.Visibility = System.Windows.Visibility.Collapsed;
                VolumeUp_img.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider volume_slider = (Slider)sender;
            VolumeImageToDisplay(volume_slider.Value);
        }
    }
}
