using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Windows.Input;

namespace CatboobGGStream
{
    // This base class is responsible for passing on click events to a delagated command.
    // This code is specific to how windows and wpf route delagate commands. 
    public class DelegateCmdBase : ICommand
    {
        private readonly Predicate<object> can_execute_cmd_m;
        private readonly Action<object> execute_cmd_m;

        public event EventHandler CanExecuteChanged;

        public DelegateCmdBase(Action<object> execute_cmd_p)
            : this(execute_cmd_p, null)
        {
        }

        public DelegateCmdBase(Action<object> execute_cmd_p, Predicate<object> can_execute_cmd_p)
        {
            execute_cmd_m = execute_cmd_p;
            can_execute_cmd_m = can_execute_cmd_p;
        }

        public bool CanExecute(object parameter)
        {
            if (can_execute_cmd_m == null)
                return true;

            return can_execute_cmd_m(parameter);            
        }

        public void Execute(object parameter)
        {
            execute_cmd_m(parameter);            
        }

        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
