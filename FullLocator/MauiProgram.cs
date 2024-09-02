using FullLocator.Models;
using FullLocator.Models.Armazenamento;
using FullLocator.Views.Menu;
#if ANDROID
using FullLocator.Platforms.Android;
#endif
using Microsoft.Extensions.Logging;

namespace FullLocator
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
            builder.Services.AddSingleton<ViewMenu>();

#if ANDROID
   builder.Services.AddTransient<IServiceTest, DemoServices>();
#endif

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IDataService, DataService>();

            return builder.Build();
        }
    }
}
