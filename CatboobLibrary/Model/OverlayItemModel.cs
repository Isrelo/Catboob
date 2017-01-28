using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;

namespace CatboobGGStream.Model
{
    public class OverlayItemModel
    {
        public int HotKeyID { get; set; }
        public double SoundVolume { get; set; }
        public String ImagePath { get; set; }
        public String HotKey { get; set; }
        public String SoundPath { get; set; }
        public String DisplayDuration { get; set; }
        public String FadeInDuration { get; set; }
        public String FadeOutDuration { get; set; }
        public Visibility PlayVisible { get; set; }
        public ICommand PlayClick { get; set; }
        public Visibility StopVisible { get; set; }
        public ICommand StopClick { get; set; }

        public OverlayItemModel()
        {
            // Default Dipslay Interval in Milliseconds
            DisplayDuration = "00:03:00";
            FadeInDuration = "00:01:00";
            FadeOutDuration = "00:01:00";

            // Default Volume Level
            SoundVolume = 0.5;
            ImagePath = "";
            HotKey = "";
            SoundPath = "";

            // Hide the play and stop buttons by default.
            PlayVisible = Visibility.Visible;
            StopVisible = Visibility.Collapsed;
        }

        public OverlayItemModel GetCopy()
        {
            // Make a copy of the exsisitng overlay item.
            OverlayItemModel temp_overlay_item_model = new OverlayItemModel();
            temp_overlay_item_model.HotKeyID = this.HotKeyID;
            temp_overlay_item_model.SoundVolume = this.SoundVolume;
            temp_overlay_item_model.ImagePath = this.ImagePath;
            temp_overlay_item_model.HotKey = this.HotKey;
            temp_overlay_item_model.SoundPath = this.SoundPath;
            temp_overlay_item_model.DisplayDuration = this.DisplayDuration;
            temp_overlay_item_model.FadeInDuration = this.FadeInDuration;
            temp_overlay_item_model.FadeOutDuration = this.FadeOutDuration;
            temp_overlay_item_model.PlayVisible = this.PlayVisible;
            temp_overlay_item_model.PlayClick = this.PlayClick;
            temp_overlay_item_model.StopVisible = this.StopVisible;
            temp_overlay_item_model.StopClick = this.StopClick;

            return temp_overlay_item_model;
        }
    }
}
