using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;

namespace FinancialManagement.Domain.Interfaces.Repositories;
public interface IFinancialTargetRepository
{
    Task<IEnumerable<FinancialTarget>> GetFinancialTargets(Guid userId);
    Task<FinancialTarget?> GetFinancialTargetById(Guid id);
    Task<FinancialTarget> AddFinancialTarget(FinancialTarget financialTarget);
    Task UpdateFinancialTarget(FinancialTarget financialTarget, string nameProperty);
    Task DeleteFinancialTarget(Guid id);
}
