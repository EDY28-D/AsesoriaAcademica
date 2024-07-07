using System;

namespace CleanArchitecture.Application.ViewModels.Professors;

public sealed record UpdateProfessorViewModel(
    Guid Id,
    Guid UserId, Guid ResearchGroupId, bool IsCoordinator);