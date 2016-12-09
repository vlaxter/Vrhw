using FluentAssertions;
using Microsoft.Owin.Testing;
using System;
using System.Net;
using System.Net.Http;
using Vrhw.Api.App_Start;
using Vrhw.Shared.Helpers;
using Vrhw.Shared.Models;
using Xunit;

namespace Vrhw.Tests.IntegrationTests
{
    public class DiffIntegrationTests : IDisposable
    {
        private TestServer _server;

        public DiffIntegrationTests()
        {
            _server = TestServer.Create<IntegrationTestsStartup>();
        }

        [Fact]
        public async void PutLeft_Should_return_HttpStatusCode_Created_201()
        {
            // Arrange
            var data = new RequestModel { Data = "QmxhZGltaXIgUmliZXJhIE1vcmE=" };

            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/100/left", data.ConvetToJson());

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void PutLeft_Should_return_HttpStatusCode_BadRequest_400_When_data_is_not_Base64()
        {
            // Arrange
            var data = new RequestModel { Data = "=NotBase64=" };

            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/100/left", data.ConvetToJson());

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void PutLeft_Should_return_HttpStatusCode_BadRequest_400_When_data_is_null()
        {
            // Arrange
            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/100/left", null);

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void PutRight_Should_return_HttpStatusCode_Created_201()
        {
            // Arrange
            var data = new RequestModel { Data = "QmxhZGltaXIgUmliZXJhIE1vcmE=" };

            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/200/right", data.ConvetToJson());

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void PutRight_Should_return_HttpStatusCode_BadRequest_400_When_data_is_not_Base64()
        {
            // Arrange
            var data = new RequestModel { Data = "=NotBase64=" };

            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/200/right", data.ConvetToJson());

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void PutRight_Should_return_HttpStatusCode_BadRequest_400_When_data_is_null()
        {
            // Arrange
            // Act
            var putResponse = await _server.HttpClient.PutAsync("/v1/diff/200/right", null);

            // Arrange
            putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void GetDiff_Should_return_equals_When_left_and_right_are_equal()
        {
            // Arrange
            var data = new RequestModel { Data = "QmxhZGltaXIgUmliZXJhIE1vcmE=" };

            await _server.HttpClient.PutAsync("/v1/diff/1/left", data.ConvetToJson());
            await _server.HttpClient.PutAsync("/v1/diff/1/right", data.ConvetToJson());

            // Act
            var response = _server.HttpClient.GetAsync("/v1/diff/1").Result;
            var result = response.Content.ReadAsAsync<object>().Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.GetJsonProperty("diffResultType").Should().Be("Equals");
        }

        [Fact]
        public async void GetDiff_Should_return_SizeDoNotMatch_When_left_and_right_are_different_in_size()
        {
            // Arrange
            var dataLeft = new RequestModel { Data = "VGhpcyBpcyBhIHVuaXQgdGVzdA==" };
            var dataRight = new RequestModel { Data = "U2l6ZURvTm90TWF0Y2g=" };

            await _server.HttpClient.PutAsync("/v1/diff/2/left", dataLeft.ConvetToJson());
            await _server.HttpClient.PutAsync("/v1/diff/2/right", dataRight.ConvetToJson());

            // Act
            var response = _server.HttpClient.GetAsync("/v1/diff/2").Result;
            var result = response.Content.ReadAsAsync<object>().Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.GetJsonProperty("diffResultType").Should().Be("SizeDoNotMatch");
        }

        [Fact]
        public async void GetDiff_Should_return_ContentDoNotMatch_When_left_and_right_are_same_size_but_different_in_content_()
        {
            // Arrange
            var dataLeft = new RequestModel { Data = "VGhpcyBpcyB0aGUgdGVzdA==" };
            var dataRight = new RequestModel { Data = "VGhpc19pcyBUSEUgdGVzdA==" };

            await _server.HttpClient.PutAsync("/v1/diff/3/left", dataLeft.ConvetToJson());
            await _server.HttpClient.PutAsync("/v1/diff/3/right", dataRight.ConvetToJson());

            // Act
            var response = _server.HttpClient.GetAsync("/v1/diff/3").Result;
            var result = response.Content.ReadAsAsync<object>().Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.GetJsonProperty("diffResultType").Should().Be("ContentDoNotMatch");
        }

        [Fact]
        public void GetDiff_Should_return_HttpStatusCode_BadRequest_400_When_does_not_exists()
        {
            // Arrange
            // Act
            var response = _server.HttpClient.GetAsync("/v1/diff/4").Result;
            var result = response.Content.ReadAsAsync<object>().Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}