using System;

namespace CleanArchitecture.Application.ViewModels.ResearchGroups;

public sealed record UpdateResearchGroupViewModel(
    Guid Id,
    string Name,String Code);