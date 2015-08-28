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
    public partial class ResolutionPicker : UserControl
    {
        public delegate void SavePickedResolution(int width, int height);
        public SavePickedResolution savePickedResolution;

        public delegate void ClosePickResolution();
        public ClosePickResolution closePickedResolution;

        public Color BackgroundColor { get; set; }

        public ResolutionPicker()
        {
            InitializeComponent();
        }

        private void Discard_Click(object sender, RoutedEventArgs e)
        {
            closePickedResolution();            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int width = 100;
            int height = 100;

            ComboBoxItem temp_selected_item = (ComboBoxItem)Resolution_combobox.SelectedValue;
            String selected_resolution = temp_selected_item.Content.ToString();
            String[] selected_width_height = selected_resolution.Split('x');
            int.TryParse(selected_width_height[0], out width);
            int.TryParse(selected_width_height[1], out height);

            savePickedResolution(width, height);
        }
    }
}
