using System.Collections.ObjectModel;
using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.Services
{
    public interface IClienteService
    {
        ObservableCollection<Cliente> Clientes { get; }
        void Add(Cliente cliente);
        void Update(Cliente original, Cliente atualizado);
        void Delete(Cliente cliente);
    }
}
