using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatboobGGStream.View
{
    /// <summary>
    /// Interaction logic for TimePickerView.xaml
    /// </summary>
    public partial class TimePickerView : UserControl
    {
        public event EventHandler SaveTimePickerClick;

        public TimePickerView()
        {
            InitializeComponent();
        }       

        protected void SaveTimePicker_Click(object sender, EventArgs e)
        {
            if (this.SaveTimePickerClick != null)
                this.SaveTimePickerClick(this, e);
        }
    }
}
