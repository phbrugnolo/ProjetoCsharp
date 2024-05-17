namespace Project.Models;

public class Torneio
{
    public Torneio(List<User> users, Battle batalha)
    {
        Users = users;
        Batalha = batalha;
    }

    public List<User> Users { get; set; }
    public Battle Batalha { get; set; }
}
