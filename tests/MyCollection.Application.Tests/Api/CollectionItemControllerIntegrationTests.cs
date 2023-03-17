using FluentAssertions;

namespace MyCollection.Application.Tests.Api
{

    public class CollectionItemControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public CollectionItemControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task Get_ShouldReturn_OK()
        {
            var response = await _client.GetAsync("/api/v1/collection-items");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
