using Konspector.Misc;
using Microsoft.Extensions.Logging;

namespace Konspector;
public static class ConfigureApp{
    public static void Configure(MauiAppBuilder builder){
        //use Microsoft.Extensions.Logging
        builder.Services.AddLogging(logging => logging.AddConsole());
        //use Konspector.Misc.Settings
        builder.Services.AddSingleton<Settings>();
        #if DEBUG
    builder.Services.AddBlazorWebViewDeveloperTools();
    builder.Logging.AddDebug();
#endif
    }
}