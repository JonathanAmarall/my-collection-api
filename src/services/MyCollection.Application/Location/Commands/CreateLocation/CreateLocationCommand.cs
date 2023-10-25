using FluentValidation.Results;
using MyCollection.Core.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class CreateLocationCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public CreateLocationCommand(string initials, string description, Guid? parentId)
        {
            Initials = initials;
            Description = description;
            ParentId = parentId;
        }

        public string Initials { get; private set; }
        public string Description { get; private set; }
        public Guid? ParentId { get; private set; }

        public bool IsValid()
        {
            ValidationResult = new CreateLocationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
