﻿using Microsoft.Extensions.Logging;
using WeatherForcast.Interfaces;
using WeatherForcast.Service;

namespace WeatherForcast
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddSingleton<IApiService, ApiService> ();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
