using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CountClickLeftMouse.Model
{
    public class BaseCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        //Constructors 
        public BaseCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public BaseCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        //Events 
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        //Может ли команда выполняться
        public bool CanExecute(object? parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        //Выполнение команды.
        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
