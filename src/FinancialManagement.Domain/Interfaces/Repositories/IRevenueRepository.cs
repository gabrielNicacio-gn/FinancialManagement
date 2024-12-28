using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Domain.Interfaces.Repositories;
public interface IRevenueRepository
{
    Task<IEnumerable<Revenue>> GetRevenues();
    Task<Revenue?> GetRevenueById(Guid id);
    Task<Revenue> AddRevenue(Revenue revenue);
    Task UpdateRevenue(Revenue revenue, string nameProperty);
    Task DeleteRevenue(Guid id);
    bool RevenueExists(Guid id);
}
