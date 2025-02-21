using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Models;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infrastructure.Repositories;
public class RevenueRepository : IRevenueRepository
{
        private readonly FinancialContext _context;
        public RevenueRepository(FinancialContext context)
        {
                _context = context;
        }
        public async Task<Revenue> AddRevenue(Revenue revenue)
        {
                var revenueCreated = _context.Revenues.Add(revenue).Entity;
                await _context.SaveChangesAsync();
                return revenueCreated;
        }

        public bool RevenueExists(Guid id)
        {
                var exist = _context.Revenues.Any(e => e.IdRevenue == id);
                return exist;
        }

        public async Task<IEnumerable<Revenue>> GetRevenues(Guid userId)
        {
                var expenses = await _context.Revenues
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.DateRevenue)
                .ToListAsync();
                return expenses;
        }

        public async Task<Revenue?> GetRevenueById(Guid id)
        {
                var expense = await _context.Revenues
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.IdRevenue == id);
                return expense;
        }

        public async Task UpdateRevenue(Revenue revenue, string nameProperty)
        {
                var entity = _context.Revenues.Attach(revenue);
                entity.State = EntityState.Modified;
                entity.Property(nameProperty).IsModified = true;
                await _context.SaveChangesAsync();
        }

        public async Task DeleteRevenue(Guid id)
        {
                await _context.Revenues
               .Where(e => e.IdRevenue == id)
               .ExecuteDeleteAsync();
        }
}
