using Bogus;
using Bogus.DataSets;
using MyCollection.Domain.Entities;
using System;
using Xunit;

namespace MyCollection.Domain.Tests
{
    [CollectionDefinition(nameof(ConctactCollection))]
    public class ConctactCollection : ICollectionFixture<ContactTestsFixture>
    {

    }
    public class ContactTestsFixture : IDisposable
    {
        public Contact GenerateConctactValid()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var fullName = new Faker().Name.FullName(genero);

            var contactFaker = new Faker<Contact>("pt_BR")
                .CustomInstantiator(f => new Contact(
                    fullName,
                    f.Internet.Email(fullName),
                    f.Phone.PhoneNumber()));

            return contactFaker;
        }

        public void Dispose()
        {

        }

    }



}
