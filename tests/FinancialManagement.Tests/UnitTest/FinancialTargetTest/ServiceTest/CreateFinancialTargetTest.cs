using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Application.Services;

namespace FinancialManagement.Tests.UnitTest.FinancialTargetTest.ServiceTest;
public class CreateFinancialTargetTest
{
    [Fact]
    public async Task CreateFinancialTarget_Success()
    {
        // Arrange
        var newFinancialTarget = new CreateFinancialTargetDto
        {
            Title = "Test",
            ValueNeeded = 4000,
            DateLimit = new DateTime(2025, 12, 31),
            Description = "Test Description",
        };
        var financialTarget = new FinancialTarget
        {
            IdFinancialTarget = Guid.NewGuid(),
            Title = newFinancialTarget.Title,
            ValueNeeded = newFinancialTarget.ValueNeeded,
            DateLimit = newFinancialTarget.DateLimit,
            Description = newFinancialTarget.Description,
        };
        var iLogger = new Mock<ILogger<FinancialTargetServices>>();
        var mockFinancialTargetRepository = new Mock<IFinancialTargetRepository>();
        mockFinancialTargetRepository.Setup(x => x.AddFinancialTarget(It.IsAny<FinancialTarget>())).ReturnsAsync(financialTarget);
        var financialTargetService = new FinancialTargetServices(mockFinancialTargetRepository.Object, iLogger.Object);
        // Act
        var result = await financialTargetService.CreateNewFinancialTarget(newFinancialTarget);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newFinancialTarget.Title, result.Data?.Title);
        Assert.Equal(newFinancialTarget.ValueNeeded, result.Data?.ValueNeeded);
        Assert.Equal(newFinancialTarget.DateLimit, result.Data?.DateLimit);
        Assert.Equal(newFinancialTarget.Description, result.Data?.Description);
    }
}
