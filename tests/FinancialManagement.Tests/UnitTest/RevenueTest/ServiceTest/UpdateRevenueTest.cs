using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.Revenue;
using Microsoft.VisualBasic;

namespace FinancialManagement.Tests.UnitTest.RevenueTest.ServiceTest;
public class UpdateRevenueTest
{
    [Fact]
    public async Task Should_Update_Revenue()
    {
        // Arrange
        var UpdateRevenue = new UpdateRevenueDto
        {
            Description = " new Test Description",
            DateRevenue = DateTime.UtcNow,
            Value = 1000,
            NamePropertyToBeUpdate = "Description"
        };

        string nameProperty = UpdateRevenue.NamePropertyToBeUpdate;

        var revenue = new Revenue
        {
            IdRevenue = Guid.NewGuid(),
            Description = UpdateRevenue.Description,
            DateRevenue = UpdateRevenue.DateRevenue,
            Value = UpdateRevenue.Value,
        };

        var revenueRepository = new Mock<IRevenueRepository>();
        revenueRepository.Setup(x => x.UpdateRevenue(It.IsAny<Revenue>(), It.IsAny<string>()));

        var iloggerMock = new Mock<ILogger<RevenueServices>>();

        var revenueService = new RevenueServices(revenueRepository.Object, iloggerMock.Object);

        // Act
        await revenueService.UpdateRevenue(UpdateRevenue, nameProperty);

        // Assert
        revenueRepository.Verify(x => x.UpdateRevenue(It.IsAny<Revenue>(), It.IsAny<string>()), Times.Once);
    }
}
