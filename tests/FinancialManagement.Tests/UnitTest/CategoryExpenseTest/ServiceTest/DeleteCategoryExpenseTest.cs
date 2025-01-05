using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.Services;

namespace FinancialManagement.Tests.UnitTest.CategoryExpenseTest.ServiceTest
{
    public class DeleteCategoryExpenseTest
    {
        [Fact]
        public async Task DeleteCategoryExpense_Success()
        {
            // Arrange

            var idCategoryExpense = Guid.NewGuid();

            var mockRepository = new Mock<ICategoryExpenseRepository>();
            mockRepository.Setup(repo => repo.DeleteCategoryExpense(idCategoryExpense));
            var iLogger = new Mock<ILogger<CategoryExpenseServices>>();

            var categoryExpenseService = new CategoryExpenseServices(mockRepository.Object, iLogger.Object);

            // Act
            await categoryExpenseService.RemoveCategoryExpense(idCategoryExpense);

            // Assert
            mockRepository.Verify(x => x.DeleteCategoryExpense(idCategoryExpense), Times.Once);
        }
    }
}