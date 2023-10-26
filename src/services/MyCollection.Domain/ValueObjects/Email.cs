using MyCollection.Core.Models;
using System.Text.RegularExpressions;

namespace MyCollection.Domain.ValueObjects;

public sealed class Email : ValueObject
{

    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    public string Value { get; private set; }



    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
