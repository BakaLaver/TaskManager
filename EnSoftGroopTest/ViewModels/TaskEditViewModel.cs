using EnSoftGroopTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using EnSoftGroopTest.ViewModels.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EnSoftGroopTest.ViewModels
{
    public class TaskEditViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly TaskItem _task;
        private readonly bool _isNewTask;
        private RelayCommand _saveCommand;

        public TaskItem Task => _task;

        public ICommand SaveCommand => _saveCommand;
        public ICommand CancelCommand { get; }

        public TaskEditViewModel(TaskItem task, bool isNewTask)
        {
            _task = task;
            _isNewTask = isNewTask;

            _saveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);

            // Подписываемся на изменения свойств задачи
            _task.PropertyChanged += (s, e) =>
            {
                // При любом изменении задачи проверяем, можно ли активировать кнопку
                _saveCommand.RaiseCanExecuteChanged();
                // Также сообщаем UI об изменении (для валидации)
                OnPropertyChanged(nameof(Task));
            };
        }

        private bool CanExecuteSave(object parameter)
        {
            // Проверяем, что название не пустое и дата не в прошлом (для новой задачи)
            bool isValid = !string.IsNullOrWhiteSpace(_task.Title) && !HasDueDateError();
            return isValid;
        }

        private bool HasDueDateError()
        {
            if (_task.DueDate.HasValue && _isNewTask)
            {
                return _task.DueDate.Value.Date < DateTime.Now.Date;
            }
            return false;
        }

        private void ExecuteSave(object parameter)
        {
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);

            if (window != null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        private void ExecuteCancel(object parameter)
        {
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);

            window?.Close();
        }

        // IDataErrorInfo для валидации
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(Task.Title):
                        if (string.IsNullOrWhiteSpace(_task.Title))
                            error = "Название задачи обязательно";
                        break;

                    case nameof(Task.DueDate):
                        if (_task.DueDate.HasValue && _isNewTask && _task.DueDate.Value.Date < DateTime.Now.Date)
                            error = "Дата не может быть в прошлом";
                        break;
                }

                // При изменении валидации обновляем команду
                _saveCommand?.RaiseCanExecuteChanged();

                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
