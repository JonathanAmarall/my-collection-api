namespace MyCollection.Core.Exceptions;

public class PropertyQueryNullException : Exception
{
    public PropertyQueryNullException(string message) : base(message)
    {
    }
}