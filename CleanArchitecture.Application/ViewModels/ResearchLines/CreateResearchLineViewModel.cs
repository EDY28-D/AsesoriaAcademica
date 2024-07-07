using System;

namespace CleanArchitecture.Application.ViewModels.ResearchLines;
public sealed record CreateResearchLineViewModel(Guid ResearchGroupId,string Name,string Code);