using MyCollection.Core.Exceptions;
using MyCollection.Core.Models;
using System.Text.RegularExpressions;

namespace MyCollection.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new DomainException("Email does not null or empty");
        }

        if (!EmailFormatRegex.Value.IsMatch(email))
        {
            throw new DomainException("Email has invalid format.");
        }

        return new Email(email);
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
