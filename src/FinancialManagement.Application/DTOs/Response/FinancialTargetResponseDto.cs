using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Response;


public record FinancialTargetResponseDto(Guid IdFinancialTarget, string Title, decimal ValueNeeded,
DateTime DateLimit, string Status, string Description);


