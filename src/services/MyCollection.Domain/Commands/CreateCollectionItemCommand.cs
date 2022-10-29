using FluentValidation;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using FluentValidation.Results;

namespace MyCollection.Domain
{
    public class CreateCollectionItemCommand : ICommand
    {
        public ValidationResult? ValidationResult { get; protected set; }
        public CreateCollectionItemCommand(string title, string autor, int quantity, string? edition, EType itemType, Guid locationId)
        {
            Title = title;
            Autor = autor;
            Quantity = quantity;
            Edition = edition;
            ItemType = itemType;
            LocationId = locationId;
        }

        public string Title { get; set; }
        public string Autor { get; set; }
        public int Quantity { get; set; }
        public string? Edition { get; set; }
        public EType ItemType { get; private set; }
        public Guid LocationId { get;  set; }

        public bool IsValidate()
        {
            ValidationResult = new CreateCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

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

            RuleFor(e => e.LocationId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("Informe uma localização corretamente");
        }
    }
}
