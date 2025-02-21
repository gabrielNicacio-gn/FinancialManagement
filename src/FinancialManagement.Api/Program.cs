using FinancialManagement.Api.Extensions;
using FinancialManagement.Api.IoC;
using FinancialManagement.Api.Routes;
using FinancialManagement.Identity.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwagger();
builder.ConfigureIoc();
builder.Services.AddAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
        var context = scope.ServiceProvider.GetRequiredService<FinancialManagementIdentityContext>();
        Thread.Sleep(3000);
        context.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapExpenseRoutes();
app.MapRevenueRoutes();
app.MapFinancialTargetsRoutes();
app.MapCategoryExpenseRoutes();
app.MapIndentityRoutes();

app.Run();
