using FluentValidation.Results;
using MyCollection.Core.Contracts;

namespace MyCollection.Core.Messages.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool isSuccess, string message, object data, ValidationResult? validationResult = null)
        {
            ValidationResult = validationResult;
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }

        public ValidationResult? ValidationResult { get; private set; }
        public string Message { get; private set; }
        public bool IsSuccess { get; private set; }
        public object Data { get; private set; }
        public bool IsFailure => !IsSuccess;
    }
}
