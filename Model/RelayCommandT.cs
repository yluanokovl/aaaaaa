using System.Windows.Input;

namespace WpfApp9.Model
{
    public class RelayCommand<T>(Action<object?> execute, Predicate<object?>? canExecute = null) : ICommand
    {
        private readonly Action<object?> _execute = execute ?? throw new
        ArgumentNullException(nameof(execute));
        private readonly Predicate<object?>? _canExecute = canExecute;
        public bool CanExecute(object? parameter)
        => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter)
        {
            if (CanExecute(parameter)) _execute.Invoke(parameter);
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
