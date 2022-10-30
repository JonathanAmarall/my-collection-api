using FluentValidation;
using FluentValidation.Results;
using MyCollection.Domain.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class LendCollectionItemCommand : ICommand
    {
        internal ValidationResult? ValidationResult { get; set; }

        public LendCollectionItemCommand(Guid collectionItemId, Guid? contactId, string? fullName, string? email, string? phone)
        {
            CollectionItemId = collectionItemId;
            ContactId = contactId;
            FullName = fullName;
            Email = email;
            Phone = phone;
        }

        public Guid CollectionItemId { get; private set; }
        public Guid? ContactId { get; private set; }
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }

        public bool IsValidate()
        {
            ValidationResult = new BorrowCollectionItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BorrowCollectionItemCommandValidation : AbstractValidator<LendCollectionItemCommand>
    {
        public BorrowCollectionItemCommandValidation()
        {
            RuleFor(c => c.CollectionItemId)
                .NotEqual(Guid.Empty)
                .WithMessage("Informe um Item corretamente.");

            When(c => c.ContactId == Guid.Empty || c.ContactId == null, () =>
            {
                RuleFor(contact => contact.FullName).NotNull().MaximumLength(100)
                .WithMessage("Nome Completo não pode ser nulo e não pode ultrapassar 100 caracteres.");

                RuleFor(contact => contact.Email).NotNull().EmailAddress().MaximumLength(100)
                .WithMessage("Email não pode ser nulo, deve ser válido e não pode ultrapassar 100 caracteres.");

                RuleFor(contact => contact.Phone).NotNull().MaximumLength(100)
                .WithMessage("Telefone não pode ser nulo e não pode ultrapassar 20 caracteres.");
            });

        }
    }
}
