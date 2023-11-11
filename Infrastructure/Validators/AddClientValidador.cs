using CarteiraClientes.ViewModels.Client;
using FluentValidation;

namespace CarteiraClientes.Infrastructure.Validators;

public class AddClientValidador : AbstractValidator<AddClientViewModel>
{
    public AddClientValidador()
    {
        RuleFor(c => c.FullName)
            .NotEmpty()
            .WithMessage("Nome do Cliente deve ser informado!")
            .NotNull()
            .WithMessage("Nome do Cliente deve ser informado!")
            .MinimumLength(2)
            .WithMessage("Nome do Cliente deve conter ao menos 2 letras!")
            .MaximumLength(100)
            .WithMessage("Nome do Cliente deve conter no máxima 100 letras!");

        RuleFor(c => c.Age)
            .NotEmpty()
            .WithMessage("Idade do Cliente deve ser informada!")
            .NotNull()
            .WithMessage("Idade do Cliente deve ser informada!")
            .Must(age => age >= 18)
            .WithMessage("O Cliente deve ser maior de idade!");

        RuleFor(c => c.Document)
            .NotEmpty()
            .WithMessage("Documento (RG) do Cliente deve ser informado!")
            .NotNull()
            .WithMessage("Documento (RG) do Cliente deve ser informado!");
        
        
    }
}