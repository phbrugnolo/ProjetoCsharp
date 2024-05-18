namespace Project.Models
{
    public class Torneio
    {
        public Torneio() { }

        public Torneio(string nome, ICollection<User> users, Battle batalha)
        {
            Nome = nome;
            Users = users;
            Batalha = batalha;
            TorneioId = Guid.NewGuid().ToString();
        }

        public string? Nome { get; set; }
        public Battle Batalha { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public string TorneioId { get; set; }
    }
}
