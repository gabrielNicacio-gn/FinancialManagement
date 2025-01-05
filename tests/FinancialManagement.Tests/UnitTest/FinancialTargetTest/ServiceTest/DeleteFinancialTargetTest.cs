using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.FinancialTargetTest.ServiceTest;
public class DeleteFinancialTargetTest
{
    [Fact]
    public async Task DeleteFinancialTarget_Success()
    {
        // Arrange
        var idFinancialTarget = Guid.NewGuid();

        var iLogger = new Mock<ILogger<FinancialTargetServices>>();
        var financialTargetRepository = new Mock<IFinancialTargetRepository>();
        financialTargetRepository.Setup(x => x.DeleteFinancialTarget(It.IsAny<Guid>()));

        var financialTargetService = new FinancialTargetServices(financialTargetRepository.Object, iLogger.Object);

        // Act
        await financialTargetService.RemoveFinancialTarget(idFinancialTarget);

        // Assert
        financialTargetRepository.Verify(x => x.DeleteFinancialTarget(It.IsAny<Guid>()), Times.Once);
    }
}
