using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchGroups.GetAll;

public sealed record ResearchGroupsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ResearchGroupViewModel>>;