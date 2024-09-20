using FluentValidation;
using Valid.Test.Application.Commands;

namespace Valid.Test.Application.Validators
{
    public class GravarProtocoloCommandValidator : AbstractValidator<GravarProtocoloCommand>
    {
        public GravarProtocoloCommandValidator()
        {
            RuleFor(p => p.NumeroProtocolo)
                .NotEmpty().WithMessage("O Número do Protocolo é obrigatório.");

            RuleFor(p => p.NumeroVia)
                .NotEmpty().WithMessage("O Número da Via é obrigatório.")
                .GreaterThan(0).WithMessage("O Número da Via deve ser maior que zero.");

            RuleFor(p => p.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter exatamente 11 dígitos e apenas números.");

            RuleFor(p => p.Rg)
                .NotEmpty().WithMessage("O RG é obrigatório.");

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.");

            RuleFor(p => p.NomeMae)
                .MaximumLength(100).WithMessage("O Nome da Mãe não pode exceder 100 caracteres.");

            RuleFor(p => p.NomePai)
                .MaximumLength(100).WithMessage("O Nome do Pai não pode exceder 100 caracteres.");

            RuleFor(p => p.Foto)
                .NotEmpty().WithMessage("A Foto é obrigatória.")
                .Must(f => f.ContentType == "image/jpeg" || f.ContentType == "image/png")
                .WithMessage("A Foto deve estar no formato jpg ou png.");
        }
    }
}
