using System;

namespace CleanArchitecture.Shared.ResearchLines;

public sealed record ResearchLineViewModel(
    Guid Id,
     string Name,
    string Code,
    bool IsDeleted);