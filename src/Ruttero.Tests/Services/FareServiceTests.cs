using Xunit;
using Moq;
using Ruttero.Services;
using Ruttero.Interfaces.Repositories;
using Ruttero.Dtos.Fares;
using Ruttero.Models;

namespace Ruttero.Tests.Services;

public class FareServiceTests
{
    [Fact]
    public async Task CreateFareAsync_ShouldReturnSuccess_WhenFareIsCreated()
    {
        // Arrange
        var mockRepo = new Mock<IFareRepository>();
        var service = new FareService(mockRepo.Object);

        var dto = new CreateFareRequestDto { Description = "Test", Price = 100 };

        // Act
        var result = await service.CreateFareAsync(dto, userId: 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("La tarifa se ha registrado exitosamente.", result.Message);
    }
}
