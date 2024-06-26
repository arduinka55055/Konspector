﻿using Konspector.Misc;
using Konspector.Storage;
using Microsoft.Extensions.Logging;

namespace Konspector;

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
		ConfigureApp.Configure(builder);
		builder.Services.AddMauiBlazorWebView();
		return builder.Build();
	}
}
