using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Supabase;

namespace to_do;

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

        // 1. Load the assembly
        var assembly = Assembly.GetExecutingAssembly();
        
        // 2. Load the JSON stream (Ensure the filename matches exactly!)
        using var stream = assembly.GetManifestResourceStream("to_do.appsettings.json");
        
        if (stream == null)
        {
            throw new Exception("Could not find appsettings.json. Did you set Build Action to 'Embedded Resource'?");
        }

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        
        // 3. Extract Supabase credentials
        var url = config["Supabase:Url"];
        var key = config["Supabase:AnonKey"];

        var options = new SupabaseOptions { AutoRefreshToken = true };
        builder.Services.AddSingleton(new Supabase.Client(url, key, options));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // 4. Return ONCE at the very end
        return builder.Build();
    }
}