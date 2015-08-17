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
    public class OverlayItem : INotifyPropertyChanged
    {
        private int hotkey_id;
        private double sound_volume;
        private String image_path;
        private String hot_key;
        private String sound_path;
        private Visibility play_visible;
        private Visibility stop_visible;

        public int HotKeyID
        {
            get { return hotkey_id; }
            set
            {
                hotkey_id = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public double SoundVolume
        {
            get { return sound_volume; }
            set
            {
                sound_volume = value;

                // Required Changed Event
                NotifyPropertyChanged();
            }
        }

        public String ImagePath 
        { 
            get{ return image_path; }
            set
            {
                image_path = value;

                // Required Changed Event
                NotifyPropertyChanged();
            } 
        }

        public String HotKey 
        { 
            get{ return hot_key; }
            set
            {
                hot_key = value;

                // Required Changed Event
                NotifyPropertyChanged();
            } 
        }

        public String SoundPath 
        { 
            get{ return sound_path; }
            set
            {
                sound_path = value;

                // Required Changed Event
                NotifyPropertyChanged();
            } 
        }

        public Visibility PlayVisible 
        { 
            get{ return play_visible; }
            set
            {
                play_visible = value;

                // Required Changed Event
                NotifyPropertyChanged();
            } 
        }

        public Visibility StopVisible 
        { 
            get{ return stop_visible; }
            set
            {
                stop_visible = value;

                // Required Changed Event
                NotifyPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OverlayItem()
        {
            // Default Volume Level
            sound_volume = 0.5;
            image_path = "";
            hot_key = "";
            sound_path = "";
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
