using System;

namespace CleanArchitecture.Shared.SAdvisoryContract;

public sealed record AdvisoryContractViewModel(
    Guid Id,
    Guid ProfessorId,
    Guid StudentId,
    Guid ResearchLineId,
    string tesisThopic,
    bool IsDeleted);