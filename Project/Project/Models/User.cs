namespace Project;

public class User
{
    public User(string nome)
    {
        Nome = nome;
        Id = Guid.NewGuid().ToString();
        Vitoria = 0;
        Derrota = 0;

    }

    public string? Nome { get; set; }
    public int Vitoria { get; set; }
    public int Derrota { get; set; }
    public string? Id { get; set; }


}
