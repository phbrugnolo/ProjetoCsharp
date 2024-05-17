namespace Project.Models;

public class User
{
    public User(string nome, int idade, int telefone)
    {
        Nome = nome;
        Idade = idade;
        Telefone = telefone;
        Id = Guid.NewGuid().ToString();
        Vitoria = 0;
        Derrota = 0;

    }

    public string? Nome { get; set; }
    public int Idade { get; set; }
    public int Telefone { get; set; }
    public int Vitoria { get; set; }
    public int Derrota { get; set; }
    public string? Id { get; set; }


}
