using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.Interfaces.Services;

namespace FinancialManagement.Tests.UnitTest.CategoryExpenseTest.ServiceTest
{
    public class CreateNewCategoryExpenseTest
    {
        [Fact]
        public async Task CreateNewCategoryExpense_Success()
        {
            // Arrange
            var newCategoryExpense = new CreateCategoryExpenseDto
            {
                Name = "Test Category Expense",
            };
            var categoryExpense = new CategoryExpense
            {
                IdCategory = Guid.NewGuid(),
                Name = newCategoryExpense.Name,
            };
            var iLogger = new Mock<ILogger<CategoryExpenseServices>>();
            var mockCategoryExpenseRepository = new Mock<ICategoryExpenseRepository>();
            mockCategoryExpenseRepository.Setup(x => x.AddCategoryExpense(It.IsAny<CategoryExpense>())).ReturnsAsync(categoryExpense);

            var categoryExpenseService = new CategoryExpenseServices(mockCategoryExpenseRepository.Object, iLogger.Object);
            // Act
            var result = await categoryExpenseService.CreateNewCategoryExpense(newCategoryExpense);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryExpense.Name, result.Data?.Name);
        }
    }
}