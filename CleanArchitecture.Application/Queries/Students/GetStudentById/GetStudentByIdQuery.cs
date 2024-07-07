using System;
using CleanArchitecture.Application.ViewModels.Students;
using MediatR;

namespace CleanArchitecture.Application.Queries.Students.GetStudentById;

public sealed record GetStudentByIdQuery(Guid StudentId) : IRequest<StudentViewModel?>;