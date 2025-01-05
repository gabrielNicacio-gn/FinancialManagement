using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Tests.UnitTest.CategoryExpenseTest.ServiceTest
{
    public class GetCategoryExpenseByIdTest
    {
        [Fact]
        public async Task GetCategoryExpenseById_Success()
        {
            // Arrange
            var categoryExpense = new CategoryExpense
            {
                IdCategory = Guid.NewGuid(),
                Name = "Test Category 1",
            };
            var mockRepository = new Mock<ICategoryExpenseRepository>();
            mockRepository.Setup(x => x.GetCategoryExpenseById(It.IsAny<Guid>())).ReturnsAsync(categoryExpense);
            var iLogger = new Mock<ILogger<CategoryExpenseServices>>();

            var categoryExpenseService = new CategoryExpenseServices(mockRepository.Object, iLogger.Object);
            // Act
            var result = await categoryExpenseService.GetCategoryExpenseById(categoryExpense.IdCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryExpense.Name, result.Name);
        }
    }
}