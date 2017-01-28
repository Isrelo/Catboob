﻿using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using CatboobGGStream.Model;

namespace CatboobGGStream.ViewModel
{
    // The ViewModel can know about the Model but can never konw about the view.
    // Updating and getting the current model and arranging the model into a
    // format that the view can easily bind to, is the job of this class.
    public class MainWindowViewModel : INPCBase
    {
        private ToolBarViewModel main_toolbar_view_model_m;
        private OverlayListViewModel overlay_list_view_model_m;
        private ModifyOverlayItemViewModel modify_overlay_item_view_model_m;

        public ToolBarViewModel MainToolBarViewModel
        {
            get { return main_toolbar_view_model_m; }
            set
            {
                main_toolbar_view_model_m = value;
                OnPropertyChanged("MainToolBarModel");
            }
        }

        public OverlayListViewModel OverlayListViewModel
        {
            get { return overlay_list_view_model_m; }
            set
            {
                overlay_list_view_model_m = value;
                OnPropertyChanged("OverlayListViewModel");
            }
        }

        public ModifyOverlayItemViewModel CurrentModifyOverlayItemViewModel
        {
            get { return modify_overlay_item_view_model_m; }
            set
            {
                modify_overlay_item_view_model_m = value;
                OnPropertyChanged("ModifyOverlayItemViewModel");
            }
        }

        public MainWindowViewModel()
        {
            // Create the initial main toolbar.
            ToolBarModel temp_toolbar_model_m = new ToolBarModel();
            temp_toolbar_model_m.TitleText = "Catboob";
            temp_toolbar_model_m.ToolBarHeight = 56;
            temp_toolbar_model_m.ToolBarBackgroundColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF009688");
            temp_toolbar_model_m.ToolBarFontColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
            temp_toolbar_model_m.MainMenuVisible = Visibility.Visible;
            temp_toolbar_model_m.MainMenuClick = new DelegateCmdBase((x) => DisplayAppMenu());
            temp_toolbar_model_m.DeleteClick = new DelegateCmdBase((x) => DeleteOverlayItem());
            temp_toolbar_model_m.EditClick = new DelegateCmdBase((x) => EditOverlayItem());

            main_toolbar_view_model_m = new ToolBarViewModel(temp_toolbar_model_m);
            main_toolbar_view_model_m.ShowModifyButtons(false);

            // Create the inital OverlayItemList
            OverlayListModel temp_overlay_list_model = new OverlayListModel();
            temp_overlay_list_model.OverlayItemSelectionChanged = new DelegateCmdBase((x) => ShowModifyOverlayItemButtons(x));
            temp_overlay_list_model.AddOverlayItemClick = new DelegateCmdBase((x) => AddOverlayItem());

            overlay_list_view_model_m = new OverlayListViewModel(temp_overlay_list_model);
            overlay_list_view_model_m.LoadOverlayItems();

            // Create the inital ModifyOverlayItemForm
            ModifyOverlayItemModel temp_modify_overlay_item_model = new ModifyOverlayItemModel();
            temp_modify_overlay_item_model.ModifyOverlayItemVisibility = Visibility.Collapsed;
            temp_modify_overlay_item_model.HotkeyClick = new DelegateCmdBase((x) => GetHotkey());
            temp_modify_overlay_item_model.DisplayDurationClick = new DelegateCmdBase((x) => ShowDisplayDurationDialog());
            temp_modify_overlay_item_model.FadeInDurationClick = new DelegateCmdBase((x) => ShowFadeInDurationDialog());
            temp_modify_overlay_item_model.FadeOutDurationClick = new DelegateCmdBase((x) => ShowFadeOutDurationDialog());
            temp_modify_overlay_item_model.SaveOverlayItemClick = new DelegateCmdBase((x) => SaveOverlayItem());
            temp_modify_overlay_item_model.CancelOverlayItemClick = new DelegateCmdBase((x) => CancelModifyOverlayItem());
            temp_modify_overlay_item_model.CurrentOverlayItemModel = new OverlayItemModel();

            modify_overlay_item_view_model_m = new ModifyOverlayItemViewModel(temp_modify_overlay_item_model);

            // Create the inital Time Picker dailog.
            TimePickerModel temp_time_picker_model = new TimePickerModel();
            temp_time_picker_model.TimePickerVisible = Visibility.Collapsed;
            temp_time_picker_model.DiscardClick = new DelegateCmdBase((x) => CancelTimePickerDialog());

            modify_overlay_item_view_model_m.CurrentTimePickerViewModel = new TimePickerViewModel(temp_time_picker_model);
        }

        public void DisplayAppMenu()
        {
            System.Diagnostics.Debug.WriteLine("Cliked display menu button!");
        }

        public void DeleteOverlayItem()
        {
            System.Diagnostics.Debug.WriteLine("Cliked delete menu button!");

            // Signal the OverlayListViewModel to remove the currently selected item.
            overlay_list_view_model_m.RemoveOverlayItem(overlay_list_view_model_m.SelectedOverlayItem);
        }

        public void AddOverlayItem()
        {
            main_toolbar_view_model_m.TitleText = "Add Item";            

            System.Diagnostics.Debug.WriteLine("Cliked add OverlayItem button!");

            OverlayItemModel temp_overlay_item_model = new OverlayItemModel();
            modify_overlay_item_view_model_m.AddOverlayItem(temp_overlay_item_model);                      

            ShowModifyOverlayItemForm(true);            
        }

        public void EditOverlayItem()
        {
            main_toolbar_view_model_m.TitleText = "Edit Item";

            System.Diagnostics.Debug.WriteLine("Cliked edit menu button!");

            modify_overlay_item_view_model_m.EditOverlayItem(overlay_list_view_model_m.SelectedOverlayItem.GetCopy());

            ShowModifyOverlayItemForm(true);
        }

        public void SaveOverlayItem()
        {
            System.Diagnostics.Debug.WriteLine("Cliked save overlay item button!");

            if (main_toolbar_view_model_m.TitleText == "Add Item")
            {
                overlay_list_view_model_m.AddOverlayItem(modify_overlay_item_view_model_m.CurrentOverlayItemModel);
            }
            else
            {
                overlay_list_view_model_m.RemoveOverlayItem(overlay_list_view_model_m.SelectedOverlayItem);
                overlay_list_view_model_m.AddOverlayItem(modify_overlay_item_view_model_m.CurrentOverlayItemModel);
                overlay_list_view_model_m.SelectedOverlayItem = modify_overlay_item_view_model_m.CurrentOverlayItemModel;
            }

            ShowModifyOverlayItemForm(false);
        }

        public void CancelModifyOverlayItem()
        {
            System.Diagnostics.Debug.WriteLine("Cliked cancel overlay item button!");

            ShowModifyOverlayItemForm(false);
        }

        public void GetHotkey()
        {
            System.Diagnostics.Debug.WriteLine("Cliked get hotkey button!");
        }

        public void CancelTimePickerDialog()
        {
            System.Diagnostics.Debug.WriteLine("Cliked cancel get duration button!");

            // Hide the time picker dialog.
            modify_overlay_item_view_model_m.CurrentTimePickerViewModel.TimePickerVisible = Visibility.Collapsed;
        }

        public void SaveDisplayDuration()
        {
            System.Diagnostics.Debug.WriteLine("Cliked save duration button!");
            CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.DisplayDuration = modify_overlay_item_view_model_m.CurrentTimePickerViewModel.SelectedTime;

            CancelTimePickerDialog();
        }

        public void ShowDisplayDurationDialog()
        {
            System.Diagnostics.Debug.WriteLine("Cliked get display duration button!");

            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.CurrentTimePickerType = TimePickerViewModel.TimePickerType.DISPLAY_DURATION;
            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.SaveClick = new DelegateCmdBase((x) => SaveDisplayDuration());

            string temp_display_duration = CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.DisplayDuration;
            this.ShowTimePickerDialog(temp_display_duration);
        }

        public void SaveFadeInDuration()
        {
            System.Diagnostics.Debug.WriteLine("Cliked save fade in duration button!");
            CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.FadeInDuration = modify_overlay_item_view_model_m.CurrentTimePickerViewModel.SelectedTime;

            CancelTimePickerDialog();
        }

        public void ShowFadeInDurationDialog()
        {
            System.Diagnostics.Debug.WriteLine("Cliked get fade in duration button!");

            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.CurrentTimePickerType = TimePickerViewModel.TimePickerType.FADE_IN_DURATION;
            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.SaveClick = new DelegateCmdBase((x) => SaveFadeInDuration());

            string temp_display_duration = CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.FadeInDuration;
            this.ShowTimePickerDialog(temp_display_duration);
        }

        public void SaveFadeOutDuration()
        {
            System.Diagnostics.Debug.WriteLine("Cliked save fade out duration button!");
            CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.FadeOutDuration = modify_overlay_item_view_model_m.CurrentTimePickerViewModel.SelectedTime;

            CancelTimePickerDialog();
        }

        public void ShowFadeOutDurationDialog()
        {
            System.Diagnostics.Debug.WriteLine("Cliked get fade out duration button!");

            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.CurrentTimePickerType = TimePickerViewModel.TimePickerType.FADE_OUT_DURATION;
            CurrentModifyOverlayItemViewModel.CurrentTimePickerViewModel.SaveClick = new DelegateCmdBase((x) => SaveFadeOutDuration());

            string temp_display_duration = CurrentModifyOverlayItemViewModel.CurrentOverlayItemModel.FadeOutDuration;
            this.ShowTimePickerDialog(temp_display_duration);
        }

        public void ShowTimePickerDialog(string current_time_p)
        {
            modify_overlay_item_view_model_m.CurrentTimePickerViewModel.SelectedTime = current_time_p;
            modify_overlay_item_view_model_m.CurrentTimePickerViewModel.TimePickerVisible = Visibility.Visible;
        }

        public void ShowMainApplicationView(bool is_visible_p)
        {
            if (is_visible_p)
            {
                main_toolbar_view_model_m.ToolBarVisible = Visibility.Visible;
                overlay_list_view_model_m.OverlayListVisibility = Visibility.Visible;
            }
            else
            {
                main_toolbar_view_model_m.ToolBarVisible = Visibility.Collapsed;
                overlay_list_view_model_m.OverlayListVisibility = Visibility.Collapsed;
            }
        }

        public void ShowModifyOverlayItemForm(bool is_visible_p)
        {
            if(is_visible_p)
            {
                modify_overlay_item_view_model_m.ModifyOverlayItemVisibility = Visibility.Visible;

                ShowMainApplicationView(false);
            }
            else
            {
                modify_overlay_item_view_model_m.ModifyOverlayItemVisibility = Visibility.Collapsed;

                ShowMainApplicationView(true);
            }
        }

        public void ShowModifyOverlayItemButtons(object is_visible_p)
        {
            bool temp_is_visible = (bool)is_visible_p;
            main_toolbar_view_model_m.ShowModifyButtons(temp_is_visible);
        }        
    }
}
