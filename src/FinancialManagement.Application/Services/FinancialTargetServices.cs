
using FinancialManagement.Application.DTOs.Request.FinancialTarget;
using FinancialManagement.Application.DTOs.Response;
using FinancialManagement.Application.DTOs.Shared;
using FinancialManagement.Application.Interfaces.Services;
using FinancialManagement.Domain.Enums;
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
        public async Task<BaseResponseDto<FinancialTargetResponseDto>> CreateNewFinancialTarget(CreateFinancialTargetDto newFinancialTarget)
        {
                var financialTarget = new FinancialTarget
                {
                        Title = newFinancialTarget.Title,
                        ValueNeeded = newFinancialTarget.ValueNeeded,
                        DateLimit = newFinancialTarget.DateLimit,
                        Description = newFinancialTarget.Description,
                        Status = StatusFinancialTarget.Accumulating
                };
                var financialTargetCreated = await _financialTargetRepository.AddFinancialTarget(financialTarget);
                _logger.LogInformation($"Financial Target created with success{financialTargetCreated.IdFinancialTarget}");
                var newFinancialTargetResponse = new FinancialTargetResponseDto(financialTargetCreated.IdFinancialTarget,
                financialTargetCreated.Title, financialTargetCreated.ValueNeeded, financialTargetCreated.DateLimit, financialTargetCreated.Status.ToString(),
                financialTargetCreated.Description);
                return new BaseResponseDto<FinancialTargetResponseDto>(newFinancialTargetResponse);
        }

        public async Task<BaseResponseDto<IEnumerable<FinancialTargetResponseDto>>> GetAllFinancialTarget()
        {
                var financialTargets = await _financialTargetRepository.GetFinancialTargets();
                _logger.LogInformation("Return all Financial Targets");
                var listFinancilTargets = financialTargets.Select(financialTarget => new FinancialTargetResponseDto(financialTarget.IdFinancialTarget,
                financialTarget.Title, financialTarget.ValueNeeded, financialTarget.DateLimit, financialTarget.Status.ToString(),
                financialTarget.Description));
                return new BaseResponseDto<IEnumerable<FinancialTargetResponseDto>>(listFinancilTargets);
        }

        public async Task<BaseResponseDto<FinancialTargetResponseDto>> GetFinancialTargetById(Guid idFinancialTarget)
        {
                var financialTarget = await _financialTargetRepository.GetFinancialTargetById(idFinancialTarget);

                if (financialTarget is null)
                {
                        _logger.LogInformation($"Financial Target not found");
                        return new BaseResponseDto<FinancialTargetResponseDto>(false);
                }

                _logger.LogInformation($"Found Fiancial Target with Id{financialTarget.IdFinancialTarget}");
                var financialTargetResponse = new FinancialTargetResponseDto(financialTarget.IdFinancialTarget, financialTarget.Title,
                financialTarget.ValueNeeded, financialTarget.DateLimit, financialTarget.Status.ToString(), financialTarget.Description);
                return new BaseResponseDto<FinancialTargetResponseDto>(financialTargetResponse);
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
                };
                await _financialTargetRepository.UpdateFinancialTarget(financialTarget,
                nameProperty);
                _logger.LogInformation("Financial Target Updated");
        }
}
