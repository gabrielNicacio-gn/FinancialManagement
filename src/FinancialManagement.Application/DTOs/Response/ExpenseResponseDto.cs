using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response
{
    public record ExpenseResponseDto(Guid IdExpense, decimal Value, DateTime DateExpense, string Description, Guid IdCategory);
}