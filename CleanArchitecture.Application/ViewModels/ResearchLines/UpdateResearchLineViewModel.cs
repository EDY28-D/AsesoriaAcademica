using System;

namespace CleanArchitecture.Application.ViewModels.ResearchLines;

public sealed record UpdateResearchLineViewModel(
    Guid Id,Guid ResearchGroupId,
    string Name,String Code);