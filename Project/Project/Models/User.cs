using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class User
{
    public User(string nome, string email, string telefone, int idade)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Idade = idade;
        Vitoria = 0;
        Derrota = 0;
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;

    }

    [Required(ErrorMessage = "Este campo e obrigatorio")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "Este campo e obrigatorio")]
    public int Idade { get; set; }
    [Required(ErrorMessage = "Este campo e obrigatorio")]
    public string? Telefone { get; set; }
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    public string Email { get; set; }
    public int Vitoria { get; set; }
    public int Derrota { get; set; }
    public string Id { get; set; }
    public DateTime CriadoEm { get; set; }


}
