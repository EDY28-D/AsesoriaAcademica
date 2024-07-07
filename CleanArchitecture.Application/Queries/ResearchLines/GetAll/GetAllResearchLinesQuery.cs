using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchLines.GetAll;

public sealed record ResearchLinesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ResearchLineViewModel>>;