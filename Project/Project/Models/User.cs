using Project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class User
    {
        public User(string nome, string email, string telefone, int idade)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Idade = idade;
        }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        public int Vitoria { get; set; } = 0;
        public int Derrota { get; set; } = 0;

        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public string UserId { get; set; } = Guid.NewGuid().ToString();
    }
}
