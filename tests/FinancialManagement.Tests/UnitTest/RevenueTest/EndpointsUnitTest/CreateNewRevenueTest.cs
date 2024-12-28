using System;
using System.Collections.Generic;
using System.Linq;


namespace FinancialManagement.Tests.UnitTest.RevenueTest.EndpointsUnitTest;
public class CreateNewRevenueTest
{
    [Fact]
    public async Task ShouldCreateNewRevenue()
    {
        // Arrange
        var newRequest = new CreateRevenueDto
        {
            Value = 100,
            Description = "Test",
            DateRevenue = DateTime.Now,
        };

        var revenueRepository = new Mock<IRevenueRepository>();
        revenueRepository.Setup(x => x.AddRevenue(It.IsAny<Revenue>()))
            .ReturnsAsync(new Revenue
            {
                IdRevenue = Guid.NewGuid(),
                Value = newRequest.Value,
                Description = newRequest.Description,
                DateRevenue = newRequest.DateRevenue
            });

        var httpClient = new HttpClient();
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton<IRevenueRepository>(revenueRepository.Object)
        .BuildServiceProvider();
        var webApp = builder.Build();
        webApp.MapRevenueRoutes();

        // Act
        var response = await httpClient
        .PostAsJsonAsync<CreateRevenueDto>("/revenue", newRequest);
        // Assert
        Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
        throw new NotImplementedException();
    }
}
