using FluentValidation;

namespace MyCollection.Domain
{
    public class CreateCollectionItemCommandValidation : AbstractValidator<CreateCollectionItemCommand>
    {
        public CreateCollectionItemCommandValidation()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Por favor, verifique se o título foi informado.")
                .Length(3, 100).WithMessage("O Título deve ter entre 3 e 100 caracteres.");

            RuleFor(e => e.Autor)
               .Length(3, 100).WithMessage("Autor deve ter entre 3 e 100 caracteres.");

            RuleFor(e => e.Quantity)
                .NotNull().WithMessage("Quantidade deve ser maior do que 1.")
                .GreaterThan(0);

            RuleFor(e => e.ItemType)
                .IsInEnum().WithMessage("Por favor, informe um tipo de Item válido");
        }
    }
}
