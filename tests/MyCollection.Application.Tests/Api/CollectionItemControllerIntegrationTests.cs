using FluentAssertions;
using MyCollection.Application.Tests.DTOs;
using MyCollection.Core.Helpers;
using MyCollection.Domain;
using MyCollection.Domain.Entities;
using System.Net.Http.Json;

namespace MyCollection.Application.Tests.Api
{

    public class CollectionItemControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CollectionItemControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task GetAll_ShouldReturn_OK()
        {
            var response = await _client.GetAsync("/api/v1/collection-items");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var collectionItemPagedList = content.ToObject<PagedListDto<Domain.Entities.CollectionItem>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            collectionItemPagedList.TotalCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Create_ShouldReturn_OK()
        {
            var command = new CreateCollectionItemCommand("Livro ABC", "John Doe", 10, "Gold", EType.BOOK.GetHashCode());
            var response = await _client.PostAsJsonAsync("/api/v1/collection-items", command);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_WithInvalidRequest_ShouldReturnBadRequest()
        {
            var command = new CreateCollectionItemCommand(string.Empty, string.Empty, 10, "Gold", EType.BOOK.GetHashCode());
            var response = await _client.PostAsJsonAsync("/api/v1/collection-items", command);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
