using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Torneio
    {
        public Torneio(string nome, string descricao, float premiacao)
        {
            Nome = nome;
            Descricao = descricao;
            Premiacao = premiacao;
        }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public float Premiacao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public string TorneioId { get; set; } = Guid.NewGuid().ToString();
    }
}
