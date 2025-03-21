using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using FinancialManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Repositories;
public class FinancialTargetRepository : IFinancialTargetRepository
{
        private readonly FinancialContext _context;
        public FinancialTargetRepository(FinancialContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<FinancialTarget>> GetFinancialTargets(Guid userId)
        {
                var financialTargets = await _context.FinancialTargets
                .AsNoTracking()
                .OrderByDescending(e => e.DateLimit)
                .Where(ft => ft.UserId == userId)
                .ToListAsync();
                return financialTargets;
        }

        public async Task<FinancialTarget?> GetFinancialTargetById(Guid id)
        {
                var financialTarget = await _context
                .FinancialTargets
                .AsNoTracking()
                .SingleOrDefaultAsync(ft => ft.IdFinancialTarget == id);
                return financialTarget;
        }

        public async Task<FinancialTarget> AddFinancialTarget(FinancialTarget financialTarget)
        {
                var financialTargetCreated = _context.FinancialTargets.Add(financialTarget).Entity;
                await _context.SaveChangesAsync();
                return financialTargetCreated;
        }

        public async Task UpdateFinancialTarget(FinancialTarget financialTarget, string nameProperty)
        {
                var entity = _context.FinancialTargets.Attach(financialTarget);
                entity.State = EntityState.Modified;
                entity.Property(nameProperty).IsModified = true;

                await _context.SaveChangesAsync();
        }

        public async Task DeleteFinancialTarget(Guid id)
        {
                await _context.FinancialTargets
               .Where(e => e.IdFinancialTarget == id)
               .ExecuteDeleteAsync();
        }
}
