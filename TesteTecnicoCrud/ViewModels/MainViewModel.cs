using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TesteTecnicoCrud.Services;
using TesteTecnicoCrud.Shared.Models;
using TesteTecnicoCrud.Views;

namespace TesteTecnicoCrud.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IClienteService _service;

        public ObservableCollection<Cliente> Clientes => _service.Clientes;

        [ObservableProperty] 
        private Cliente? selectedCliente;

        private bool CanOperate => SelectedCliente is not null;

        public MainViewModel(IClienteService service)
        {
            _service = service;
        }

        partial void OnSelectedClienteChanged(Cliente? oldValue, Cliente? newValue)
        {
            DeleteCommand.NotifyCanExecuteChanged();
            EditCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void Add() => OpenForm(null);

        [RelayCommand(CanExecute = nameof(CanOperate))]
        private void Edit()
        {
            if (SelectedCliente is null) return;
            OpenForm(SelectedCliente);
        }

        [RelayCommand(CanExecute = nameof(CanOperate))]
        private async Task Delete()
        {
            if (SelectedCliente is null)
            {
                await Application.Current!.MainPage.DisplayAlert("Atenção", "Selecione um cliente para excluir.", "OK");
                return;
            }

            var nome = $"{SelectedCliente.Name} {SelectedCliente.Lastname}".Trim();
            var ok = await Application.Current!.MainPage.DisplayAlert(
                "Confirmação", $"Excluir {nome}?", "Sim", "Não");

            if (!ok) return;

            _service.Delete(SelectedCliente);
            SelectedCliente = null; // <- importante
        }


        private void OpenForm(Cliente? cliente)
        {
#if WINDOWS
            var page = new ClienteFormPage();

            var win = new Window(page)
            {
                Title = cliente is null ? "Novo Cliente" : "Editar Cliente",
                Width = 520,
                Height = 480
            };

            var vm = new ClienteFormViewModel(_service, win, cliente);
            page.BindingContext = vm;

            Application.Current.OpenWindow(win);
#endif
        }

    }
}
