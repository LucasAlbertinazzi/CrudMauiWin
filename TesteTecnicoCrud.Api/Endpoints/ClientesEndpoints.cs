using Microsoft.AspNetCore.Mvc;
using TesteTecnicoCrud.Api.Repositories;
using TesteTecnicoCrud.Shared.Models;

namespace TesteTecnicoCrud.Api.Endpoints;

public static class ClientesEndpoints
{
    private const string BaseRoute = "/clientes";

    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseRoute, ([FromServices] IClienteRepository repo)
            => Results.Ok(repo.GetAll()))
            .WithTags("Clientes");

        app.MapGet($"{BaseRoute}/{{id:int}}", ([FromServices] IClienteRepository repo, int id)
            => repo.GetById(id) is { } c ? Results.Ok(c) : Results.NotFound())
            .WithTags("Clientes");

        app.MapPost(BaseRoute, ([FromServices] IClienteRepository repo, [FromBody] Cliente dto) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Lastname))
                return Results.BadRequest("Name e Lastname são obrigatórios.");
            var novo = repo.Add(dto);
            return Results.Created($"{BaseRoute}/{novo.Id}", novo);
        }).WithTags("Clientes");

        app.MapPut($"{BaseRoute}/{{id:int}}", ([FromServices] IClienteRepository repo, int id, [FromBody] Cliente dto)
            => repo.Update(id, dto) ? Results.NoContent() : Results.NotFound())
            .WithTags("Clientes");

        app.MapDelete($"{BaseRoute}/{{id:int}}", ([FromServices] IClienteRepository repo, int id)
            => repo.Delete(id) ? Results.NoContent() : Results.NotFound())
            .WithTags("Clientes");

        return app;
    }
}
