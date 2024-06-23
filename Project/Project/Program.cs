using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AcessoTotal",
            builder => builder.
                AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod());
    }
);
var app = builder.Build();

List<User> Users = new List<User>();
List<Torneio> Torneios = new List<Torneio>();
// List<Battle> Battles = new List<Battle>();

app.MapPost("/users/cadastrar/", ([FromBody] User usuario, [FromServices] AppDbContext context) =>
{
    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(usuario, new ValidationContext(usuario), errors, true))
    {
        return Results.BadRequest(errors);
    }

    User? usuarioBuscado = context.Users.FirstOrDefault(x => x.UserId == usuario.UserId);

    if (usuarioBuscado is null)
    {
        context.Users.Add(usuario);
        context.SaveChanges();
        return Results.Created("Usuário cadastrado com sucesso", usuario);
    }
    return Results.BadRequest("Já existe um usuario com este ID");
});

app.MapGet("/users/listar", ([FromServices] AppDbContext context) =>
{
    if (context.Users.Any()) return Results.Ok(context.Users.ToList());
    return Results.NotFound("Usuários não encontrados");
});

app.MapDelete("/users/remover/{id}", ([FromRoute] string id, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is not null)
    {
        context.Users.Remove(usuario);
        context.SaveChanges();
        return Results.Ok(context.Users.ToList());
    }
    return Results.NotFound("Usuário não encontrado");
});

app.MapPut("/users/edit/{id}", ([FromRoute] string id, [FromBody] User uAtualizado, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is not null)
    {
        usuario.Nome = uAtualizado.Nome;
        usuario.Email = uAtualizado.Email;
        usuario.Telefone = uAtualizado.Telefone;
        usuario.Idade = uAtualizado.Idade;
        context.Users.Update(usuario);
        context.SaveChanges();
        return Results.Ok("Usuário editado com sucesso");
    }
    return Results.NotFound("Usuário não encontrado");
});

app.MapGet("/users/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext context) =>
{
    User? usuario = context.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is null) return Results.NotFound("Usuário não encontrado");
    return Results.Ok(usuario);
});

app.MapPost("/tournament/cadastrar", ([FromBody] Torneio torneio, [FromServices] AppDbContext context) =>
{

    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(torneio, new ValidationContext(torneio), errors, true))
    {
        return Results.BadRequest(errors);
    }

    Torneio? torneioBuscado = context.Torneios.FirstOrDefault(x => x.TorneioId == torneio.TorneioId);

    if (torneioBuscado is null)
    {
        context.Torneios.Add(torneio);
        context.SaveChanges();
        return Results.Created("Usuário cadastrado com sucesso", torneio);
    }
    return Results.BadRequest("Já existe um usuario com este ID");
});

app.MapGet("/tournament/listar", ([FromServices] AppDbContext context) =>
{
    if (context.Torneios.Any()) return Results.Ok(context.Torneios.ToList());
    return Results.NotFound("Torneios não encontrados");
});

app.MapDelete("/tournament/remover/{id}", ([FromRoute] string id, [FromServices] AppDbContext context) =>
{
    Torneio? torneio = context.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is not null)
    {
        context.Torneios.Remove(torneio);
        context.SaveChanges();
        return Results.Ok(context.Torneios.ToList());
    }
    return Results.NotFound("Torneio não encontrado");
});

app.MapPut("/tournament/edit/{id}", ([FromRoute] string id, [FromBody] Torneio tAtualizado, [FromServices] AppDbContext context) =>
{
    Torneio? torneio = context.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is not null)
    {
        torneio.Nome = tAtualizado.Nome;
        torneio.Descricao = tAtualizado.Descricao;
        torneio.Premiacao = tAtualizado.Premiacao;
        context.Torneios.Update(torneio);
        context.SaveChanges();
        return Results.Ok("Torneio editado com sucesso");
    }
    return Results.NotFound("Torneio não encontrado");
});

app.MapGet("/tournament/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext context) =>
{
    Torneio? torneio = context.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is null) return Results.NotFound("Torneio não encontrado");
    return Results.Ok(torneio);
});

app.MapPost("/batalhar", ([FromBody] Battle battle, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.Find(battle.UserId);
    if (user is null) return Results.NotFound("Usuário não encontrado");
    battle.User = user;

    Torneio? torneio = context.Torneios.Find(battle.TorneioId);
    if (torneio is null) return Results.NotFound("Torneio não encontrado");
    battle.Torneio = torneio;

    string resultado = battle.Batalhar(user);

    // context.Battles.Add(battle);
    context.SaveChanges();
    return Results.Ok(resultado);

});

// app.MapGet("/batalhas/listar", ([FromServices] AppDbContext context) =>
// {
//     if (context.Battles.Any()) return Results.Ok(context.Battles.ToList());
//     return Results.NotFound("Nenhuma batalha encontrada");
// });

app.UseCors("AcessoTotal");
app.Run();
