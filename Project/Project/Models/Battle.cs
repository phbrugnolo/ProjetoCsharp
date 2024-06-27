using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Battle
    {
        public string BattleId { get; set; } = Guid.NewGuid().ToString();
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A jogada é obrigatória.")]
        public string? Jogada { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "O ID do torneio é obrigatório.")]
        public string? TorneioId { get; set; }
        public Torneio? Torneio { get; set; }

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
    }
}
