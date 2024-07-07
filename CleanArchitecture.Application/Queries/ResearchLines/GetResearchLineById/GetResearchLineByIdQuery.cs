using System;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;

public sealed record GetResearchLineByIdQuery(Guid ResearchLineId) : IRequest<ResearchLineViewModel?>;