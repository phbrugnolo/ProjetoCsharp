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
        public string? JogadaMaquina { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "O ID do torneio é obrigatório.")]
        public string? TorneioId { get; set; }
        public Torneio? Torneio { get; set; }

        public void Batalhar(User user)
        {
            string[] opcoes = { "pedra", "papel", "tesoura" };
            Random random = new Random();
            int indiceMaquina = random.Next(0, 3);
            JogadaMaquina = opcoes[indiceMaquina];

            if (Jogada == JogadaMaquina)
            {
                user.Empate += 1;
            }
            else if ((Jogada == "pedra" && JogadaMaquina == "tesoura") ||
                     (Jogada == "papel" && JogadaMaquina == "pedra") ||
                     (Jogada == "tesoura" && JogadaMaquina == "papel"))
            {
                user.Vitoria += 1;
            }
            else
            {
                user.Derrota += 1;
            }
        }
    }
}
