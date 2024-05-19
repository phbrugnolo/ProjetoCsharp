using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Battle
{
    public Battle(string jogada, string userId, string torneioId)
    {
        Jogada = jogada.ToLower().Trim();
        UserId = userId;
        TorneioId = torneioId;
    }

    public string Batalhar(User user)
    {
        string[] opcoes = { "pedra", "papel", "tesoura" };
        Random random = new Random();
        int indiceMaquina = random.Next(0, 3);
        string jogadaMaquina = opcoes[indiceMaquina];

        if (Jogada == jogadaMaquina)
        {
            return "Empate!";
        }
        else if ((Jogada == "pedra" && jogadaMaquina == "tesoura") ||
                 (Jogada == "papel" && jogadaMaquina == "pedra") ||
                 (Jogada == "tesoura" && jogadaMaquina == "papel"))
        {
            user.Vitoria += 1;
            return "Você venceu!";
        }
        else
        {
            user.Derrota += 1;
            return "Você perdeu!";
        }
    }

    [Key]
    public string BattleId { get; set; } = Guid.NewGuid().ToString();
    public string Jogada { get; set; }
    public string UserId { get; set; }
    public User? User { get; set; }
    public string TorneioId { get; set; }
    public Torneio? Torneio { get; set; }
}
