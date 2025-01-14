using FinancialManagement.Api.Extensions;
using FinancialManagement.Api.IoC;
using FinancialManagement.Api.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureIoc();
builder.Services.AddAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapExpenseRoutes();
app.MapRevenueRoutes();
app.MapFinancialTargetsRoutes();
app.MapCategoryExpenseRoutes();
app.MapIndentityRoutes();

app.Run();
