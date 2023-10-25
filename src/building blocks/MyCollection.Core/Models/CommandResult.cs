using FluentValidation.Results;
using MyCollection.Core.Contracts;

namespace MyCollection.Core.Models
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message, object data, ValidationResult? validationResult = null)
        {
            ValidationResult = validationResult;
            Message = message;
            Success = success;
            Data = data;
        }

        public ValidationResult? ValidationResult { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
    }
}
