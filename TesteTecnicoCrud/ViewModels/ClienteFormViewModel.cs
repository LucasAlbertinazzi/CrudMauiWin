using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TesteTecnicoCrud.Services;
using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.ViewModels;

public partial class ClienteFormViewModel : ObservableObject
{
    private readonly IClienteService _service;
    private readonly Window _window;

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string lastname = string.Empty;
    [ObservableProperty] private int age;
    [ObservableProperty] private string address = string.Empty;

    private readonly Cliente? _original;

    public bool IsEdit => _original is not null;

    public ClienteFormViewModel(IClienteService service, Window window, Cliente? cliente)
    {
        _service = service;
        _window = window;
        _original = cliente;

        if (cliente is not null)
        {
            Name = cliente.Name;
            Lastname = cliente.Lastname;
            Age = cliente.Age;
            Address = cliente.Address;
        }
    }


    [RelayCommand]
    private void Cancel()
        => Application.Current?.CloseWindow(_window);

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Lastname))
        {
            await Application.Current!.MainPage.DisplayAlert("Atenção", "Informe nome e sobrenome.", "OK");
            return;
        }

        var novo = new Cliente
        {
            Name = Name.Trim(),
            Lastname = Lastname.Trim(),
            Age = Age,
            Address = Address.Trim()
        };

        if (IsEdit) _service.Update(_original, novo);
        else _service.Add(novo);

        Application.Current?.CloseWindow(_window);
    }
}
