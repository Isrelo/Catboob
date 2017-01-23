//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;

namespace CatboobGGStream.Model
{
    public class ModifyOverlayItemModel
    {
        public bool IsNewOverlayItem { get; set; }
        public OverlayItemModel CurrentOverlayItemModel { get; set; }
        public Visibility ModifyOverlayItemVisibility { get; set; }
        public ICommand SaveOverlayItemClick { get; set; }
        public ICommand CancelOverlayItemClick { get; set; }
        public ICommand HotkeyClick { get; set; }
        public ICommand DisplayDurationClick { get; set; }
        public ICommand FadeInDurationClick { get; set; }
        public ICommand FadeOutDurationClick { get; set; }

        public ModifyOverlayItemModel()
        {
            IsNewOverlayItem = true;
            ModifyOverlayItemVisibility = Visibility.Visible;
        }
    }
}
