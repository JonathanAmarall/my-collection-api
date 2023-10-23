using FluentAssertions;
using MyCollection.Application.Tests.DTOs;
using MyCollection.Core.Helpers;
using MyCollection.Domain;
using MyCollection.Domain.Commands;
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
            var collectionItemPagedList = content.ToObject<PagedListDto<CollectionItem>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            collectionItemPagedList.TotalCount.Should().BeGreaterThan(0);
            collectionItemPagedList.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldReturn_OK()
        {
            var command = new CreateCollectionItemCommand("Livro ABC", "John Doe", 10, "Gold", EType.BOOK);
            var response = await _client.PostAsJsonAsync("/api/v1/collection-items", command);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_WithInvalidRequest_ShouldReturnBadRequest()
        {
            var command = new CreateCollectionItemCommand(string.Empty, string.Empty, 10, "Gold", EType.BOOK);
            var response = await _client.PostAsJsonAsync("/api/v1/collection-items", command);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_WithInvalidRequest_ShouldReturnBadRequest()
        {
            var getAllCollectionItensResponse = await _client.GetAsync("/api/v1/collection-items?pageSize=1&pageIndex=1");
            getAllCollectionItensResponse.EnsureSuccessStatusCode();

            var getAllLocationsResponse = await _client.GetAsync("/api/v1/locations?pageSize=1&pageIndex=1");
            getAllLocationsResponse.EnsureSuccessStatusCode();
            
            var collectionItem = await GetCollectionItemByResponse(getAllCollectionItensResponse);
            var location = await GetLocationByResponse(getAllLocationsResponse);

            var command = new AddLocationInCollectionItemCommand(collectionItem.Id, location.Id);

            var addLocationResponse = await _client.PutAsJsonAsync($"/api/v1/collection-items/{command.CollectionItemId}", command);
            
            var msg = await addLocationResponse.Content.ReadAsStringAsync();
            addLocationResponse.EnsureSuccessStatusCode();
            
            getAllCollectionItensResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            getAllLocationsResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            addLocationResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        

        private static async Task<Location> GetLocationByResponse(HttpResponseMessage getAllLocationsResponse)
        {
            var locationsContent = await getAllLocationsResponse.Content.ReadAsStringAsync();
            var locationsList = locationsContent.ToObject<List<Location>>();
            var location = locationsList.First();
            return location;
        }

        private static async Task<CollectionItem> GetCollectionItemByResponse(HttpResponseMessage getAllCollectionItensResponse)
        {
            var collectionItemContent = await getAllCollectionItensResponse.Content.ReadAsStringAsync();
            var collectionItemPagedList = collectionItemContent.ToObject<PagedListDto<CollectionItem>>();
            var collectionItem = collectionItemPagedList.Data.First();
            return collectionItem;
        }
    }
}
