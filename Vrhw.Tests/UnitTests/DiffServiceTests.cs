using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Vrhw.Core.Services;
using Vrhw.Shared.DTOs;
using Vrhw.Shared.Helpers;
using Vrhw.Shared.Interfaces;
using Vrhw.Shared.Models;
using Xunit;

namespace Vrhw.Tests
{
    public class DiffServiceTests
    {
        [Fact]
        public void Left_Should_return_true_if_data_is_Base64_string()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.UpsertDiff(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.Left(1, "VGhpcyBpcyBhIHRlc3Qu");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Left_Should_return_false_if_data_is_Base64_string()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.UpsertDiff(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.Left(1, "=NotBase64=");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Right_Should_return_true_if_data_is_Base64_string()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.UpsertDiff(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.Right(1, "VGhpcyBpcyBhIHRlc3Qu");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Right_Should_return_false_if_data_is_Base64_string()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.UpsertDiff(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.Right(1, "=NotBase64=");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetDiff_Should_return_Equals_if_left_and_right_are_equals()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.GetDiff(It.IsAny<int>()))
                .Returns(new DiffDto()
                {
                    Left = "VGhpcyBpcyBhIHVuaXQgdGVzdA==",
                    Right = "VGhpcyBpcyBhIHVuaXQgdGVzdA=="
                });

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.GetDiff(1).GetStrProperty(DiffMessages.DiffResultTypeProperty);

            // Assert
            result.Should().Be(DiffMessages.EqualsResponse);
        }

        [Fact]
        public void GetDiff_Should_return_SizeDoNotMatch_if_left_and_right_are_different_in_size()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.GetDiff(It.IsAny<int>()))
                .Returns(new DiffDto()
                {
                    Left = "VGhpcyBpcyBhIHVuaXQgdGVzdA==",
                    Right = "U2l6ZURvTm90TWF0Y2g="
                });

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.GetDiff(1).GetStrProperty(DiffMessages.DiffResultTypeProperty);

            // Assert
            result.Should().Be(DiffMessages.SizeDoNotMatchResponse);
        }

        [Fact]
        public void GetDiff_Should_return_ContentDoNotMatch_if_left_and_right_are_same_size_but_different_in_content()
        {
            // Arrange
            var fakeRepository = new Mock<IDiffRepository>();
            fakeRepository
                .Setup(x => x.GetDiff(It.IsAny<int>()))
                .Returns(new DiffDto()
                {
                    Left = "VGhpcyBpcyB0aGUgdGVzdA==",
                    Right = "VGhpc19pcyBUSEUgdGVzdA=="
                });

            var sut = new DiffService(fakeRepository.Object);

            // Act
            var result = sut.GetDiff(1);

            // Assert
            result.GetStrProperty(DiffMessages.DiffResultTypeProperty).Should().Be(DiffMessages.ContentDoNotMatchResponse);
            ((List<DiffSection>)result.GetProperty(DiffMessages.DiffsProperty)).Count.Should().Be(2);
            ((List<DiffSection>)result.GetProperty(DiffMessages.DiffsProperty))[0].Offset.Should().Be(4);
            ((List<DiffSection>)result.GetProperty(DiffMessages.DiffsProperty))[0].Length.Should().Be(1);
            ((List<DiffSection>)result.GetProperty(DiffMessages.DiffsProperty))[1].Offset.Should().Be(8);
            ((List<DiffSection>)result.GetProperty(DiffMessages.DiffsProperty))[1].Length.Should().Be(3);
        }
    }
}