using System;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;

public sealed record GetResearchGroupByIdQuery(Guid ResearchGroupId) : IRequest<ResearchGroupViewModel?>;