using MyCollection.Core.Models;

namespace MyCollection.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    protected Address() { }

    public Address(string street, string postalCode, string city, string number)
    {
        Street = street;
        PostalCode = postalCode;
        City = city;
        Number = number;
    }

    public string Street { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public string Number { get; private set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return PostalCode;
        yield return City;
        yield return Number;
    }
}