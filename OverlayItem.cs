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
    public class OverlayItem
    {
        public int HotKeyID { get; set; }
        public double SoundVolume { get; set; }
        public String ImagePath { get; set; }
        public String HotKey { get; set; }
        public String SoundPath { get; set; }
        public String DisplayDuration { get; set; }
        public Visibility PlayVisible { get; set; }
        public Visibility StopVisible { get; set; }

        public OverlayItem()
        {
            // Default Dipslay Interval in Milliseconds
            DisplayDuration = "00:03:00";
            // Default Volume Level
            SoundVolume = 0.5;
            ImagePath = "";
            HotKey = "";
            SoundPath = "";
        }
    }
}
