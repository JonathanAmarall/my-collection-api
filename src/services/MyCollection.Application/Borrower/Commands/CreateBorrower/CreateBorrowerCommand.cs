using FluentValidation.Results;
using MyCollection.Core.Messages.Commands;
using Newtonsoft.Json;

namespace MyCollection.Application.Borrower.Commands.CreateBorrower
{
    public class CreateBorrowerCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; private set; }

        public CreateBorrowerCommand(string firstName, string lastName, string email, string phone, string street, string postalCode, string city, string number)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Street = street;
            PostalCode = postalCode;
            City = city;
            Number = number;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string Number { get; private set; }

        public bool IsValid()
        {
            ValidationResult = new CreateBorrowerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
