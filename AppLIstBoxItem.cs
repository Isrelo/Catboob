﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatboobGGStream
{
    public class AppListBoxItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private String app_path_l;
        private String app_title_l;
        private String app_arguments_l;
        private ImageSource app_icon_l;

        public ImageSource AppIcon
        {
            get { return this.app_icon_l; }
            set
            {
                this.app_icon_l = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String AppPath
        {
            get { return this.app_path_l; }
            set
            {
                this.app_path_l = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String AppTitle
        {
            get { return this.app_title_l; }
            set
            {
                this.app_title_l = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String AppArguments
        {
            get { return this.app_arguments_l; }
            set
            {
                this.app_arguments_l = value;

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
