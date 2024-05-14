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

    private string? Nome { get; set; }
    private int Vitoria { get; set; }
    private int Derrota { get; set; }
    private string? Id { get; set; }


}
