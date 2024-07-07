using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.ResearchGroups.GetAll;

public sealed class GetAllResearchGroupsQueryHandler :
    IRequestHandler<ResearchGroupsQuery, PagedResult<ResearchGroupViewModel>>
{
    private readonly ISortingExpressionProvider<ResearchGroupViewModel, ResearchGroup> _sortingExpressionProvider;
    private readonly IResearchGroupRepository _ResearchGroupRepository;

    public GetAllResearchGroupsQueryHandler(
        IResearchGroupRepository ResearchGroupRepository,
        ISortingExpressionProvider<ResearchGroupViewModel, ResearchGroup> sortingExpressionProvider)
    {
        _ResearchGroupRepository = ResearchGroupRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ResearchGroupViewModel>> Handle(
        ResearchGroupsQuery request,
        CancellationToken cancellationToken)
    {
        var ResearchGroupsQuery = _ResearchGroupRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.ResearchLines.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            ResearchGroupsQuery = ResearchGroupsQuery.Where(ResearchGroup =>
                ResearchGroup.Name.Contains(request.SearchTerm));
        }

        var totalCount = await ResearchGroupsQuery.CountAsync(cancellationToken);

        ResearchGroupsQuery = ResearchGroupsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var ResearchGroups = await ResearchGroupsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(ResearchGroup => ResearchGroupViewModel.FromResearchGroup(ResearchGroup))
            .ToListAsync(cancellationToken);

        return new PagedResult<ResearchGroupViewModel>(
            totalCount, ResearchGroups, request.Query.Page, request.Query.PageSize);
    }
}