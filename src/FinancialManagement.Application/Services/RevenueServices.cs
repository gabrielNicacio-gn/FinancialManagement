using Microsoft.Extensions.Logging;
using FinancialManagement.Application.DTOs.Request.Expense;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Interfaces.Services;
using FinancialManagement.Domain.Models;
using FinancialManagement.Application.DTOs.Request.Revenue;
using FinancialManagement.Application.DTOs.Shared;

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

    public async Task<BaseResponseDto<RevenueResponseDto>> CreateNewRevenue(CreateRevenueDto newRevenue, Guid userId)
    {
        var revenue = new Revenue
        {
            Description = newRevenue.Description,
            Value = newRevenue.Value,
            DateRevenue = newRevenue.DateRevenue,
            UserId = userId
        };
        var created = await _revenueRepository.AddRevenue(revenue);
        _logger.LogInformation($"Revenue created with id: {created.IdRevenue}");
        var newRevenueResponse = new RevenueResponseDto(revenue.IdRevenue, created.Value, created.DateRevenue,
         created.Description);
        return new BaseResponseDto<RevenueResponseDto>(newRevenueResponse);
    }

    public async Task<BaseResponseDto<IEnumerable<RevenueResponseDto>>> GetAllRevenue(Guid userId)
    {
        var revenues = await _revenueRepository.GetRevenues(userId);
        var listRevenuesResponse = revenues
        .Select(revenue => new RevenueResponseDto(revenue.IdRevenue, revenue.Value, revenue.DateRevenue, revenue.Description));
        _logger.LogInformation("Returning all revenues");
        return new BaseResponseDto<IEnumerable<RevenueResponseDto>>(listRevenuesResponse);
    }

    public async Task<BaseResponseDto<RevenueResponseDto>> GetRevenueById(Guid idRevenue)
    {
        var revenue = await _revenueRepository.GetRevenueById(idRevenue);

        if (revenue is null)
        {
            _logger.LogInformation($"Revenue not found");
            return new BaseResponseDto<RevenueResponseDto>(false);
        }

        var revenueResponse = new RevenueResponseDto(revenue.IdRevenue, revenue.Value, revenue.DateRevenue, revenue.Description);
        _logger.LogInformation($"Returning revenue with id: {revenue.IdRevenue}");
        return new BaseResponseDto<RevenueResponseDto>(revenueResponse);
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
