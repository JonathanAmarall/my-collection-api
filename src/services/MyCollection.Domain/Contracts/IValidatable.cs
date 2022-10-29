using FluentValidation.Results;

namespace MyCollection.Domain.Contracts
{
    public interface IValidatable
    {
        bool IsValidate();
    }
}
