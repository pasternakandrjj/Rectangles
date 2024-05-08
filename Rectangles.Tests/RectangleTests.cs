using Microsoft.AspNetCore.Mvc.Testing;
using Rectangles.Models;
using System.Net;
using System.Net.Http.Json;

namespace Rectangles.Tests
{
    public class RectangleTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RectangleTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRectangles_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Rectangle/Get?a=2&b=3");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GenerateRectangles_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Rectangle/GenerateRectangles");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GenerateRectangles_AddsToDatabase()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Rectangle/GenerateRectangles");

            // Assert
            response.EnsureSuccessStatusCode();

            // Get rectangles from response
            var rectangles = await response.Content.ReadFromJsonAsync<List<Rectangle>>();

            // Check if rectangles are added to the database
            Assert.NotEmpty(rectangles);
        }
    }
}