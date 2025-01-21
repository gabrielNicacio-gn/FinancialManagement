using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Api.Extensions;
using FinancialManagement.Application.DTOs.Request.CategoryExpense;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Api.Routes;
public static class CategoryExpenseEndpoints
{
    public static void MapCategoryExpenseRoutes(this WebApplication app)
    {
        var expensesRoutes = app.MapGroup("/")
        .WithTags("CategoryExpenses")
        .WithSummary("CategoryExpenses routes")
        .RequireAuthorization();

        expensesRoutes.MapPost("/category-expense", async (ICategoryExpenseServices categoryEcxpenseServices, CreateCategoryExpenseDto request) =>
        {
            var result = await categoryEcxpenseServices.CreateNewCategoryExpense(request);
            return Results.Created($"/expense/{result.Data?.IdCategory}", result);
        })
        .WithDescription("Create a new category expense")
        .WithSummary("Create a category expense")
        .Produces<CategoryExpense>(201)
        .Validate<CreateCategoryExpenseDto>();

        expensesRoutes.MapGet("/categorys-expense", async (ICategoryExpenseServices categoryEcxpenseServices) =>
        {
            var result = await categoryEcxpenseServices.GetAllCategoryExpenses();
            return Results.Ok(result);
        })
        .WithDescription("Get all categorys expense")
        .Produces<CategoryExpense>(200)
        .WithSummary("Get all categorys expense");

        expensesRoutes.MapGet("/category-expense{id}", async (ICategoryExpenseServices categoryEcxpenseServices, Guid id) =>
        {
            var result = await categoryEcxpenseServices.GetCategoryExpenseById(id);
            return result.IsSucess
            ? Results.Ok(result)
            : Results.NotFound();
        })
        .WithDescription("Get category expense by id")
        .Produces<CategoryExpense>(200)
        .Produces<CategoryExpense>(404)
        .WithSummary("Get category expense by id");

        expensesRoutes.MapPut("/category-expense", async (ICategoryExpenseServices categoryEcxpenseServices, UpdateCategoryExpenseDto request) =>
        {
            await categoryEcxpenseServices.UpdateCategoryExpense(request, request.NamePropertyToBeUpdate);
            return Results.NoContent();
        })
        .WithDescription("Update an category expense")
        .WithSummary("Update an category expense")
        .Produces<CategoryExpense>(204)
        .Validate<UpdateCategoryExpenseDto>();

        expensesRoutes.MapDelete("/category-expense{id}", async (ICategoryExpenseServices categoryEcxpenseServices, Guid id) =>
        {
            await categoryEcxpenseServices.RemoveCategoryExpense(id);
            return Results.NoContent();
        })
        .WithDescription("Delete an category expense")
        .Produces<CategoryExpense>(204)
        .WithSummary("Delete an category expense");
    }
}
