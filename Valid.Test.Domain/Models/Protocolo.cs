using System.ComponentModel.DataAnnotations;
using Valid.Test.Domain.Models.Base;

namespace Valid.Test.Domain.Models
{
    public class Protocolo : Entity<Guid>
    {
        [Required(ErrorMessage = "O Número do Protocolo é obrigatório.")]
        public string? NumeroProtocolo { get; set; }

        [Required(ErrorMessage = "O Número da Via do documento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Número da Via deve ser maior que zero.")]
        public int NumeroVia { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório.")]
        public string? Rg { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string? Nome { get; set; }

        public string? NomeMae { get; set; }

        public string? NomePai { get; set; }

        [Required(ErrorMessage = "A Foto é obrigatória.")]
        public byte[]? Foto { get; set; }
    }
}