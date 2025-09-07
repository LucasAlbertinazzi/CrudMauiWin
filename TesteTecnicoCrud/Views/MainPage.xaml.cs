using TesteTecnicoCrud.ViewModels;

namespace TesteTecnicoCrud.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}