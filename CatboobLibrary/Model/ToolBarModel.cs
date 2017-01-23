using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace CatboobGGStream.Model
{
    // This class is only ever concerned with holding onto
    // the actual data.
    public class ToolBarModel
    {
        public int ToolBarHeight { get; set; }
        public String TitleText { get; set; }
        public SolidColorBrush ToolBarBackgroundColor { get; set; }
        public SolidColorBrush ToolBarFontColor { get; set; }
        public Visibility ToolBarVisible { get; set; }
        public Visibility MainMenuVisible { get; set; }
        public ICommand MainMenuClick { get; set; }
        public Visibility BackVisible { get; set; }
        public ICommand BackClick { get; set; }
        public Visibility CancelVisible { get; set; }
        public ICommand CancelClick { get; set; }
        public Visibility DeleteVisible { get; set; }
        public ICommand DeleteClick { get; set; }
        public Visibility EditVisible { get; set; }
        public ICommand EditClick { get; set; }
        public Visibility SaveVisible { get; set; }
        public ICommand SaveClick { get; set; }

        public ToolBarModel()
        {
            // Make sure the toolbar is shown by default.
            ToolBarVisible = Visibility.Visible;

            // Hide all of the buttons on the toolbar
            // by default. This allows us to only have
            // to show just the buttons we want show on
            // the toolbar.            
            MainMenuVisible = Visibility.Collapsed;
            BackVisible = Visibility.Collapsed;
            CancelVisible = Visibility.Collapsed;
            DeleteVisible = Visibility.Collapsed;
            EditVisible = Visibility.Collapsed;
            SaveVisible = Visibility.Collapsed;            
        }
    }
}
