using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.ExpenseTest.ServiceTest;
public class DeleteExpenseTest
{
    [Fact]
    public async Task DeleteExpense_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var expenseId = Guid.NewGuid();

        var mockExpenseRepository = new Mock<IExpenseRepository>();
        mockExpenseRepository.Setup(x => x.DeleteExpenses(It.IsAny<Guid>()));

        var ilogger = new Mock<ILogger<ExpenseServices>>();

        var _expenseService = new ExpenseServices(mockExpenseRepository.Object, ilogger.Object);

        // Act
        await _expenseService.RemoveExpense(expenseId);

        // Assert
        mockExpenseRepository.Verify(x => x.DeleteExpenses(It.IsAny<Guid>()), Times.Once);
    }
}
