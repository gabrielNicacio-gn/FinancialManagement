
using FinancialManagement.Api.ExtensionsRoutes;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.Interfaces.Services;

namespace FinancialManagement.Api.Routes;
public static class FinancialTargetEndpoints
{
        public static void MapFinancialTargetsRoutes(this WebApplication app)
        {
                var financialTargetRoutes = app.MapGroup("/")
                .WithTags("Financial Target")
                .WithSummary("Financial Target routes");

                financialTargetRoutes.MapGet("/financial-targets", async (IFinancialTargetServices financialTargetServices) =>
                {
                        var financialTargets = await financialTargetServices.GetAllFinancialTarget();
                        return Results.Ok(financialTargets);
                })
                .WithSummary("Return All Financial target")
                .WithDescription("Return All Financial target");

                financialTargetRoutes.MapGet("/financial-target{id}", async (IFinancialTargetServices financialTargetServices, Guid idFinancialTarget) =>
                {
                        var financialTargets = await financialTargetServices.GetFinancialTargetById(idFinancialTarget);
                        return Results.Ok(financialTargets);
                })
                .WithSummary("Return All Financial target")
                .WithDescription("Return All Financial target");

                financialTargetRoutes.MapPost("/financial-target", async (IFinancialTargetServices financialTargetServices, CreateFinancialTargetDto createFinancial) =>
                {
                        var newFinancialTarget = await financialTargetServices.CreateNewFinancialTarget(createFinancial);
                        return Results.Created($"/financial-target/{newFinancialTarget.IdFinancialTarget}", newFinancialTarget);
                })
                .WithSummary("Return All Financial target")
                .WithDescription("Return All Financial target")
                .Validate<CreateFinancialTargetDto>();

                financialTargetRoutes.MapPut("/financial-target", async (IFinancialTargetServices financialTargetServices, UpdateFinancialTargetDto updateFinancial) =>
                {
                        await financialTargetServices.UpdateFinancialTarget(updateFinancial, updateFinancial.NamePropertyToBeUpdate);
                        return Results.NoContent();
                })
                .WithSummary("Return All Financial target")
                .WithDescription("Return All Financial target")
                .Validate<UpdateFinancialTargetDto>();

                financialTargetRoutes.MapDelete("/financial-target{id}", async (IFinancialTargetServices financialTargetServices, Guid idFinancialTarget) =>
                {
                        await financialTargetServices.RemoveFinancialTarget(idFinancialTarget);
                        return Results.NoContent();
                })
                .WithSummary("Return All Financial target")
                .WithDescription("Return All Financial target");
        }
}
