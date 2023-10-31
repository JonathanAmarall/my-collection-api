using FluentValidation;

namespace MyCollection.Application.Borrower.Commands.CreateBorrower
{
    public class CreateBorrowerCommandValidator : AbstractValidator<CreateBorrowerCommand>
    {
        public CreateBorrowerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(256).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Street).MaximumLength(256).NotEmpty();
            RuleFor(x => x.PostalCode).MaximumLength(8).NotEmpty();
            RuleFor(x => x.City).MaximumLength(256).NotEmpty();
            RuleFor(x => x.Number).MaximumLength(256).NotEmpty();
        }
    }
}
