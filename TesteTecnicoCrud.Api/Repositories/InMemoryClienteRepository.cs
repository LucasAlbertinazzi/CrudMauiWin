using System.Collections.Concurrent;
using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.Api.Repositories;

public class InMemoryClienteRepository : IClienteRepository
{
    private readonly ConcurrentDictionary<int, Cliente> _data = new();
    private int _seq = 0;

    public InMemoryClienteRepository()
    {
        // seed opcional
        Add(new Cliente { Name = "Ana", Lastname = "Silva", Age = 30, Address = "Rua A, 100" });
        Add(new Cliente { Name = "Bruno", Lastname = "Souza", Age = 25, Address = "Av. B, 200" });
    }

    public IEnumerable<Cliente> GetAll() => _data.Values.OrderBy(c => c.Id);

    public Cliente? GetById(int id) => _data.TryGetValue(id, out var c) ? c : null;

    public Cliente Add(Cliente c)
    {
        var id = Interlocked.Increment(ref _seq);
        var novo = new Cliente
        {
            Id = id,
            Name = c.Name,
            Lastname = c.Lastname,
            Age = c.Age,
            Address = c.Address
        };
        _data[id] = novo;
        return novo;
    }

    public bool Update(int id, Cliente c)
    {
        if (!_data.ContainsKey(id)) return false;
        _data[id] = new Cliente
        {
            Id = id,
            Name = c.Name,
            Lastname = c.Lastname,
            Age = c.Age,
            Address = c.Address
        };
        return true;
    }

    public bool Delete(int id) => _data.TryRemove(id, out _);
}
