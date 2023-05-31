using System;
using System.Windows.Input;

namespace QuizOstateczny.ViewModel
{
    class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                if (canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        private Action<object> execute;
        private Predicate<object> canExecute;
        private Action<string> navigate;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute) 
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<string> navigate)
        {
            this.navigate = navigate;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
