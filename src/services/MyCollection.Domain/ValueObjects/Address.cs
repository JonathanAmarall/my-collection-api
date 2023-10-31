using MyCollection.Core.Exceptions;
using MyCollection.Core.Models;

namespace MyCollection.Domain.ValueObjects;

public sealed class Address : ValueObject
{
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

    public static Address Create(string street, string postalCode, string city, string number)
    {
        if (string.IsNullOrEmpty(street))
        {
            throw new DomainException("The Street property cannot be null");
        }

        if (string.IsNullOrEmpty(postalCode))
        {
            throw new DomainException("The postalCode property cannot be null");
        }

        if (string.IsNullOrEmpty(city))
        {
            throw new DomainException("The city property cannot be null");
        }

        if (string.IsNullOrEmpty(number))
        {
            throw new DomainException("The number property cannot be null");
        }

        return new(street, postalCode, city, number);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return PostalCode;
        yield return City;
        yield return Number;
    }
}