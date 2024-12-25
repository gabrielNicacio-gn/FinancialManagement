using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Repositories;
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
    }
}
