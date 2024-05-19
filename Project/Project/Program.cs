using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapPost("/users/cadastrar/", ([FromBody] User usuario, [FromServices] AppDbContext context) =>
{
    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(usuario, new ValidationContext(usuario), errors, true))
    {
        return Results.BadRequest(errors);
    }

    User? usuarioBuscado = context.Users.FirstOrDefault(x => x.Nome == usuario.Nome);

    if (usuarioBuscado is null)
    {
        context.Users.Add(usuario);
        context.SaveChanges();
        return Results.Created("Usuario cadastrado com sucesso", usuario);
    }
    return Results.BadRequest("Já existe um usuario com este nome");
});

app.MapGet("/users/listar", ([FromServices] AppDbContext context) =>
{
    if (context.Users.Any()) return Results.Ok(context.Users.ToList());
    return Results.NotFound("Usuarios não encontrados");
});

app.MapDelete("/users/remover/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (usuario is not null)
    {
        context.Users.Remove(usuario);
        context.SaveChanges();
        return Results.Ok("Usuario removido com sucesso");
    }
    return Results.NotFound("Usuario não encontrado");
});

app.MapPut("/users/edit/{nome}", ([FromRoute] string nome, [FromBody] User uAtualizado, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (usuario is not null)
    {
        usuario.Nome = uAtualizado.Nome;
        usuario.Email = uAtualizado.Email;
        usuario.Telefone = uAtualizado.Telefone;
        usuario.Idade = uAtualizado.Idade;
        context.Users.Update(usuario);
        context.SaveChanges();
        return Results.Ok("Usuario editado com sucesso");
    }
    return Results.NotFound("Usuario não encontrado");
});

app.MapGet("/users/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (usuario is null) return Results.NotFound("Usuario não encontrado");
    return Results.Ok(usuario);
});

app.MapPost("/tournament/cadastrar", ([FromBody] Torneio torneio, [FromServices] AppDbContext context) =>
{

    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(torneio, new ValidationContext(torneio), errors, true))
    {
        return Results.BadRequest(errors);
    }

    Torneio? torneioBuscado = context.Torneios.FirstOrDefault(x => x.Nome == torneio.Nome);

    if (torneioBuscado is null)
    {
        context.Torneios.Add(torneio);
        context.SaveChanges();
        return Results.Created("Usuario cadastrado com sucesso", torneio);
    }
    return Results.BadRequest("Já existe um usuario com este nome");
});

app.MapPost("/batalhar/", ([FromBody] Battle battle, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.Find(battle.UserId);
    if (user is null) return Results.NotFound("Usuario nao encontrado");
    battle.User = user;

    Torneio? torneio = context.Torneios.Find(battle.TorneioId);
    if (torneio is null) return Results.NotFound("Torneio nao encontrado");
    battle.Torneio = torneio;

    string resultado = battle.Batalhar(user);

    context.SaveChanges();
    return Results.Ok(resultado);

});

app.Run();
