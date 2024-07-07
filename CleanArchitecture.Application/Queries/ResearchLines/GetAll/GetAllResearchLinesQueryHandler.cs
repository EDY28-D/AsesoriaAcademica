using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.ResearchLines.GetAll;

public sealed class GetAllResearchLinesQueryHandler :
    IRequestHandler<ResearchLinesQuery, PagedResult<ResearchLineViewModel>>
{
    private readonly ISortingExpressionProvider<ResearchLineViewModel, ResearchLine> _sortingExpressionProvider;
    private readonly IResearchLineRepository _ResearchLineRepository;

    public GetAllResearchLinesQueryHandler(
        IResearchLineRepository ResearchLineRepository,
        ISortingExpressionProvider<ResearchLineViewModel, ResearchLine> sortingExpressionProvider)
    {
        _ResearchLineRepository = ResearchLineRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ResearchLineViewModel>> Handle(
        ResearchLinesQuery request,
        CancellationToken cancellationToken)
    {
        var ResearchLinesQuery = _ResearchLineRepository
    .GetAllNoTracking()
    .IgnoreQueryFilters()
    .Include(x => x.ResearchGroup) // Include the related entity without the Where clause
    .Where(x => request.IncludeDeleted || !x.Deleted);


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            ResearchLinesQuery = ResearchLinesQuery.Where(ResearchLine =>
                ResearchLine.Name.Contains(request.SearchTerm));
        }

        var totalCount = await ResearchLinesQuery.CountAsync(cancellationToken);

        ResearchLinesQuery = ResearchLinesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var ResearchLines = await ResearchLinesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(ResearchLine => ResearchLineViewModel.FromResearchLine(ResearchLine))
            .ToListAsync(cancellationToken);

        return new PagedResult<ResearchLineViewModel>(
            totalCount, ResearchLines, request.Query.Page, request.Query.PageSize);
    }
}