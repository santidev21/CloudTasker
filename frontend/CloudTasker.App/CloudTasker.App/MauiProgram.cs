using CloudTasker.App.Services;
using CloudTasker.App.ViewModels;
using CloudTasker.App.Views;
using Microsoft.Extensions.Logging;

namespace CloudTasker.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Dependencies
            builder.Services.AddSingleton<TaskService>();
            builder.Services.AddSingleton<TasksViewModel>();
            builder.Services.AddTransient<TasksPage>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<TaskDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
