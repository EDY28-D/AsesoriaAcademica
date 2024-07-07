using System;

namespace CleanArchitecture.Application.ViewModels.AdvisoryContracts;

public sealed record CreateAdvisoryContractViewModel(Guid professorId, Guid studentId, Guid researchLineId, string thesisTopic, string message, string status);