using MyCollection.Core.Messages.Commands;

namespace MyCollection.Core.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }

    public interface IHandlerAsync<T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}
