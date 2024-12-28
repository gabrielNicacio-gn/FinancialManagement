using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Domain.Models;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Api.ExtensionsRoutes;

namespace FinancialManagement.Api.Routes;
public static class RevenueEndpoints
{
    public static void MapRevenueRoutes(this WebApplication app)
    {
        var revenueRoutes = app.MapGroup("/")
        .WithTags("Revenues")
        .WithSummary("Revenues routes");

        revenueRoutes.MapPost("/revenue", async (IRevenueRepository revenueRepository, CreateRevenueDto request) =>
        {
            var revenue = new Revenue
            {
                Value = request.Value,
                DateRevenue = request.DateRevenue,
                Description = request.Description,
                Category = request.Category
            };
            var result = await revenueRepository.AddRevenue(revenue);
            return Results.Created($"/revenue/{result.IdRevenue}", result);
        })
        .WithDescription("Create a new revenue")
        .WithSummary("Create a new revenue")
        .Produces<Revenue>(201)
        .Validate<CreateRevenueDto>();

        revenueRoutes.MapGet("/revenues", async (IRevenueRepository revenueRepository) =>
        {
            var result = await revenueRepository.GetRevenues();
            return Results.Ok(result);
        })
        .WithDescription("Get all revenues")
        .WithSummary("Get all revenues")
        .Produces<List<Revenue>>(200);

        revenueRoutes.MapGet("/revenue/{id}", async (IRevenueRepository revenueRepository, Guid id) =>
        {
            var result = await revenueRepository.GetRevenueById(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithDescription("Get revenue by id")
        .WithSummary("Get revenue by id")
        .Produces<Revenue>(200)
        .Produces(404);

        revenueRoutes.MapPut("/revenue", async (IRevenueRepository revenueRepository, UpdateRevenueDto request) =>
        {
            var revenue = new Revenue
            {
                Value = request.Value,
                DateRevenue = request.DateRevenue,
                Description = request.Description,
                Category = request.Category
            };
            await revenueRepository.UpdateRevenue(revenue, nameof(revenue.Value));
            return Results.NoContent();
        })
        .WithDescription("Update a revenue")
        .WithSummary("Update a revenue")
        .Produces(204)
        .Validate<UpdateRevenueDto>();

        revenueRoutes.MapDelete("/revenue/{id}", async (IRevenueRepository revenueRepository, Guid id) =>
        {
            await revenueRepository.DeleteRevenue(id);
            return Results.NoContent();
        })
        .WithDescription("Delete a revenue")
        .WithSummary("Delete a revenue")
        .Produces(204);
    }
}
