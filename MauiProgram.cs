using CloudSparkMAUI.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace CloudSparkMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .Services
                .AddSingleton<FirebaseAuthService>()
                .AddSingleton<FirebaseFunctionService>()
                .AddSingleton<FirebaseDatabaseService>()
                .AddSingleton<FirebaseStorageService>();

            builder.Services.AddMauiBlazorWebView();

            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
