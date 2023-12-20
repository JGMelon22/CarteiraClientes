using CarteiraClientes.ViewModels.Company;

namespace CarteiraClientes.Infrastructure.Validators;

public class CompanyValidator : AbstractValidator<CompanyInputViewModel>
{
    public CompanyValidator()
    {
        // Validar data 
        var dataValida = DateTime.Now;

        // Minimum gross revenue
        var minimumGrossRevenue = 100.00M;

        RuleFor(c => c.CompanyName)
            .NotEmpty()
            .WithMessage("Nome da empresa deve ser informado!")
            .NotNull()
            .WithMessage("Nome da empresa deve ser informado!")
            .MinimumLength(2)
            .WithMessage("Nome da empresa deve conter ao menos 2 caracteres")
            .MaximumLength(100)
            .WithMessage("Nome da empresa não deve exceder 100 caracteres");

        RuleFor(c => c.FoundedDate)
            .NotEmpty()
            .WithMessage("Data de criação da empresa deve ser informada!")
            .NotNull()
            .WithMessage("Data de criação da empresa deve ser informada!")
            .Must(foundedDate => foundedDate < dataValida);

        RuleFor(c => c.Revenue)
            .NotEmpty()
            .WithMessage("Receita bruta da empresa deve ser informada!")
            .NotNull()
            .WithMessage("Receita bruta da empresa deve ser informada!")
            .Must(revenue => revenue > minimumGrossRevenue)
            .WithMessage($"Receita bruta da empresa deve ser superior a {minimumGrossRevenue:C2}");
    }
}