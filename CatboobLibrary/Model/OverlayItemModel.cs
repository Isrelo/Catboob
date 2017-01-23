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
            FadeInDuration = "00:00:01";
            FadeOutDuration = "00:00:01";

            // Default Volume Level
            SoundVolume = 0.5;
            ImagePath = "";
            HotKey = "";
            SoundPath = "";

            // Hide the play and stop buttons by default.
            PlayVisible = Visibility.Visible;
            StopVisible = Visibility.Collapsed;
        }
    }
}
