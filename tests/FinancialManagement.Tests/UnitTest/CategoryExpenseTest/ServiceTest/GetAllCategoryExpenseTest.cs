using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.Services;

namespace FinancialManagement.Tests.UnitTest.CategoryExpenseTest.ServiceTest;
public class GetAllCategoryExpenseTest
{
    [Fact]
    public async Task GetAllCategoryExpense_Success()
    {
        // Arrange
        var categoryExpense = new List<CategoryExpense>
        {
            new CategoryExpense
            {
                IdCategory = Guid.NewGuid(),
                Name = "Test Category 1",
            },
            new CategoryExpense
            {
                IdCategory = Guid.NewGuid(),
                Name = "Test Category 2",
            }
        };
        var mockRepository = new Mock<ICategoryExpenseRepository>();
        mockRepository.Setup(x => x.GetCategoryExpenses()).ReturnsAsync(categoryExpense);
        var iLogger = new Mock<ILogger<CategoryExpenseServices>>();

        var categoryExpenseService = new CategoryExpenseServices(mockRepository.Object, iLogger.Object);
        // Act
        var result = await categoryExpenseService.GetAllCategoryExpenses();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryExpense.Count, result.Count());
    }

}

