using Microsoft.Maui;
using Microsoft.Maui;
#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;

using MauiWindow = Microsoft.Maui.Controls.Window;
using WinUIWindow = Microsoft.UI.Xaml.Window;
#endif

namespace TesteTecnicoCrud
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

#if WINDOWS
        protected override MauiWindow CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Created += (_, __) =>
            {
                try
                {
                    // converte para WinUI.Window usando o alias
                    var xamlWin = (WinUIWindow)window.Handler.PlatformView;
                    var hwnd = WindowNative.GetWindowHandle(xamlWin);
                    var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                    var appWindow = AppWindow.GetFromWindowId(windowId);

                    if (appWindow.Presenter is OverlappedPresenter presenter)
                    {
                        presenter.IsResizable = true;
                        presenter.IsMaximizable = true;
                        presenter.Maximize();
                    }
                }
                catch
                {
                    // fallback silencioso
                }
            };

            return window;
        }
#endif

    }
}