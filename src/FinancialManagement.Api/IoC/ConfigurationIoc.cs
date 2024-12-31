
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Application.Services;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Interfaces.Services;
using FinancialManagement.Infrastructure.Data;
using FinancialManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Api.IoC;
public static class ConfigurationIoc
{
    public static void ConfigureIoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<FinancialContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("npgsqlConnStr"));
        });

        builder.Services.AddScoped<IExpenseRepository, ExpensesRepository>();
        builder.Services.AddScoped<IRevenueRepository, RevenueRepository>();
        builder.Services.AddScoped<IFinancialTargetRepository, FinancialTargetRepository>();

        builder.Services.AddScoped<IRevenueServices, RevenueServices>();
        builder.Services.AddScoped<IExpenseServices, ExpenseServices>();
        builder.Services.AddScoped<IFinancialTargetServices, FinancialTargetServices>();
    }
}
