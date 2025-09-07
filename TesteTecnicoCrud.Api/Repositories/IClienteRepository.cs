using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.Api.Repositories;

public interface IClienteRepository
{
    IEnumerable<Cliente> GetAll();
    Cliente? GetById(int id);
    Cliente Add(Cliente c);
    bool Update(int id, Cliente c);
    bool Delete(int id);
}
