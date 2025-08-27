using System.Windows.Input;
using CloudTasker.App.Models;
using CloudTasker.App.Services;
using Microsoft.VisualBasic;

namespace CloudTasker.App.ViewModels
{
    public class TaskDetailViewModel : BindableObject
    {
        private readonly TaskService _taskService;
        private readonly INavigation _navigation;

        public string Id { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(); }
        }

        public string PageTitle => string.IsNullOrEmpty(Id) ? "Add Task" : "Edit Task";

        public ICommand SaveCommand { get; }

        public TaskDetailViewModel(TaskService taskService, INavigation navigation, TaskItem? task = null)
        {
            _taskService = taskService;
            _navigation = navigation;

            if (task != null)
            {
                Id = task.Id;
                Title = task.Title;
                Description = task.Description;
                IsDone = task.IsDone;
            }

            SaveCommand = new Command(async () => await OnSave());
        }

        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Application.Current.MainPage.DisplayAlert("Validation", "Title is required", "OK");
                return;
            }

            var newTask = new TaskItem
            {
                Title = Title,
                Description = Description,
                DueDate = DateTimeOffset.UtcNow.AddDays(1),
                IsDone = IsDone
            };

            if (string.IsNullOrEmpty(Id))
                await _taskService.CreateTaskAsync(newTask);
            else
                newTask.Id = Id;
                await _taskService.UpdateTaskAsync(Id, newTask);

            await _navigation.PopAsync();
        }
    }
}
