using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Compras.Services;
using Compras.MVVM.Views;
using Compras.MVVM.ViewModels;
namespace Compras;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<Database>();

        builder.Services.AddTransient<CreateListViewModel>();
        builder.Services.AddTransient<CreateListPage>();
        return builder.Build();
	}
}
