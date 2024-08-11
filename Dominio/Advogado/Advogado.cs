using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    [Serializable]
    public class Advogado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senioridade é obrigatória.")]
        public Senioridade Senioridade { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        public Endereco Endereco { get; set; }
    }
}
