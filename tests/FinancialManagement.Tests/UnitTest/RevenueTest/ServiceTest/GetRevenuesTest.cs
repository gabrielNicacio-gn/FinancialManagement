using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.RevenueTest.ServiceTest;
public class GetRevenuesTest
{
    [Fact]
    public async Task Should_Get_Revenues()
    {
        // Arrange
        var revenues = new List<Revenue>
            {
                new Revenue
                {
                    IdRevenue = Guid.NewGuid(),
                    Description = "Test Description",
                    DateRevenue = DateTime.UtcNow,
                    Value = 1000,
                },
                new Revenue
                {
                    IdRevenue = Guid.NewGuid(),
                    Description = "Test Description 2",
                    DateRevenue = DateTime.UtcNow,
                    Value = 2000,
                }
            };

        var revenueRepository = new Mock<IRevenueRepository>();
        revenueRepository.Setup(x => x.GetRevenues()).ReturnsAsync(revenues);

        var iloggerMock = new Mock<ILogger<RevenueServices>>();

        var revenueService = new RevenueServices(revenueRepository.Object, iloggerMock.Object);

        // Act
        var result = await revenueService.GetAllRevenue();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(revenues.Count, result.Data?.Count());
    }
}
