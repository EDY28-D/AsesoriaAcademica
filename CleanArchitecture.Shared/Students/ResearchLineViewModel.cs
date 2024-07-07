using System;

namespace CleanArchitecture.Shared.Students;

public sealed record ResearchLineViewModel(
    Guid Id,
    Guid UserId,
    string Code,
    bool IsDeleted);