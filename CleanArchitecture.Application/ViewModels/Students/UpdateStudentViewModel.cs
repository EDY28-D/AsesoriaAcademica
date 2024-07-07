using System;

namespace CleanArchitecture.Application.ViewModels.Students;

public sealed record UpdateStudentViewModel(
    Guid Id,
    Guid UserId,String Code);