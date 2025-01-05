using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;

namespace FinancialManagement.Tests.UnitTest.CategoryExpenseTest.ServiceTest
{
    public class UpdateCategoryExpenseTest
    {
        [Fact]
        public async Task UpdateCategoryExpense_Success()
        {
            // Arrange
            var newCategoryExpense = new UpdateCategoryExpenseDto
            {
                Name = "Test Category 1",
                NamePropertyToBeUpdate = "Name"
            };
            var categoryExpense = new CategoryExpense
            {
                IdCategory = Guid.NewGuid(),
                Name = newCategoryExpense.Name,
            };
            var nameProperty = newCategoryExpense.NamePropertyToBeUpdate;
            var mockRepository = new Mock<ICategoryExpenseRepository>();
            mockRepository.Setup(x => x.UpdateCategoryExpense(It.IsAny<CategoryExpense>(), It.IsAny<string>()));
            var iLogger = new Mock<ILogger<CategoryExpenseServices>>();

            var categoryExpenseService = new CategoryExpenseServices(mockRepository.Object, iLogger.Object);
            // Act
            await categoryExpenseService.UpdateCategoryExpense(newCategoryExpense, nameProperty);

            // Assert
            mockRepository.Verify(x => x.UpdateCategoryExpense(It.IsAny<CategoryExpense>(), It.IsAny<string>()), Times.Once);
        }
    }
}