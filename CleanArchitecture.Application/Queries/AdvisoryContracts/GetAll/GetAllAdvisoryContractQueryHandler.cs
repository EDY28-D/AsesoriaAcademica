using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;

public sealed class GetAllAdvisoryContractsQueryHandler :
    IRequestHandler<AdvisoryContractsQuery, PagedResult<AdvisoryContractViewModel>>
{
    private readonly ISortingExpressionProvider<AdvisoryContractViewModel, AdvisoryContract> _sortingExpressionProvider;
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;

    public GetAllAdvisoryContractsQueryHandler(
        IAdvisoryContractRepository AdvisoryContractRepository,
        ISortingExpressionProvider<AdvisoryContractViewModel, AdvisoryContract> sortingExpressionProvider)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<AdvisoryContractViewModel>> Handle(
        AdvisoryContractsQuery request,
        CancellationToken cancellationToken)
    {
        var AdvisoryContractsQuery = _AdvisoryContractRepository
    .GetAllNoTracking()
    .IgnoreQueryFilters()
    .Include(x => x.Student)
    .Where(x => request.IncludeDeleted || !x.Deleted);


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            //  AdvisoryContractsQuery = AdvisoryContractsQuery.Where(AdvisoryContract =>
            //    AdvisoryContract.ResearchGroup.Contains(request.SearchTerm));
        }

        var totalCount = await AdvisoryContractsQuery.CountAsync(cancellationToken);

        AdvisoryContractsQuery = AdvisoryContractsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var AdvisoryContracts = await AdvisoryContractsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(AdvisoryContract => AdvisoryContractViewModel.FromAdvisoryContract(AdvisoryContract))
            .ToListAsync(cancellationToken);

        return new PagedResult<AdvisoryContractViewModel>(
            totalCount, AdvisoryContracts, request.Query.Page, request.Query.PageSize);
    }
}