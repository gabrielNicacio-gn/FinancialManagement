using Microsoft.Extensions.Logging;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Interfaces.Services;
using FinancialManagement.Domain.Models;
using FinancialManagement.Application.DTOs.Request.Revenue;

namespace FinancialManagement.Application.Services;
public class RevenueServices : IRevenueServices
{
    private readonly IRevenueRepository _revenueRepository;
    private readonly ILogger<RevenueServices> _logger;
    public RevenueServices(IRevenueRepository revenueRepository, ILogger<RevenueServices> logger)
    {
        _revenueRepository = revenueRepository;
        _logger = logger;
    }

    public async Task<RevenueResponseDto> CreateNewRevenue(CreateRevenueDto newRevenue)
    {
        var revenue = new Revenue
        {
            Description = newRevenue.Description,
            Value = newRevenue.Value,
            DateRevenue = newRevenue.DateRevenue
        };
        var created = await _revenueRepository.AddRevenue(revenue);
        _logger.LogInformation($"Revenue created with id: {created.IdRevenue}");
        return new RevenueResponseDto(created.Value, created.DateRevenue, created.Description);
    }

    public async Task<IEnumerable<RevenueResponseDto>> GetAllRevenue()
    {
        var revenues = await _revenueRepository.GetRevenues();
        var response = revenues
        .Select(revenue => new RevenueResponseDto(revenue.Value, revenue.DateRevenue, revenue.Description));
        _logger.LogInformation("Returning all revenues");
        return response;
    }

    public async Task<RevenueResponseDto> GetRevenueById(Guid idRevenue)
    {
        var revenue = await _revenueRepository.GetRevenueById(idRevenue)
        ?? throw new Exception("Revenue not found");
        var response = new RevenueResponseDto(revenue.Value, revenue.DateRevenue, revenue.Description);
        _logger.LogInformation($"Returning revenue with id: {revenue.IdRevenue}");
        return response;
    }

    public async Task RemoveRevenue(Guid idRevenue)
    {
        await _revenueRepository.DeleteRevenue(idRevenue);
        _logger.LogInformation($"Revenue with id: {idRevenue} removed");
    }

    public async Task UpdateRevenue(UpdateRevenueDto updateRevenue, string nameProperty)
    {
        var revenue = new Revenue
        {
            Description = updateRevenue.Description,
            Value = updateRevenue.Value,
            DateRevenue = updateRevenue.DateRevenue
        };
        await _revenueRepository.UpdateRevenue(revenue, nameProperty);
        _logger.LogInformation($"Revenue with id: {nameProperty} updated");
    }
}
