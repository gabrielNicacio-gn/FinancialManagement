using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response
{
    public record RevenueResponseDto(Guid IdRevenue, decimal Value, DateTime DateRevenue, string Description);
}