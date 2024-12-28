using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Domain.Models;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Api.ExtensionsRoutes;
using FinancialManagement.Domain.Interfaces.Services;

namespace FinancialManagement.Api.Routes;
public static class RevenueEndpoints
{
    public static void MapRevenueRoutes(this WebApplication app)
    {
        var revenueRoutes = app.MapGroup("/")
        .WithTags("Revenues")
        .WithSummary("Revenues routes");

        revenueRoutes.MapPost("/revenue", async (IRevenueServices revenueServices, CreateRevenueDto request) =>
        {
            var result = await revenueServices.CreateNewRevenue(request);
            return Results.Created($"/revenue/{result.IdRevenue}", result);
        })
        .WithDescription("Create a new revenue")
        .WithSummary("Create a new revenue")
        .Produces<Revenue>(201)
        .Validate<CreateRevenueDto>();

        revenueRoutes.MapGet("/revenues", async (IRevenueServices revenueServices) =>
        {
            var result = await revenueServices.GetAllRevenue();
            return Results.Ok(result);
        })
        .WithDescription("Get all revenues")
        .WithSummary("Get all revenues")
        .Produces<List<Revenue>>(200);

        revenueRoutes.MapGet("/revenue/{id}", async (IRevenueServices revenueServices, Guid id) =>
        {
            var result = await revenueServices.GetRevenueById(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithDescription("Get revenue by id")
        .WithSummary("Get revenue by id")
        .Produces<Revenue>(200)
        .Produces(404);

        revenueRoutes.MapPut("/revenue", async (IRevenueServices revenueServices, UpdateRevenueDto request) =>
        {
            await revenueServices.UpdateRevenue(request, nameof(request.Description));
            return Results.NoContent();
        })
        .WithDescription("Update a revenue")
        .WithSummary("Update a revenue")
        .Produces(204)
        .Validate<UpdateRevenueDto>();

        revenueRoutes.MapDelete("/revenue/{id}", async (IRevenueServices revenueServices, Guid id) =>
        {
            await revenueServices.RemoveRevenue(id);
            return Results.NoContent();
        })
        .WithDescription("Delete a revenue")
        .WithSummary("Delete a revenue")
        .Produces(204);
    }
}
