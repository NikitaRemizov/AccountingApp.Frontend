using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AccountingApp.Frontend.Utils.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> Action { get; set; }

        public RelayCommand(Action<object> action)
        {
            Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Task.Run(() => Action?.Invoke(parameter));
        }
    }
}
