using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using CatboobGGStream.Model;

namespace CatboobGGStream.ViewModel
{
    // The ViewModel can know about the Model but can never konw about the view.
    // Updating and getting the current model and arranging the model into a
    // format that the view can easily bind to, is the job of this class.
    public class ToolBarViewModel : INPCBase
    {
        private ToolBarModel current_toolbar_model_m;

        public int ToolBarHeight
        {
            get { return current_toolbar_model_m.ToolBarHeight; }
            set
            {
                current_toolbar_model_m.ToolBarHeight = value;
                OnPropertyChanged("ToolBarHeight");
            }
        }

        public String TitleText
        {
            get { return current_toolbar_model_m.TitleText; }
            set
            {
                current_toolbar_model_m.TitleText = value;
                OnPropertyChanged("TitleText");
            }
        }

        public SolidColorBrush ToolBarBackgroundColor
        {
            get { return current_toolbar_model_m.ToolBarBackgroundColor; }
            set
            {
                current_toolbar_model_m.ToolBarBackgroundColor = value;
                OnPropertyChanged("ToolBarBackgroundColor");
            }
        }

        public SolidColorBrush ToolBarFontColor
        {
            get { return current_toolbar_model_m.ToolBarFontColor; }
            set
            {
                current_toolbar_model_m.ToolBarFontColor = value;
                OnPropertyChanged("ToolBarFontColor");
            }
        }

        public Visibility ToolBarVisible
        {
            get { return current_toolbar_model_m.ToolBarVisible; }
            set
            {
                current_toolbar_model_m.ToolBarVisible = value;
                OnPropertyChanged("ToolBarVisible");
            }
        }

        public Visibility MainMenuVisible
        {
            get { return current_toolbar_model_m.MainMenuVisible; }
            set
            {
                current_toolbar_model_m.MainMenuVisible = value;
                OnPropertyChanged("MainMenuVisible");
            }
        }

        public ICommand MainMenuClick
        {
            get { return current_toolbar_model_m.MainMenuClick; }
            set
            {
                current_toolbar_model_m.MainMenuClick = value;
                OnPropertyChanged("MainMenuClick");
            }
        }

        public Visibility BackVisible
        {
            get { return current_toolbar_model_m.BackVisible; }
            set
            {
                current_toolbar_model_m.BackVisible = value;
                OnPropertyChanged("BackVisible");
            }
        }

        public ICommand BackClick
        {
            get { return current_toolbar_model_m.BackClick; }
            set
            {
                current_toolbar_model_m.BackClick = value;
                OnPropertyChanged("BackClick");
            }
        }

        public Visibility CancelVisible
        {
            get { return current_toolbar_model_m.CancelVisible; }
            set
            {
                current_toolbar_model_m.CancelVisible = value;
                OnPropertyChanged("CancelVisible");
            }
        }

        public ICommand CancelClick
        {
            get { return current_toolbar_model_m.CancelClick; }
            set
            {
                current_toolbar_model_m.CancelClick = value;
                OnPropertyChanged("CancelClick");
            }
        }

        public Visibility DeleteVisible
        {
            get { return current_toolbar_model_m.DeleteVisible; }
            set
            {
                current_toolbar_model_m.DeleteVisible = value;
                OnPropertyChanged("DeleteVisible");
            }
        }

        public ICommand DeleteClick
        {
            get { return current_toolbar_model_m.DeleteClick; }
            set
            {
                current_toolbar_model_m.DeleteClick = value;
                OnPropertyChanged("DeleteClick");
            }
        }

        public Visibility EditVisible
        {
            get { return current_toolbar_model_m.EditVisible; }
            set
            {
                current_toolbar_model_m.EditVisible = value;
                OnPropertyChanged("EditVisible");
            }
        }

        public ICommand EditClick
        {
            get { return current_toolbar_model_m.EditClick; }
            set
            {
                current_toolbar_model_m.EditClick = value;
                OnPropertyChanged("EditClick");
            }
        }

        public Visibility SaveVisible
        {
            get { return current_toolbar_model_m.SaveVisible; }
            set
            {
                current_toolbar_model_m.SaveVisible = value;
                OnPropertyChanged("SaveVisible");
            }
        }

        public ICommand SaveClick
        {
            get { return current_toolbar_model_m.SaveClick; }
            set
            {
                current_toolbar_model_m.SaveClick = value;
                OnPropertyChanged("SaveClick");
            }
        }

        public void ShowModifyButtons(bool is_visible_p)
        {
            if (is_visible_p)
            {
                this.DeleteVisible = Visibility.Visible;
                this.EditVisible = Visibility.Visible;
            }
            else
            {
                this.DeleteVisible = Visibility.Collapsed;
                this.EditVisible = Visibility.Collapsed;
            }
        }

        public ToolBarViewModel(ToolBarModel current_toolbar_model_p)
        {
            current_toolbar_model_m = current_toolbar_model_p;
        }
    }
}
