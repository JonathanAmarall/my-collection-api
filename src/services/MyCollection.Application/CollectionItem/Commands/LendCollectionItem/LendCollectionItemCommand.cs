using FluentValidation.Results;
using MyCollection.Core.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class LendCollectionItemCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public LendCollectionItemCommand(Guid collectionItemId, Guid borrowerId)
        {
            CollectionItemId = collectionItemId;
            BorrowerId = borrowerId;
        }

        public Guid CollectionItemId { get; private set; }
        public Guid BorrowerId { get; private set; }

        public bool IsValid()
        {
            ValidationResult = new LendCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}