using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimpleChatApp.Data.DTOs.UserDTOs;
using SimpleChatApp.Data.Responses;
using SimpleChatApp.Data.Responses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.Tests
{
    public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        internal UserControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnOk_WhenEntitiesExist()
        {
            // Act
            var response = await _client.GetAsync("/api/User");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseResponse<List<GetUserDto>>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenEntityExists()
        {
            // Arrange
            var id = new Guid("your-existing-user-id"); // Replace with an existing user id

            // Act
            var response = await _client.GetAsync($"/api/User/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseResponse<GetUserDto>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenEntityIsAdded()
        {
            // Arrange
            var newUser = new AddUserDto
            {
                UserName = "Test User"
            };
            var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/User", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseResponse<string>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Entity added.", result.Description);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenEntityIsUpdated()
        {
            // Arrange
            var updatedUser = new UpdateUserDto
            {
                Id = Guid.NewGuid(),
                UserName = "Updated User"
            };
            var content = new StringContent(JsonConvert.SerializeObject(updatedUser), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/User", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseResponse<string>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Entity updated.", result.Description);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenEntityIsDeleted()
        {
            // Arrange
            var id = new Guid("your-existing-user-id"); // Replace with an existing user id

            // Act
            var response = await _client.DeleteAsync($"/api/User/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseResponse<string>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.StatusCode.Equals(StatusCode.Ok));
            Assert.Equal("Entity deleted.", result.Description);
        }
    }
}
