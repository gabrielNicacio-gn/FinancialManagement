using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.Services;

namespace FinancialManagement.Tests.UnitTest.ExpenseTest.ServiceTest;
public class GetAllExpensesTest
{
    [Fact]
    public async Task GetAllExpensesTest_WhenCalled_ReturnsAllExpenses()
    {
        // Arrange
        var expenses = new List<Expense>
            {
                new Expense
                {
                    IdExpense = Guid.NewGuid(),
                    Description = "Test Expense 1",
                    Value = 100,
                    DateExpenses = DateTime.Now,
                    IdCategory = Guid.NewGuid(),
                    CategoryeExpense = new CategoryExpense
                    {
                        IdCategory = Guid.NewGuid(),
                        Name = "Test Category 1"
                    }
                },
              new Expense
                {
                    IdExpense = Guid.NewGuid(),
                    Description = "Test Expense 2",
                    Value = 200,
                    DateExpenses = DateTime.Now,
                    IdCategory = Guid.NewGuid(),
                    CategoryeExpense = new CategoryExpense
                    {
                        IdCategory = Guid.NewGuid(),
                        Name = "Test Category 2"
                    }
                },
            };
        var mockExpenseRepository = new Mock<IExpenseRepository>();
        mockExpenseRepository.Setup(x => x.GetExpenses()).ReturnsAsync(expenses);
        var ilogger = new Mock<ILogger<ExpenseServices>>();

        var expenseService = new ExpenseServices(mockExpenseRepository.Object, ilogger.Object);
        // Act
        var result = await expenseService.GetAllExpense();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(expenses.Count, result.Count());
    }
}
