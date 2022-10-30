using FluentValidation;
using FluentValidation.Results;
using MyCollection.Domain.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class AddLocationInCollectionItemCommand : ICommand
    {

        [JsonIgnore]
        internal ValidationResult? ValidationResult { get; set; }
        public AddLocationInCollectionItemCommand(Guid collectionItemId, Guid locationId)
        {
            CollectionItemId = collectionItemId;
            LocationId = locationId;
        }


        public Guid CollectionItemId { get; set; }
        public Guid LocationId { get; set; }

        public bool IsValidate()
        {
            ValidationResult = new AddLocationInCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


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
