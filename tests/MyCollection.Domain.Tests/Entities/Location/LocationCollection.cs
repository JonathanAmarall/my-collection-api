using Xunit;

namespace MyCollection.Domain.Tests.Entities.Location
{
    [CollectionDefinition(nameof(LocationCollection))]
    public class LocationCollection : ICollectionFixture<LocationTestsFixture> { }
}
