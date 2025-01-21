using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.ExpenseTest.ServiceTest;
public class GetExpenseByIdTest
{
        [Fact]
        public async Task GetExpenseByIdTest_WhenCalled_ReturnsExpense()
        {
                // Arrange
                var expense = new Expense
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
                };
                var mockExpenseRepository = new Mock<IExpenseRepository>();
                mockExpenseRepository.Setup(x => x.GetExpensesById(It.IsAny<Guid>())).ReturnsAsync(expense);
                var ilogger = new Mock<ILogger<ExpenseServices>>();

                var expenseService = new ExpenseServices(mockExpenseRepository.Object, ilogger.Object);
                // Act
                var result = await expenseService.GetExpenseById(expense.IdExpense);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(expense.Description, result.Data?.Description);
                Assert.Equal(expense.Value, result.Data?.Value);
                Assert.Equal(expense.DateExpenses, result.Data?.DateExpense);
                //Assert.Equal(expense.IdCategory, result.);
                //Assert.Equal(expense.CategoryeExpense.IdCategory, result.CategoryeExpense.IdCategory);
                //Assert.Equal(expense.CategoryeExpense.Name, result.CategoryeExpense.Name);
        }
}
