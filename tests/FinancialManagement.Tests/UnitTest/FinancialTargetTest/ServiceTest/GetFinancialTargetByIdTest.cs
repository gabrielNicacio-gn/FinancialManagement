using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.FinancialTargetTest.ServiceTest;
public class GetFinancialTargetByIdTest
{
    [Fact]
    public async Task GetFinancialTargetById_Success()
    {
        // Arrange
        var idFinancialTarget = Guid.NewGuid();

        var financialTarget = new FinancialTarget
        {
            IdFinancialTarget = idFinancialTarget,
            ValueNeeded = 2000,
            DateLimit = new DateTime(2025, 12, 30),
            Description = "Test Description",
            Title = "Test Title",
        };

        var iLogger = new Mock<ILogger<FinancialTargetServices>>();
        var financialTargetRepository = new Mock<IFinancialTargetRepository>();
        financialTargetRepository.Setup(x => x.GetFinancialTargetById(It.IsAny<Guid>())).ReturnsAsync(financialTarget);

        var financialTargetService = new FinancialTargetServices(financialTargetRepository.Object, iLogger.Object);

        // Act
        var result = await financialTargetService.GetFinancialTargetById(idFinancialTarget);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(financialTarget.ValueNeeded, result.Data?.ValueNeeded);
        Assert.Equal(financialTarget.DateLimit, result.Data?.DateLimit);
        Assert.Equal(financialTarget.Description, result.Data?.Description);
        Assert.Equal(financialTarget.Title, result.Data?.Title);
    }
}