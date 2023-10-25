using FluentValidation;

namespace MyCollection.Domain.Commands
{
    public class CreateLocationCommandValidation : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidation()
        {
            RuleFor(e => e.Initials)
               .NotEmpty().WithMessage("Por favor, verifique se a Sigla foi informado.")
               .Length(3, 10).WithMessage("A Sigla deve ter entre 3 e 10 caracteres.");

            RuleFor(e => e.Description)
               .Length(3, 100).WithMessage("Descrição deve ter entre 3 e 100 caracteres.");

            RuleFor(e => e.ParentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Informe uma localização pai corretamente");
        }
    }
}
