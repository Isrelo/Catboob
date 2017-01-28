using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using CatboobGGStream.Model;

namespace CatboobGGStream.ViewModel
{
    // This class is responsible for allowing the user to edit
    // new and existing OverlayItems.
    public class ModifyOverlayItemViewModel : INPCBase
    {
        private ModifyOverlayItemModel modify_overlay_item_model_m;
        private TimePickerViewModel time_picker_view_model_m;
        private ToolBarViewModel modify_overlay_item_toolbar_view_model_m;        

        public OverlayItemModel CurrentOverlayItemModel
        {
            get { return modify_overlay_item_model_m.CurrentOverlayItemModel; }
            set
            {
                modify_overlay_item_model_m.CurrentOverlayItemModel = value;
                OnPropertyChanged("CurrentOverlayItemModel");
            }
        }

        public TimePickerViewModel CurrentTimePickerViewModel
        {
            get { return time_picker_view_model_m; }
            set
            {
                time_picker_view_model_m = value;
                OnPropertyChanged("CurrentTimePickerViewModel");
            }
        }

        public ToolBarViewModel ModifyOverlayItemToolbarViewModel
        {
            get { return modify_overlay_item_toolbar_view_model_m; }
            set
            {
                modify_overlay_item_toolbar_view_model_m = value;
                OnPropertyChanged("ModifyOverlayItemToolbarViewModel");
            }
        }

        public Visibility ModifyOverlayItemVisibility
        {
            get { return modify_overlay_item_model_m.ModifyOverlayItemVisibility; }
            set
            {
                modify_overlay_item_model_m.ModifyOverlayItemVisibility = value;
                OnPropertyChanged("ModifyOverlayItemVisibility");
            }
        }

        public ICommand HotkeyClick
        {
            get { return modify_overlay_item_model_m.HotkeyClick; }
            set
            {
                modify_overlay_item_model_m.HotkeyClick = value;
                OnPropertyChanged("HotkeyClick");
            }
        }
                        
        public ICommand DisplayDurationClick
        {
            get { return modify_overlay_item_model_m.DisplayDurationClick; }
            set
            {
                modify_overlay_item_model_m.DisplayDurationClick = value;
                OnPropertyChanged("DisplayDurationClick");
            }
        }

        public ICommand FadeInDurationClick
        {
            get { return modify_overlay_item_model_m.FadeInDurationClick; }
            set
            {
                modify_overlay_item_model_m.FadeInDurationClick = value;
                OnPropertyChanged("FadeInDurationClick");
            }
        }

        public ICommand FadeOutDurationClick
        {
            get { return modify_overlay_item_model_m.FadeOutDurationClick; }
            set
            {
                modify_overlay_item_model_m.FadeOutDurationClick = value;
                OnPropertyChanged("FadeOutDurationClick");
            }
        }

        public void EditOverlayItem(OverlayItemModel overlay_item_model_to_edit_p)
        {
            modify_overlay_item_toolbar_view_model_m.TitleText = "Edit Item";

            CurrentOverlayItemModel = overlay_item_model_to_edit_p;
        }

        public void AddOverlayItem(OverlayItemModel overlay_item_model_to_add_p)
        {
            modify_overlay_item_toolbar_view_model_m.TitleText = "Add Item";

            CurrentOverlayItemModel = overlay_item_model_to_add_p;
        }

        public ModifyOverlayItemViewModel(ModifyOverlayItemModel modify_overlay_item_model_p)
        {
            modify_overlay_item_model_m = modify_overlay_item_model_p;

            // Create the initial main toolbar.
            ToolBarModel temp_toolbar_model_m = new ToolBarModel();
            temp_toolbar_model_m.ToolBarHeight = 56;
            temp_toolbar_model_m.ToolBarBackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF009688");
            temp_toolbar_model_m.ToolBarFontColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
            temp_toolbar_model_m.CancelVisible = Visibility.Visible;
            temp_toolbar_model_m.CancelClick = modify_overlay_item_model_m.CancelOverlayItemClick;
            temp_toolbar_model_m.SaveVisible = Visibility.Visible;
            temp_toolbar_model_m.SaveClick = modify_overlay_item_model_m.SaveOverlayItemClick;

            modify_overlay_item_toolbar_view_model_m = new ToolBarViewModel(temp_toolbar_model_m);
        }
    }
}
