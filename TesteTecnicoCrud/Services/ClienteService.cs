using System.Collections.ObjectModel;
using System.Text.Json;
using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.Services
{
    public class ClienteService : IClienteService
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _jsonOpts = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };

        public ObservableCollection<Cliente> Clientes { get; } = new();

        public ClienteService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "clientes_cache.json");
            _ = LoadAsync();
        }

        public void Add(Cliente cliente)
        {
            MainThread.BeginInvokeOnMainThread(() => Clientes.Add(cliente));
            _ = PersistAsync();
        }

        public void Update(int index, Cliente cliente)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (index >= 0 && index < Clientes.Count)
                    Clientes[index] = cliente;
            });
            _ = PersistAsync();
        }

        public void Update(Cliente original, Cliente atualizado)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var idx = Clientes.IndexOf(original);
                if (idx >= 0)
                {
                    atualizado.Id = original.Id;
                    Clientes[idx] = atualizado;
                }
            });
            _ = PersistAsync();
        }

        public void Delete(Cliente cliente)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (cliente is not null) Clientes.Remove(cliente);
            });
            _ = PersistAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    MainThread.BeginInvokeOnMainThread(() => Clientes.Clear());
                    await PersistAsync();
                    return;
                }

                await using var fs = File.OpenRead(_filePath);
                var list = await JsonSerializer.DeserializeAsync<List<Cliente>>(fs, _jsonOpts) ?? new();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Clientes.Clear();
                    foreach (var c in list) Clientes.Add(c);
                });
            }
            catch
            {
            }
        }

        private async Task PersistAsync()
        {
            try
            {
                var snapshot = Clientes.ToList();
                Directory.CreateDirectory(FileSystem.AppDataDirectory);
                await using var fs = File.Create(_filePath);
                await JsonSerializer.SerializeAsync(fs, snapshot, _jsonOpts);
            }
            catch
            {
            }
        }
    }
}
