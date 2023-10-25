using MyCollection.Core.Contracts;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace MyCollection.Domain
{
    public class CreateCollectionItemCommand : ICommand
    {
        [JsonIgnore]
        internal ValidationResult? ValidationResult { get;  set; }

        public CreateCollectionItemCommand(string title, string autor, int quantity, string? edition, int itemType)
        {
            Title = title;
            Autor = autor;
            Quantity = quantity;
            Edition = edition;
            ItemType = itemType;
        }

        public string Title { get; set; }
        public string Autor { get; set; }
        public int Quantity { get; set; }
        public string? Edition { get; set; }
        public int ItemType { get; private set; }


        public bool IsValid()
        {
            ValidationResult = new CreateCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
