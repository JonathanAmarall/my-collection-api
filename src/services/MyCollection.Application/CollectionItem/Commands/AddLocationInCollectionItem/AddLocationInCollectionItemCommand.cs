using FluentValidation.Results;
using MyCollection.Core.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class AddLocationInCollectionItemCommand : ICommand
    {

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }
        public AddLocationInCollectionItemCommand(Guid collectionItemId, Guid locationId)
        {
            CollectionItemId = collectionItemId;
            LocationId = locationId;
        }


        public Guid CollectionItemId { get; set; }
        public Guid LocationId { get; set; }

        public bool IsValid()
        {
            ValidationResult = new AddLocationInCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
