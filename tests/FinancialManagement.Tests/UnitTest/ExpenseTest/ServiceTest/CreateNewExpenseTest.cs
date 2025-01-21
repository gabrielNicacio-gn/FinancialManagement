using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.ExpenseTest.ServiceTest;
public class CreateNewExpenseTest
{
    [Fact]
    public async Task CreateNewExpense_Success()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var newExpense = new CreateExpenseDto
        {
            Description = "Test Description",
            Value = 100,
            DateExpenses = DateTime.Now,
            CategoryExpense = categoryId
        };
        var expense = new Expense
        {
            IdExpense = Guid.NewGuid(),
            Description = newExpense.Description,
            Value = newExpense.Value,
            DateExpenses = newExpense.DateExpenses,
            IdCategory = newExpense.CategoryExpense
        };

        var expenseRepository = new Mock<IExpenseRepository>();
        expenseRepository.Setup(x => x.AddExpenses(It.IsAny<Expense>())).ReturnsAsync(expense);

        var ilogger = new Mock<ILogger<ExpenseServices>>();

        var service = new ExpenseServices(expenseRepository.Object, ilogger.Object);

        // Act
        var result = await service.CreateNewExpense(newExpense);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(expense.Description, result.Data?.Description);
        Assert.Equal(expense.Value, result.Data?.Value);
        Assert.Equal(expense.DateExpenses, result.Data?.DateExpense);
    }
}
