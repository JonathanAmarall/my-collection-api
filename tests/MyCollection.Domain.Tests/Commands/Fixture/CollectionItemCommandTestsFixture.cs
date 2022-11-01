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
              EType.BOOK);

            return command;
        }

        public CreateCollectionItemCommand GenerateCreateCollectionItemCommandInvalid()
        {
            var command = new CreateCollectionItemCommand(
                 "De",
                 "E",
                 -10,
                 "Deluxe",
                 EType.BOOK);

            return command;
        }


        public LendCollectionItemCommand GenerateLendCollectionItemCommandValid()
        {
            var command = new LendCollectionItemCommand(
               Guid.NewGuid(),
               Guid.NewGuid(),
               "John Doe",
               "john.d@mail.com",
               "559939865");

            return command;
        }

        public LendCollectionItemCommand GenerateLendCollectionItemCommandInvalid()
        {
            var command = new LendCollectionItemCommand(
              Guid.Empty,
              Guid.Empty,
              "",
              "",
              "");

            return command;
        }





        public void Dispose()
        {

        }
    }
}
