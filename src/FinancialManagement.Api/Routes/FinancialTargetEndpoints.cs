
using FinancialManagement.Api.Extensions;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Api.Routes;
public static class FinancialTargetEndpoints
{
        public static void MapFinancialTargetsRoutes(this WebApplication app)
        {
                var financialTargetRoutes = app.MapGroup("/")
                .WithTags("Financial Target")
                .WithSummary("Financial Target routes")
                .RequireAuthorization();

                financialTargetRoutes.MapGet("/financial-targets", async (IFinancialTargetServices financialTargetServices, GetUserCurrent userCurrent) =>
                {
                        var userId = userCurrent.GetUserIdFromToken();
                        var financialTargets = await financialTargetServices.GetAllFinancialTarget(userId);
                        return Results.Ok(financialTargets);
                })
                .WithSummary("Return All Financial target")
                .Produces(200)
                .WithDescription("Return All Financial target");

                financialTargetRoutes.MapGet("/financial-target{id}", async (IFinancialTargetServices financialTargetServices, Guid idFinancialTarget) =>
                {
                        var financialTargets = await financialTargetServices.GetFinancialTargetById(idFinancialTarget);
                        return financialTargets.IsSucess
                        ? Results.Ok(financialTargets)
                        : Results.NotFound();
                })
                .WithSummary("Return Financial target by Id")
                .Produces(200)
                .Produces(404)
                .WithDescription("Return Financial target by Id");

                financialTargetRoutes.MapPost("/financial-target", async (IFinancialTargetServices financialTargetServices, CreateFinancialTargetDto createFinancial, GetUserCurrent userCurrent) =>
                {
                        var userId = userCurrent.GetUserIdFromToken();
                        var newFinancialTarget = await financialTargetServices.CreateNewFinancialTarget(createFinancial, userId);
                        return Results.Created($"/financial-target/{newFinancialTarget.Data?.IdFinancialTarget}", newFinancialTarget);
                })
                .WithSummary("Create a new Financial target")
                .WithDescription("Create a new Financial target")
                .Produces(201)
                .Validate<CreateFinancialTargetDto>();

                financialTargetRoutes.MapPut("/financial-target", async (IFinancialTargetServices financialTargetServices, UpdateFinancialTargetDto updateFinancial) =>
                {
                        await financialTargetServices.UpdateFinancialTarget(updateFinancial, updateFinancial.NamePropertyToBeUpdate);
                        return Results.NoContent();
                })
                .WithSummary("Update a Financial target")
                .WithDescription("Update a Financial target")
                .Produces(204)
                .Validate<UpdateFinancialTargetDto>();

                financialTargetRoutes.MapDelete("/financial-target{id}", async (IFinancialTargetServices financialTargetServices, Guid idFinancialTarget) =>
                {
                        await financialTargetServices.RemoveFinancialTarget(idFinancialTarget);
                        return Results.NoContent();
                })
                .WithSummary("Delete a Financial target")
                .Produces(204)
                .WithDescription("Delete a Financial target");
        }
}
