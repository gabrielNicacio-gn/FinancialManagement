using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response
{
    public class ExpenseResponseDto<T>
    {
        public T Data { get; set; } = default!;

        public ExpenseResponseDto(T data)
        {
            Data = data;
        }
    }
}