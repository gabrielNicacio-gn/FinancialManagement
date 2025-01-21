using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Domain.Interfaces.Services;


namespace FinancialManagement.Tests.UnitTest.RevenueTest.ServiceTest;
public class CreateNewRevenueTest
{
    [Fact]
    public async Task Should_Create_New_Revenue()
    {
        var newDto = new CreateRevenueDto()
        {
            Description = "Test Description",
            DateRevenue = DateTime.UtcNow,
            Value = 1000,
        };
        // Arrange
        var revenue = new Revenue
        {
            IdRevenue = Guid.NewGuid(),
            Description = newDto.Description,
            DateRevenue = newDto.DateRevenue,
            Value = newDto.Value,
        };

        var revenueRepository = new Mock<IRevenueRepository>();
        revenueRepository.Setup(x => x.AddRevenue(It.IsAny<Revenue>())).ReturnsAsync(revenue);

        var iloggerMock = new Mock<ILogger<RevenueServices>>();

        var revenueService = new RevenueServices(revenueRepository.Object, iloggerMock.Object);

        // Act
        var result = await revenueService.CreateNewRevenue(newDto);

        // Assert
        Assert.NotNull(result);
        //Assert.Equal(revenue.IdRevenue, result.IdRevenue);
        Assert.Equal(revenue.Description, result.Data?.Description);
        Assert.Equal(revenue.DateRevenue, result.Data?.DateRevenue);
        Assert.Equal(revenue.Value, result.Data?.Value);
    }
}
