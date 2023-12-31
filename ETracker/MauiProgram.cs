﻿using Microsoft.Extensions.Logging;
using Telerik.Maui.Controls.Compatibility;

namespace ETracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseTelerik()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                fonts.AddFont("Lato-Black.ttf", "LatoBlack");
                fonts.AddFont("Lato-BlackItalic.ttf", "LatoBlackItalic");
                fonts.AddFont("Lato-Bold.ttf", "LatoBold");
                fonts.AddFont("Lato-BoldItalic.ttf", "LatoBoldItalic");
                fonts.AddFont("Lato-Italic.ttf", "LatoItalic");
                fonts.AddFont("Lato-Light.ttf", "LatoLight");
                fonts.AddFont("Lato-LightItalic.ttf", "LatoLightItalic");
                fonts.AddFont("Lato-Regular.ttf", "LatoRegular");
                fonts.AddFont("Lato-Thin.ttf", "LatoThin");
                fonts.AddFont("Lato-ThinItalic.ttf", "LatoThinItalic");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

