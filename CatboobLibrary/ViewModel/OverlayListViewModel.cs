//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CatboobGGStream.Model;

namespace CatboobGGStream.ViewModel
{
    // The ViewModel can know about the Model but can never konw about the view.
    // Updating and getting the current model and arranging the model into a
    // format that the view can easily bind to, is the job of this class.
    public class OverlayListViewModel : INPCBase
    {
        private OverlayItemModel selected_overlay_item_m;
        OverlayListModel overlay_list_model_m;

        public ObservableCollection<OverlayItemModel> OverlayItemsList { get; set; }

        public Visibility OverlayListVisibility
        {
            get { return overlay_list_model_m.OverlayListVisibility; }
            set
            {
                overlay_list_model_m.OverlayListVisibility = value;
                OnPropertyChanged("OverlayListVisibility");
            }
        }

        public ICommand AddOverlayItemClick
        {
            get { return overlay_list_model_m.AddOverlayItemClick; }
            set
            {
                overlay_list_model_m.AddOverlayItemClick = value;
                OnPropertyChanged("AddOverlayItemClick");
            }
        }

        public OverlayItemModel SelectedOverlayItem
        {
            get { return selected_overlay_item_m; }
            set
            {
                if(value == null)
                    overlay_list_model_m.OverlayItemSelectionChanged.Execute(false);
                else
                    overlay_list_model_m.OverlayItemSelectionChanged.Execute(true);
                   
                selected_overlay_item_m = value;
                OnPropertyChanged("SelectedOverlayItem");
            }
        }

        public OverlayListViewModel(OverlayListModel overlay_list_model_p)
        {
            overlay_list_model_m = overlay_list_model_p;

            this.LoadOverlayItems();
        }

        public void AddOverlayItem(OverlayItemModel overlay_item_model_to_add_p)
        {
            overlay_item_model_to_add_p.PlayClick = new DelegateCmdBase((x) => PlaySound());
            overlay_item_model_to_add_p.StopClick = new DelegateCmdBase((x) => StopSound());
            OverlayItemsList.Add(overlay_item_model_to_add_p);
        }

        public void RemoveOverlayItem(OverlayItemModel overlay_item_model_to_remove_p)
        {
            OverlayItemsList.Remove(overlay_item_model_to_remove_p);
        }

        public void PlaySound()
        {
            System.Diagnostics.Debug.WriteLine("Cliked play sound button!");
        }

        public void StopSound()
        {
            System.Diagnostics.Debug.WriteLine("Cliked stop sound button!");
        }

        public void LoadOverlayItems()
        {
            //TODO: Testing Code Remove Later
            ObservableCollection<OverlayItemModel> temp_overlay_items_list = new ObservableCollection<OverlayItemModel>();
            OverlayItemModel temp_item = new OverlayItemModel();
            temp_item.HotKeyID = 0;
            temp_item.SoundVolume = 0.5;
            temp_item.ImagePath = "C:\\Program Files\\Catboob\\Images\\GGButton.png";
            temp_item.HotKey = "LeftCtrl + G";
            temp_item.SoundPath = "C:\\Program Files\\Catboob\\Sounds\\gg.mp3";
            temp_item.DisplayDuration = "00:03:00";

            OverlayItemsList = temp_overlay_items_list;

            this.AddOverlayItem(temp_item);
        }

        public void SaveOverlayItems()
        {
        }
    }
}
