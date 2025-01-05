using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.ExpenseTest.ServiceTest;
public class UpdateExpenseTest
{
        [Fact]
        public async Task UpdateExpenseTest_WhenCalled_ReturnsUpdatedExpense()
        {
                var newExpense = new UpdateExpenseDto
                {
                        Description = "Test Expense 1",
                        Value = 100,
                        DateExpenses = DateTime.Now,
                        CategoryExpense = Guid.NewGuid(),
                        NamePropertyToBeUpdate = "Description"
                };
                var nameProperty = newExpense.NamePropertyToBeUpdate;
                // Arrange
                var expense = new Expense
                {
                        IdExpense = Guid.NewGuid(),
                        Description = newExpense.Description,
                        Value = newExpense.Value,
                        DateExpenses = newExpense.DateExpenses,
                        IdCategory = newExpense.CategoryExpense,
                        CategoryeExpense = new CategoryExpense
                        {
                                IdCategory = newExpense.CategoryExpense,
                                Name = "Test Category 1"
                        }
                };

                var mockExpenseRepository = new Mock<IExpenseRepository>();

                mockExpenseRepository.Setup(x => x.UpdateExpenses(It.IsAny<Expense>(), It.IsAny<string>()));
                var ilogger = new Mock<ILogger<ExpenseServices>>();

                var expenseService = new ExpenseServices(mockExpenseRepository.Object, ilogger.Object);
                // Act
                await expenseService.UpdateExpense(newExpense, nameProperty);

                // Assert
                mockExpenseRepository.Verify(x => x.UpdateExpenses(It.IsAny<Expense>(), It.IsAny<string>()), Times.Once);
        }
}
