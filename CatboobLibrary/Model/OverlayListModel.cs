//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;

namespace CatboobGGStream.Model
{
    public class OverlayListModel
    {
        public Visibility OverlayListVisibility { get; set; }
        public ICommand AddOverlayItemClick { get; set; }
        public ICommand OverlayItemSelectionChanged { get; set; }

        public OverlayListModel()
        {
            OverlayListVisibility = Visibility.Visible;
        }
    }
}
