using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;

namespace CatboobGGStream
{
    // This base class is responsible for handeling raising events for when properties update there values.
    // This code is specific to how windows and wpf handle property binding.
    public class INPCBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public virtual void VerifyPropertyName(string property_name_p)
        {
            if (TypeDescriptor.GetProperties(this)[property_name_p] == null)
            {
                String temp_message = "Invalid property name: " + property_name_p;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(temp_message);
                else
                    Debug.Fail(temp_message);
            }
        }

        protected virtual void OnPropertyChanged(string property_name_p)
        {
            this.VerifyPropertyName(property_name_p);

            if (this.PropertyChanged != null)
            {
                var temp_event = new PropertyChangedEventArgs(property_name_p);
                this.PropertyChanged(this, temp_event);
            }
        }
    }
}
