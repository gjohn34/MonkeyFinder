using Microsoft.Extensions.DependencyInjection;
using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder;

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

		// Common API
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services
			.AddSingleton<MonkeyService>()
			.AddSingleton<MonkeysViewModel>()
			.AddSingleton<MainPage>();

		builder.Services
			.AddTransient<MonkeyDetailsViewModel>()
			.AddTransient<DetailsPage>();

        return builder.Build();
	}
}
