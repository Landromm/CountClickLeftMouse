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
		#region Поля.
		private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;
		#endregion

		#region Конструкторы.
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
		#endregion

		#region События.
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
		#endregion

		//Метод определяющий - Может ли команда выполняться.
		public bool CanExecute(object? parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        //Метод выполнение команды.
        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
