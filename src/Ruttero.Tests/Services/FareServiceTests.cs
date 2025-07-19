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

        var dto = new CreateFareRequestDto
        {
            Description = "Test",
            Price = 100
        };

        // Act
        var result = await service.CreateFareAsync(dto, userId: 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("La tarifa se ha registrado exitosamente.", result.Message);
    }

    [Fact]
    public async Task UpdateFareAsync_ShouldReturnError_WhenFareNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Fare?)null);
        var service = new FareService(mockRepo.Object);

        var dto = new UpdateFareRequestDto
        {
            Id = 1,
            IsActive = true
        };

        // Act
        var result = await service.UpdateFareAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Tarifa no encontrada.", result.Message);
    }

    [Fact]
    public async Task UpdateFareAsync_ShouldReturnError_WhenFareAlreadyHasDesiredState()
    {
        // Arrange
        var fare = new Fare
        {
            Id = 1,
            IsActive = true
        };
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(fare.Id)).ReturnsAsync(fare);
        var service = new FareService(mockRepo.Object);

        var dto = new UpdateFareRequestDto
        {
            Id = 1,
            IsActive = true
        };

        // Act
        var result = await service.UpdateFareAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("La tarifa ya está activa.", result.Message);
    }

    [Fact]
    public async Task UpdateFareAsync_ShouldUpdateFare_WhenStateIsDifferent()
    {
        // Arrange
        var fare = new Fare { Id = 1, IsActive = false };
        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(fare.Id)).ReturnsAsync(fare);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Fare>())).Returns(Task.CompletedTask);
        var service = new FareService(mockRepo.Object);

        var dto = new UpdateFareRequestDto
        {
            Id = 1,
            IsActive = true
        };

        // Act
        var result = await service.UpdateFareAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("La tarifa fue reactivada exitosamente.", result.Message);
        Assert.True(fare.IsActive);
    }

    [Fact]
    public async Task GetAllFaresAsync_ShouldReturnListOfFares()
    {
        // Arrange
        var fares = new List<Fare>
    {
        new Fare {
            Id = 1,
            Description = "Tarifa 1",
            Price = 100
            },

        new Fare {
            Id = 2,
            Description = "Tarifa 2",
            Price = 200
             }
    };

        var mockRepo = new Mock<IFareRepository>();
        mockRepo.Setup(r => r.GetAllFaresAsync(It.IsAny<int>())).ReturnsAsync(fares);
        var service = new FareService(mockRepo.Object);

        // Act
        var result = await service.GetAllFaresAsync(userId: 1);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Tarifa 1", result[0].Description);
        Assert.Equal(200, result[1].Price);
    }

}
