using System;

namespace CleanArchitecture.Application.ViewModels.AdvisoryContracts;

public sealed record UpdateAdvisoryContractViewModel(
    Guid Id,
     Guid professorId, Guid studentId, Guid researchLineId, string thesisTopic, string message, string status);