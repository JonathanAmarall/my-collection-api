using FluentValidation.Results;

namespace MyCollection.Core.Messages.Commands
{
    public class CommandResult<T> : ICommandResult
    {
        public CommandResult(bool isSuccess, string message, T data, ValidationResult? validationResult = null)
        {
            ValidationResult = validationResult;
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }

        public ValidationResult? ValidationResult { get; private set; }
        public string Message { get; private set; }
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public bool IsFailure => !IsSuccess;

        public static ICommandResult Failure(string message, ValidationResult? validationResult)
        {
            return new CommandResult<T>(false, message, default, validationResult);
        }

        public static ICommandResult Success(string message, T data)
        {
            return new CommandResult<T>(true, message, data);
        }
    }
}
