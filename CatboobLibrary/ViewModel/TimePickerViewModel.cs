using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Input;
using CatboobGGStream.Model;

namespace CatboobGGStream.ViewModel
{
    public class TimePickerViewModel : INPCBase
    {
        public enum TimePickerType
        {
            DISPLAY_DURATION = 0,
            FADE_IN_DURATION = 1,
            FADE_OUT_DURATION = 2
        }

        TimePickerType current_time_picker_type_m;
        TimePickerModel time_picker_model_m;

        public TimePickerType CurrentTimePickerType
        {
            get { return current_time_picker_type_m; }
            set
            {
                current_time_picker_type_m = value;
                OnPropertyChanged("CurrentTimePickerType");
            }
        }

        public string Minute
        {
            get { return time_picker_model_m.Minute; }
            set
            {
                time_picker_model_m.Minute = value;
                OnPropertyChanged("Minute");
            }
        }

        public string Second
        {
            get { return time_picker_model_m.Second; }
            set
            {
                time_picker_model_m.Second = value;
                OnPropertyChanged("Second");
            }
        }

        public string Millisecond
        {
            get { return time_picker_model_m.Millisecond; }
            set
            {
                time_picker_model_m.Millisecond = value;
                OnPropertyChanged("Millisecond");
            }
        }

        public Visibility TimePickerVisible
        {
            get { return time_picker_model_m.TimePickerVisible; }
            set
            {
                time_picker_model_m.TimePickerVisible = value;
                OnPropertyChanged("TimePickerVisible");
            }
        }

        public ICommand DiscardClick
        {
            get { return time_picker_model_m.DiscardClick; }
            set
            {
                time_picker_model_m.DiscardClick = value;
                OnPropertyChanged("DiscardClick");
            }
        }

        public ICommand SaveClick
        {
            get { return time_picker_model_m.SaveClick; }
            set
            {
                time_picker_model_m.SaveClick = value;
                OnPropertyChanged("SaveClick");
            }
        }

        public string SelectedTime
        {
            get
            {
                string temp_selected_time = String.Format("{0}:{1}:00", Minute, Second);
                return temp_selected_time;
            }
            set
            {
                string[] temp_selected_time = value.Split(':');
                this.Minute = temp_selected_time[0];
                this.Second = temp_selected_time[1];
                this.Millisecond = temp_selected_time[2];
                OnPropertyChanged("SelectedTime");
            }
        }

        public TimePickerViewModel(TimePickerModel time_picker_model_p)
        {
            time_picker_model_m = time_picker_model_p;
        }
    }
}
