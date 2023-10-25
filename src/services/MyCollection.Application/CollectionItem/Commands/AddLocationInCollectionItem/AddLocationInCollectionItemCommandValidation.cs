using FluentValidation;

namespace MyCollection.Domain.Commands
{
    public class AddLocationInCollectionItemCommandValidation : AbstractValidator<AddLocationInCollectionItemCommand>
    {
        public AddLocationInCollectionItemCommandValidation()
        {
            RuleFor(e => e.CollectionItemId)
            .NotEqual(Guid.Empty)
            .WithMessage("Informe uma Item corretamente.");

            RuleFor(e => e.LocationId)
            .NotEqual(Guid.Empty)
            .WithMessage("Informe uma localização corretamente.");
        }
    }
}
