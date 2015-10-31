using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatboobGGStream
{
    public class OverlayListBoxItem : INotifyPropertyChanged
    {
        private OverlayItem overlay_item_data;

        public event PropertyChangedEventHandler PropertyChanged;

        public String ImagePath
        {
            get { return this.OverlayItemData.ImagePath; }
            set
            {
                this.OverlayItemData.ImagePath = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String HotKey
        {
            get { return this.OverlayItemData.HotKey; }
            set
            {
                this.OverlayItemData.HotKey = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public Visibility PlayVisible
        {
            get { return this.OverlayItemData.PlayVisible; }
            set
            {
                this.OverlayItemData.PlayVisible = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public Visibility StopVisible
        {
            get { return this.OverlayItemData.StopVisible; }
            set
            {
                this.OverlayItemData.StopVisible = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public OverlayItem OverlayItemData
        {
            get { return overlay_item_data; }
            set
            {
                overlay_item_data = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public OverlayListBoxItem()
        {
            overlay_item_data = new OverlayItem();
        }

        public void PopulateOverlayItem(OverlayItem overlay_item_to_dispaly)
        {
            this.ImagePath = overlay_item_to_dispaly.ImagePath;
            this.HotKey = overlay_item_to_dispaly.HotKey;
            this.PlayVisible = overlay_item_to_dispaly.PlayVisible;
            this.StopVisible = overlay_item_to_dispaly.StopVisible;
            this.OverlayItemData = overlay_item_to_dispaly;
        }

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
