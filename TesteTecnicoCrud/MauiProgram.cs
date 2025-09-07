using Microsoft.Extensions.Logging;
using TesteTecnicoCrud.Services;
using TesteTecnicoCrud.ViewModels;

namespace TesteTecnicoCrud
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IClienteService, ClienteService>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<ClienteFormViewModel>();

            return builder.Build();
        }
    }
}
