using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Interfaces.Repositories;
using FinancialManagement.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Application.Services;
public class FinancialTargetServices : IFinancialTargetServices
{
        private readonly IFinancialTargetRepository _financialTargetRepository;
        private readonly ILogger<FinancialTargetServices> _logger;
        public FinancialTargetServices(IFinancialTargetRepository financialTargetRepository, ILogger<FinancialTargetServices> logger)
        {
                _financialTargetRepository = financialTargetRepository;
                _logger = logger;
        }
        public async Task<FinancialTargetResponseDto> CreateNewFinancialTarget(CreateFinancialTargetDto newFinancialTarget)
        {
                var financialTarget = new FinancialTarget
                {
                        Title = newFinancialTarget.Title,
                        ValueNeeded = newFinancialTarget.ValueNeeded,
                        DateLimit = newFinancialTarget.DateLimit,
                        Description = newFinancialTarget.Description,
                        Status = newFinancialTarget.Status
                };
                var financialTargetCreated = await _financialTargetRepository.AddFinancialTarget(financialTarget);
                _logger.LogInformation($"Financial Target created with success{financialTargetCreated.IdFinancialTarget}");
                return new FinancialTargetResponseDto(financialTargetCreated.IdFinancialTarget,
                financialTargetCreated.Title, financialTargetCreated.ValueNeeded, financialTargetCreated.DateLimit, financialTargetCreated.Status,
                financialTargetCreated.Description);
        }

        public async Task<IEnumerable<FinancialTargetResponseDto>> GetAllFinancialTarget()
        {
                var financialTargets = await _financialTargetRepository.GetFinancialTargets();
                _logger.LogInformation("Return all Financial Targets");
                return financialTargets.Select(financialTarget => new FinancialTargetResponseDto(financialTarget.IdFinancialTarget,
                financialTarget.Title, financialTarget.ValueNeeded, financialTarget.DateLimit, financialTarget.Status,
                financialTarget.Description));
        }

        public async Task<FinancialTargetResponseDto> GetFinancialTargetById(Guid idFinancialTarget)
        {
                var financialTarget = await _financialTargetRepository.GetFinancialTargetById(idFinancialTarget)
                ?? throw new Exception("Financial Target Not Found");
                _logger.LogInformation($"Found Fiancial Target with Id{financialTarget.IdFinancialTarget}");
                return new FinancialTargetResponseDto(financialTarget.IdFinancialTarget, financialTarget.Title,
                financialTarget.ValueNeeded, financialTarget.DateLimit, financialTarget.Status, financialTarget.Description);
        }

        public async Task RemoveFinancialTarget(Guid idFinancialTarget)
        {
                await _financialTargetRepository.DeleteFinancialTarget(idFinancialTarget);
                _logger.LogInformation($"Financial Target Deleted with {idFinancialTarget}");
        }

        public async Task UpdateFinancialTarget(UpdateFinancialTargetDto updateFinancialTarget, string nameProperty)
        {
                var financialTarget = new FinancialTarget
                {
                        Title = updateFinancialTarget.Title,
                        ValueNeeded = updateFinancialTarget.ValueNeeded,
                        DateLimit = updateFinancialTarget.DateLimit,
                        Description = updateFinancialTarget.Description,
                        Status = updateFinancialTarget.Status
                };
                await _financialTargetRepository.UpdateFinancialTarget(financialTarget,
                nameProperty);
                _logger.LogInformation("Financial Target Updated");
        }
}
