using System;

namespace CleanArchitecture.Application.ViewModels.Professors;

public sealed record CreateProfessorViewModel(Guid UserId,Guid ResearchGroupId,bool IsCoordinator);