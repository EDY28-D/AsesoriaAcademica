using System;

namespace CleanArchitecture.Application.ViewModels.Students;

public sealed record CreateStudentViewModel(Guid UserId,string Code);