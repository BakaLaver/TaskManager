using EnSoftGroopTest.Models;
using EnSoftGroopTest.Services;
using EnSoftGroopTest.Services.Abstraction;
using EnSoftGroopTest.ViewModels.Commands;
using EnSoftGroopTest.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TaskStatus = EnSoftGroopTest.Models.TaskStatus;

namespace EnSoftGroopTest.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFileService _fileService;
        private ObservableCollection<TaskItem> _tasks;
        private TaskItem _selectedTask;
        private string _searchText;
        private string _selectedFilter = "Все";
        private ICollectionView _tasksView;

        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                (EditTaskCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteTaskCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                TasksView?.Refresh();
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
                TasksView?.Refresh();
            }
        }

        public ICollectionView TasksView
        {
            get => _tasksView;
            set
            {
                _tasksView = value;
                OnPropertyChanged();
            }
        }

        // Команды
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ToggleStatusCommand { get; }

        public MainViewModel()
        {
            _fileService = new FileService();
            Tasks = new ObservableCollection<TaskItem>();

            // Инициализация команд
            AddTaskCommand = new RelayCommand(ExecuteAddTask);
            EditTaskCommand = new RelayCommand(ExecuteEditTask, CanExecuteEditDelete);
            DeleteTaskCommand = new RelayCommand(ExecuteDeleteTask, CanExecuteEditDelete);
            ToggleStatusCommand = new RelayCommand(ExecuteToggleStatus);

            _selectedFilter = "Все";
            // Загрузка данных
            LoadTasksAsync();

            // Настройка CollectionView
            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.CreatedAt), ListSortDirection.Descending));
        }

        private async void LoadTasksAsync()
        {
            try
            {
                var loadedTasks = await _fileService.LoadTasksAsync();
                Tasks.Clear();
                foreach (var task in loadedTasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool FilterTasks(object obj)
        {
            if (obj is TaskItem task)
            {
                // Диагностика - посмотрим, что приходит
                System.Diagnostics.Debug.WriteLine($"Filter: Task Status={task.Status}, SelectedFilter={SelectedFilter}");

                // Фильтр по статусу
                if (SelectedFilter != "Все")
                {
                    // Получаем статус задачи в виде строки для сравнения
                    string taskStatusString = task.Status == TaskStatus.Active ? "Активные" : "Завершённые";

                    // Если статус задачи не соответствует выбранному фильтру - скрываем
                    if (taskStatusString != SelectedFilter)
                    {
                        System.Diagnostics.Debug.WriteLine($"  -> Filtered out: {task.Title}");
                        return false;
                    }
                }

                // Поиск по названию и описанию
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    bool matchesSearch = (task.Title?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true ||
                                         task.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true);

                    if (!matchesSearch)
                    {
                        System.Diagnostics.Debug.WriteLine($"  -> Search filtered out: {task.Title}");
                        return false;
                    }
                }

                System.Diagnostics.Debug.WriteLine($"  -> Passed: {task.Title}");
                return true;
            }
            return false;
        }

        private bool CanExecuteEditDelete(object parameter) => SelectedTask != null;

        private void ExecuteAddTask(object parameter)
        {
            var newTask = new TaskItem()
            {
                Status = TaskStatus.Active
            };
            var editVm = new TaskEditViewModel(newTask, isNewTask: true);
            var editWindow = new TaskEditWindow { DataContext = editVm };

            if (editWindow.ShowDialog() == true)
            {
                Tasks.Add(newTask);
                SaveTasksAsync();
            }
        }

        private void ExecuteEditTask(object parameter)
        {
            var taskCopy = new TaskItem
            {
                Id = SelectedTask.Id,
                Title = SelectedTask.Title,
                Description = SelectedTask.Description,
                Status = SelectedTask.Status,
                Priority = SelectedTask.Priority,
                DueDate = SelectedTask.DueDate,
                CreatedAt = SelectedTask.CreatedAt
            };

            var editVm = new TaskEditViewModel(taskCopy, isNewTask: false);
            var editWindow = new TaskEditWindow { DataContext = editVm };

            if (editWindow.ShowDialog() == true)
            {
                // Обновляем оригинальную задачу
                SelectedTask.Title = taskCopy.Title;
                SelectedTask.Description = taskCopy.Description;
                SelectedTask.Priority = taskCopy.Priority;
                SelectedTask.Status = taskCopy.Status;
                SelectedTask.DueDate = taskCopy.DueDate;

                SaveTasksAsync();
                TasksView.Refresh();
            }
        }

        private async void ExecuteDeleteTask(object parameter)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить задачу?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Tasks.Remove(SelectedTask);
                await SaveTasksAsync();
            }
        }

        private void ExecuteToggleStatus(object parameter)
        {
            if (parameter is TaskItem task)
            {
                task.Status = task.Status == TaskStatus.Active ?
                    TaskStatus.Completed : TaskStatus.Active;

                SaveTasksAsync();
                TasksView.Refresh();
            }
        }

        private async Task SaveTasksAsync()
        {
            try
            {
                await _fileService.SaveTasksAsync(Tasks.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
