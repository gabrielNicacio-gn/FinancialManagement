
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Api.ExtensionsRoutes;
using FinancialManagement.Application.Interfaces.Services;

namespace FinancialManagement.Api.Routes;
public static class ExpenseEndpoints
{
    public static void MapExpenseRoutes(this WebApplication app)
    {
        var expensesRoutes = app.MapGroup("/")
        .WithTags("Expenses")
        .WithSummary("Expenses routes");

        expensesRoutes.MapPost("/expense", async (IExpenseServices expenseServices, CreateExpenseDto request) =>
        {
            var result = await expenseServices.CreateNewExpense(request);
            return Results.Created($"/expense/{result.IdExpense}", result);
        })
        .WithDescription("Create a new expense")
        .WithSummary("Create a new expense")
        .Validate<CreateExpenseDto>();

        expensesRoutes.MapGet("/expenses", async (IExpenseServices expenseServices) =>
        {
            var result = await expenseServices.GetAllExpense();
            return Results.Ok(result);
        })
        .WithDescription("Get all expenses")
        .WithSummary("Get all expenses");

        expensesRoutes.MapGet("/expense{id}", async (IExpenseServices expenseServices, Guid id) =>
        {
            var result = await expenseServices.GetExpenseById(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithDescription("Get all expenses")
        .WithSummary("Get all expenses");

        expensesRoutes.MapPut("/expense", async (IExpenseServices expenseServices, UpdateExpenseDto request) =>
        {
            await expenseServices.UpdateExpense(request, request.NamePropertyToBeUpdate);
            return Results.NoContent();
        })
        .WithDescription("Update an expense")
        .WithSummary("Update an expense")
        .Validate<UpdateExpenseDto>();

        expensesRoutes.MapDelete("/expense{id}", async (IExpenseServices expenseServices, Guid id) =>
        {
            await expenseServices.RemoveExpense(id);
            return Results.NoContent();
        })
        .WithDescription("Delete an expense")
        .WithSummary("Delete an expense");
    }
}
