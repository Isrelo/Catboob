using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatboobGGStream
{
    class LIstBoxItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private String image_path_l;
        private String item_title_l;

        public String ImagePath
        {
            get { return this.image_path_l; }
            set
            {
                this.image_path_l = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String ItemTitle
        {
            get { return this.item_title_l; }
            set
            {
                this.item_title_l = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
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
