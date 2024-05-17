using System.ComponentModel.DataAnnotations;
using Project.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();


List<User> Users = new List<User>();


app.MapPost("/api/user/cadastrar/", ([FromBody] User usuario, [FromServices] AppDbContext context) =>
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
    //Endpoint com várias linhas de código 
    User? user = context.Users.FirstOrDefault(x => x.Nome == nome);

    if (user is null) return Results.NotFound("Usuário não Encotrado");
    return Results.Ok(user);

});



app.Run();