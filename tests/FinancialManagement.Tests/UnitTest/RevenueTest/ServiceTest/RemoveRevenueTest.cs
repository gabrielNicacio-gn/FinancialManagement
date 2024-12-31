using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.RevenueTest.ServiceTest
{
    public class RemoveRevenueTest
    {
        [Fact]
        public async Task Should_Remove_Revenue()
        {
            // Arrange
            var idRevenue = Guid.NewGuid();
            var revenue = new Revenue
            {
                IdRevenue = Guid.NewGuid(),
                Description = "Test Description",
                DateRevenue = DateTime.UtcNow,
                Value = 1000,
            };

            var revenueRepository = new Mock<IRevenueRepository>();
            revenueRepository.Setup(x => x.DeleteRevenue(It.IsAny<Guid>()));

            var iloggerMock = new Mock<ILogger<RevenueServices>>();

            var revenueService = new RevenueServices(revenueRepository.Object, iloggerMock.Object);
            // Act
            await revenueService.RemoveRevenue(idRevenue);
            // Assert
        }
    }
}