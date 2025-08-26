using System.Collections.ObjectModel;
using System.Windows.Input;
using CloudTasker.App.Models;
using CloudTasker.App.Services;

namespace CloudTasker.App.ViewModels
{
    public class TasksViewModel : BindableObject
    {
        private readonly TaskService _taskService;

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        // Commands for UI
        public ICommand LoadTasksCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }

        public TasksViewModel(TaskService taskService)
        {
            _taskService = taskService;

            LoadTasksCommand = new Command(async () => await LoadTasksAsync());
            RefreshCommand = new Command(async () => await LoadTasksAsync());
            DeleteTaskCommand = new Command<string>(async (id) => await DeleteTaskAsync(id));
            AddTaskCommand = new Command(OnAddTask);
            EditTaskCommand = new Command<TaskItem>(OnEditTask);
        }

        public async Task LoadTasksAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Tasks.Clear();

                var tasks = await _taskService.GetTasksAsync();
                // Order by UpdatedAt descending
                foreach (var task in tasks.OrderByDescending(t => t.UpdatedAt))
                {
                    Tasks.Add(task);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteTaskAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return;

            var ok = await _taskService.DeleteTaskAsync(id);
            if (ok)
            {
                var toRemove = Tasks.FirstOrDefault(t => t.Id == id);
                if (toRemove != null)
                    Tasks.Remove(toRemove);
            }
        }

        private void OnAddTask()
        {
            // 👉 Navigation to Add Task page (to implement)
            Console.WriteLine("AddTask tapped");
        }

        private void OnEditTask(TaskItem task)
        {
            if (task == null) return;

            // 👉 Navigation to Edit Task page with the task (to implement)
            Console.WriteLine($"EditTask tapped for {task.Id}");
        }
    }
}
