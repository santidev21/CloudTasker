using CloudTasker.App.ViewModels;

namespace CloudTasker.App.Views;

public partial class TaskDetailPage : ContentPage
{
    public TaskDetailPage(TaskDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}