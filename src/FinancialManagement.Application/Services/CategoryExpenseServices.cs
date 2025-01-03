using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Application.Services
{
    public class CategoryExpenseServices : ICategoryExpenseServices
    {
        private readonly ICategoryExpenseRepository _categoryExpenseRepository;
        private readonly ILogger<CategoryExpenseServices> _logger;
        public CategoryExpenseServices(ICategoryExpenseRepository categoryExpenseRepository, ILogger<CategoryExpenseServices> logger)
        {
            _categoryExpenseRepository = categoryExpenseRepository;
            _logger = logger;
        }
        public async Task<CategoryExpenseResponseDto> CreateNewCategoryExpense(CreateCategoryExpenseDto categoryExpenseDto)
        {
            var newCategoryExpense = new CategoryExpense
            {
                Name = categoryExpenseDto.Name,
            };
            var categoryExpenseCreated = await _categoryExpenseRepository.AddCategoryExpense(newCategoryExpense);
            _logger.LogInformation($"CategoryExpense created with id: {categoryExpenseCreated.IdCategory}");
            return new CategoryExpenseResponseDto(categoryExpenseCreated.IdCategory, categoryExpenseCreated.Name);

        }

        public async Task<IEnumerable<CategoryExpenseResponseDto>> GetAllCategoryExpenses()
        {
            var categoryExpenses = await _categoryExpenseRepository.GetCategoryExpenses();
            _logger.LogInformation($"CategoryExpenses found: {categoryExpenses.Count()}");
            return categoryExpenses.Select(categoryExpense =>
             new CategoryExpenseResponseDto(categoryExpense.IdCategory, categoryExpense.Name));
        }

        public async Task<CategoryExpenseResponseDto> GetCategoryExpenseById(Guid idCategoryExpense)
        {
            var categoryExpense = await _categoryExpenseRepository.GetCategoryExpenseById(idCategoryExpense)
            ?? throw new Exception("CategoryExpense not found");
            _logger.LogInformation($"CategoryExpense found with id: {categoryExpense.IdCategory}");
            return new CategoryExpenseResponseDto(categoryExpense.IdCategory, categoryExpense.Name);

        }

        public async Task RemoveCategoryExpense(Guid idCategoryExpense)
        {
            await _categoryExpenseRepository.DeleteCategoryExpense(idCategoryExpense);
            _logger.LogInformation($"CategoryExpense with id: {idCategoryExpense} removed");
        }

        public async Task UpdateCategoryExpense(UpdateCategoryExpenseDto updateCategoryExpense, string nameProperty)
        {
            var categoryExpenseUpdated = new CategoryExpense
            {
                Name = updateCategoryExpense.Name,
            };
            await _categoryExpenseRepository.UpdateCategoryExpense(categoryExpenseUpdated, nameProperty);
            _logger.LogInformation($"CategoryExpense with id: {categoryExpenseUpdated.IdCategory} updated");
        }
    }
}