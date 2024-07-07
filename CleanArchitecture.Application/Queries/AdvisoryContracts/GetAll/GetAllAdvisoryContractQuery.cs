using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using MediatR;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;

public sealed record AdvisoryContractsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<AdvisoryContractViewModel>>;