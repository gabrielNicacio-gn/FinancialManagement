using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.RevenueTest.ServiceTest;
public class GetRevenuesByIdTest
{
    [Fact]
    public async Task Should_Get_Revenues_By_Id()
    {
        // Arrange
        var revenue = new Revenue
        {
            IdRevenue = Guid.NewGuid(),
            Description = "Test Description",
            DateRevenue = DateTime.UtcNow,
            Value = 1000,
        };

        var revenueRepository = new Mock<IRevenueRepository>();
        revenueRepository.Setup(x => x.GetRevenueById(It.IsAny<Guid>())).ReturnsAsync(revenue);

        var iloggerMock = new Mock<ILogger<RevenueServices>>();

        var revenueService = new RevenueServices(revenueRepository.Object, iloggerMock.Object);

        // Act
        var result = await revenueService.GetRevenueById(revenue.IdRevenue);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revenue.IdRevenue, result.IdRevenue);
        Assert.Equal(revenue.Description, result.Description);
        Assert.Equal(revenue.DateRevenue, result.DateRevenue);
        Assert.Equal(revenue.Value, result.Value);
    }
}
