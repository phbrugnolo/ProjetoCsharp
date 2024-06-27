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

app.MapPost("/users/cadastrar/", ([FromBody] User usuario, [FromServices] AppDbContext ctx) =>
{
    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(usuario, new ValidationContext(usuario), errors, true))
    {
        return Results.BadRequest(errors);
    }

    User? usuarioBuscado = ctx.Users.FirstOrDefault(x => x.UserId == usuario.UserId);

    if (usuarioBuscado is null)
    {
        ctx.Users.Add(usuario);
        ctx.SaveChanges();
        return Results.Created($"/users/buscar/{usuario.UserId}", usuario);
    }
    return Results.BadRequest("Já existe um usuario com este ID");
});

app.MapGet("/users/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.Users.Any()) return Results.Ok(ctx.Users.ToList());
    return Results.NotFound("Usuários não encontrados");
});

app.MapDelete("/users/remover/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    User? usuario = ctx.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is not null)
    {
        ctx.Users.Remove(usuario);
        ctx.SaveChanges();
        return Results.Ok("Usuário removido com sucesso");
    }
    return Results.NotFound("Usuário não encontrado");
});

app.MapPut("/users/edit/{id}", ([FromRoute] string id, [FromBody] User uAtualizado, [FromServices] AppDbContext ctx) =>
{
    User? usuario = ctx.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is not null)
    {
        usuario.Nome = uAtualizado.Nome;
        usuario.Email = uAtualizado.Email;
        usuario.Telefone = uAtualizado.Telefone;
        usuario.Idade = uAtualizado.Idade;
        ctx.Users.Update(usuario);
        ctx.SaveChanges();
        return Results.Ok("Usuário editado com sucesso");
    }
    return Results.NotFound("Usuário não encontrado");
});

app.MapGet("/users/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    User? usuario = ctx.Users.FirstOrDefault(x => x.UserId == id);

    if (usuario is null) return Results.NotFound("Usuário não encontrado");
    return Results.Ok(usuario);
});

app.MapPost("/tournament/cadastrar", ([FromBody] Torneio torneio, [FromServices] AppDbContext ctx) =>
{
    List<ValidationResult> errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(torneio, new ValidationContext(torneio), errors, true))
    {
        return Results.BadRequest(errors);
    }

    Torneio? torneioBuscado = ctx.Torneios.FirstOrDefault(x => x.TorneioId == torneio.TorneioId);

    if (torneioBuscado is null)
    {
        ctx.Torneios.Add(torneio);
        ctx.SaveChanges();
        return Results.Created($"/tournament/buscar/{torneio.TorneioId}", torneio);
    }
    return Results.BadRequest("Já existe um torneio com este ID");
});

app.MapGet("/tournament/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.Torneios.Any()) return Results.Ok(ctx.Torneios.ToList());
    return Results.NotFound("Torneios não encontrados");
});

app.MapDelete("/tournament/remover/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Torneio? torneio = ctx.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is not null)
    {
        ctx.Torneios.Remove(torneio);
        ctx.SaveChanges();
        return Results.Ok("Torneio removido com sucesso");
    }
    return Results.NotFound("Torneio não encontrado");
});

app.MapPut("/tournament/edit/{id}", ([FromRoute] string id, [FromBody] Torneio tAtualizado, [FromServices] AppDbContext ctx) =>
{
    Torneio? torneio = ctx.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is not null)
    {
        torneio.Nome = tAtualizado.Nome;
        torneio.Descricao = tAtualizado.Descricao;
        torneio.Premiacao = tAtualizado.Premiacao;
        ctx.Torneios.Update(torneio);
        ctx.SaveChanges();
        return Results.Ok("Torneio editado com sucesso");
    }
    return Results.NotFound("Torneio não encontrado");
});

app.MapGet("/tournament/buscar/{id}", ([FromRoute] string id, [FromServices] AppDbContext ctx) =>
{
    Torneio? torneio = ctx.Torneios.FirstOrDefault(x => x.TorneioId == id);

    if (torneio is null) return Results.NotFound("Torneio não encontrado");
    return Results.Ok(torneio);
});

app.MapPost("/batalhar", ([FromBody] Battle battle, [FromServices] AppDbContext ctx) =>
{
    User? user = ctx.Users.Find(battle.UserId);
    if (user is null) return Results.NotFound("Usuário não encontrado");
    battle.User = user;

    Torneio? torneio = ctx.Torneios.Find(battle.TorneioId);
    if (torneio is null) return Results.NotFound("Torneio não encontrado");
    battle.Torneio = torneio;

    battle.Batalhar(user);

    ctx.Battles.Add(battle);
    ctx.SaveChanges();
    return Results.Ok(battle);
});

app.MapGet("/batalhas/listar", async ([FromServices] AppDbContext ctx) =>
{
    var battles = await ctx.Battles
                           .Include(b => b.User)
                           .Include(b => b.Torneio)
                           .ToListAsync();

    if (battles.Any()) return Results.Ok(battles);
    return Results.NotFound("Nenhuma batalha encontrada");
});

app.MapGet("/batalhas/last", async ([FromServices] AppDbContext ctx) =>
{
    var lastBattle = await ctx.Battles
                              .Include(b => b.User)
                              .Include(b => b.Torneio)
                              .OrderByDescending(x => x.BattleId)
                              .FirstOrDefaultAsync();

    if (lastBattle != null) return Results.Ok(lastBattle);
    return Results.NotFound("Nenhuma batalha encontrada");
});


app.UseCors("AcessoTotal");
app.Run();
