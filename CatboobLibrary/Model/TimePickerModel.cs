using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;

namespace CatboobGGStream.Model
{
    public class TimePickerModel
    {
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Second { get; set; }
        public string Millisecond { get; set; }
        public Visibility TimePickerVisible { get; set; }
        public ICommand DiscardClick { get; set; }        
        public ICommand SaveClick { get; set; }

        public TimePickerModel()
        {
            Hour = "00";
            Minute = "00";
            Second = "00";
            Millisecond = "00";
            TimePickerVisible = Visibility.Collapsed;
        }
    }
}
