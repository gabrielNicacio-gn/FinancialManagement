using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.FinancialTargetTest.ServiceTest;
public class GetAllFinancialTargetTest
{
    [Fact]
    public async Task GetAllFinancialTarget_Success()
    {
        // Arrange
        var financialTargetList = new List<FinancialTarget>
            {
                new FinancialTarget
                {
                    IdFinancialTarget = Guid.NewGuid(),
                    ValueNeeded = 2000,
                    DateLimit = new DateTime(2025, 12, 30),
                    Description = "Test Description",
                    Title = "Test Title",
                },
                new FinancialTarget
                {
                    IdFinancialTarget = Guid.NewGuid(),
                    ValueNeeded = 3000,
                    DateLimit = new DateTime(2025, 12, 30),
                    Description = "Test Description",
                    Title = "Test Title",
                }
            };

        var iLogger = new Mock<ILogger<FinancialTargetServices>>();
        var financialTargetRepository = new Mock<IFinancialTargetRepository>();
        financialTargetRepository.Setup(x => x.GetFinancialTargets()).ReturnsAsync(financialTargetList);

        var financialTargetService = new FinancialTargetServices(financialTargetRepository.Object, iLogger.Object);

        // Act
        var result = await financialTargetService.GetAllFinancialTarget();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(financialTargetList.Count, result.Count());
    }
}
