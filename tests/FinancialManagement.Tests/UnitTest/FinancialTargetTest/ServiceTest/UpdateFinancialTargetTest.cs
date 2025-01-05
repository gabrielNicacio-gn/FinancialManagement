using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;

namespace FinancialManagement.Tests.UnitTest.FinancialTargetTest.ServiceTest;
public class UpdateFinancialTargetTest
{
    [Fact]
    public async Task UpdateFinancialTarget_Success()
    {
        // Arrange
        var updateFinancialTarget = new UpdateFinancialTargetDto
        {
            Title = "Test Upddate",
            ValueNeeded = 2000,
            DateLimit = new DateTime(2025, 12, 30),
            Description = "Test Description",
            NamePropertyToBeUpdate = "Description"

        };

        var financialTarget = new FinancialTarget
        {
            IdFinancialTarget = Guid.NewGuid(),
            ValueNeeded = updateFinancialTarget.ValueNeeded,
            DateLimit = updateFinancialTarget.DateLimit,
            Description = updateFinancialTarget.Description,
            Title = updateFinancialTarget.Title,
        };
        var nameProperty = updateFinancialTarget.NamePropertyToBeUpdate;
        var iLogger = new Mock<ILogger<FinancialTargetServices>>();
        var financialTargetRepository = new Mock<IFinancialTargetRepository>();
        financialTargetRepository.Setup(x => x.UpdateFinancialTarget(It.IsAny<FinancialTarget>(), It.IsAny<string>()));

        var financialTargetService = new FinancialTargetServices(financialTargetRepository.Object, iLogger.Object);

        // Act
        await financialTargetService.UpdateFinancialTarget(updateFinancialTarget, nameProperty);

        // Assert
        financialTargetRepository.Verify(x => x.UpdateFinancialTarget(It.IsAny<FinancialTarget>(), It.IsAny<string>()), Times.Once);
    }
}
