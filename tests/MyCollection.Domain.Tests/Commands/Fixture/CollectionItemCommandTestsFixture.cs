using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using System;
using Xunit;

namespace MyCollection.Domain.Tests.Commands
{

    [CollectionDefinition(nameof(CreateCollectionItemCollection))]
    public class CreateCollectionItemCollection : ICollectionFixture<CollectionItemCommandTestsFixture>
    {

    }

    public class CollectionItemCommandTestsFixture : IDisposable
    {
        public CreateCollectionItemCommand GenerateCreateCollectionItemCommandValid()
        {
            var command = new CreateCollectionItemCommand(
              "Design Patterns: Elements of Reusable Object-Oriented Software",
              "Erich Gamma",
              10,
              "Deluxe",
              EType.BOOK.GetHashCode());

            return command;
        }

        public CreateCollectionItemCommand GenerateCreateCollectionItemCommandInvalid()
        {
            var command = new CreateCollectionItemCommand(
                 "De",
                 "E",
                 -10,
                 "Deluxe",
                 EType.BOOK.GetHashCode());

            return command;
        }

        public RentItemCommand GenerateLendCollectionItemCommandValid()
        {
            var command = new RentItemCommand(
               Guid.NewGuid(),
               Guid.NewGuid(),
               1);

            return command;
        }

        public RentItemCommand GenerateLendCollectionItemCommandWithoutContactIdValid()
        {
            var command = new RentItemCommand(
               Guid.NewGuid(),
               Guid.Empty,
               1);

            return command;
        }

        public RentItemCommand GenerateLendCollectionItemCommandInvalid()
        {
            var command = new RentItemCommand(
              Guid.Empty,
              Guid.Empty,
              -12);

            return command;
        }

        public void Dispose()
        {
        }
    }
}
