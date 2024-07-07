using System;
using CleanArchitecture.Application.ViewModels.Professors;
using MediatR;

namespace CleanArchitecture.Application.Queries.Professors.GetProfessorById;

public sealed record GetProfessorByIdQuery(Guid ProfessorId) : IRequest<ProfessorViewModel?>;