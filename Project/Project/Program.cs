using System.ComponentModel.DataAnnotations;
using Project.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();


List<User> Users = new List<User>();
List<Torneio> Torneios = new List<Torneio>();


app.MapPost("/api/user/cadastrar", ([FromBody] User usuario, [FromServices] AppDbContext context) =>
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
        return Results.Created("Usuário cadastrado com sucessos", usuario);
    }
    return Results.BadRequest("Já existe um usuário com este nome");

});

app.MapGet("/api/user/listar", ([FromServices] AppDbContext context) =>
{

    if (context.Users.Any()) return Results.Ok(context.Users.ToList());
    return Results.NotFound("Usuário não encontrado");

});

app.MapDelete("/api/user/remover/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (user is not null)
    {
        context.Users.Remove(user);
        context.SaveChanges();
        return Results.Ok("Usuário removido com suceso");
    }

    return Results.NotFound("Usuário não Encontrado");
});

app.MapPut("/api/user/edit/{nome}", ([FromRoute] string nome, [FromBody] User uAtualizado, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (user is not null)
    {
        user.Nome = uAtualizado.Nome;
        context.Users.Update(user);
        context.SaveChanges();
        return Results.Ok("Produto editado com suceso");

    }
    return Results.NotFound("Produto não Encotrado");
});

app.MapGet("/api/user/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) =>
{ 
    User? user = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (user is null) return Results.NotFound("Usuário não Encotrado");
    return Results.Ok(user);

});

app.MapPost("/api/tournament/cadastrar", ([FromBody] Torneio torneio, [FromServices] AppDbContext context) =>
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
        return Results.Created("Torneio cadastrado com sucessos", torneio);
    }
    return Results.BadRequest("Já existe um torneio com este nome");

});

app.MapPost("/batalhar", async (AppDbContext context, string userName, string torneioId, string jogada) =>
{
    // Find the user by name
    User? user = context.Users.FirstOrDefault(x => x.Nome == userName);
    if (user is null)
        return Results.NotFound("Usuário não encontrado");

    Torneio? torneio = context.Torneios.FirstOrDefault(x => x.TorneioId == torneioId);
    if (torneio is null)
        return Results.NotFound("Torneio não encontrado");

    if (!torneio.Users.Contains(user))
        return Results.BadRequest("O usuário não faz parte do torneio especificado");

    var battle = new Battle(jogada);
    
    var resultado = battle.Batalhar(user);

    await context.SaveChangesAsync();

    return Results.Ok(new { resultado, user.Vitoria, user.Derrota });
});




app.Run();