using FluentValidation.Results;
using MyCollection.Core.Messages.Commands;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class LendCollectionItemCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public LendCollectionItemCommand(Guid collectionItemId, Guid borrowerId, int rentQuantity)
        {
            CollectionItemId = collectionItemId;
            BorrowerId = borrowerId;
            RentQuantity = rentQuantity;
        }

        public Guid CollectionItemId { get; private set; }
        public Guid BorrowerId { get; private set; }
        public int RentQuantity { get; private set; }

        public bool IsValid()
        {
            ValidationResult = new LendCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}