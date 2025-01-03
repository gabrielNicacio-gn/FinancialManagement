using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Api.ExtensionsRoutes;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.Interfaces.Services;

namespace FinancialManagement.Api.Routes;
public static class CategoryExpenseEndpoints
{
    public static void MapCategoryExpenseRoutes(this WebApplication app)
    {
        var expensesRoutes = app.MapGroup("/")
        .WithTags("CategoryExpenses")
        .WithSummary("CategoryExpenses routes");

        expensesRoutes.MapPost("/category-expense", async (ICategoryExpenseServices categoryEcxpenseServices, CreateCategoryExpenseDto request) =>
        {
            var result = await categoryEcxpenseServices.CreateNewCategoryExpense(request);
            return Results.Created($"/expense/{result.IdCategory}", result);
        })
        .WithDescription("Create a new category expense")
        .WithSummary("Create a category expense")
        .Validate<CreateCategoryExpenseDto>();

        expensesRoutes.MapGet("/categorys-expense", async (ICategoryExpenseServices categoryEcxpenseServices) =>
        {
            var result = await categoryEcxpenseServices.GetAllCategoryExpenses();
            return Results.Ok(result);
        })
        .WithDescription("Get all categorys expense")
        .WithSummary("Get all categorys expense");

        expensesRoutes.MapGet("/category-expense{id}", async (ICategoryExpenseServices categoryEcxpenseServices, Guid id) =>
        {
            var result = await categoryEcxpenseServices.GetCategoryExpenseById(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithDescription("Get category expense by id")
        .WithSummary("Get category expense by id");

        expensesRoutes.MapPut("/category-expense", async (ICategoryExpenseServices categoryEcxpenseServices, UpdateCategoryExpenseDto request) =>
        {
            await categoryEcxpenseServices.UpdateCategoryExpense(request, request.NamePropertyToBeUpdate);
            return Results.NoContent();
        })
        .WithDescription("Update an category expense")
        .WithSummary("Update an category expense")
        .Validate<UpdateCategoryExpenseDto>();

        expensesRoutes.MapDelete("/category-expense{id}", async (ICategoryExpenseServices categoryEcxpenseServices, Guid id) =>
        {
            await categoryEcxpenseServices.RemoveCategoryExpense(id);
            return Results.NoContent();
        })
        .WithDescription("Delete an category expense")
        .WithSummary("Delete an category expense");
    }
}
