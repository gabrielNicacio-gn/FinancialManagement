using FinancialManagement.Api.IoC;
using FinancialManagement.Api.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureIoc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}

app.MapExpenseRoutes();
app.MapRevenueRoutes();
app.MapFinancialTargetsRoutes();
app.MapCategoryExpenseRoutes();

app.Run();
