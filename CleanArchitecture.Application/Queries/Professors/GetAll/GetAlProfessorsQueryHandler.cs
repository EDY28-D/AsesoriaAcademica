using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Professors.GetAll;

public sealed class GetAllProfessorsQueryHandler :
    IRequestHandler<ProfessorsQuery, PagedResult<ProfessorViewModel>>
{
    private readonly ISortingExpressionProvider<ProfessorViewModel, Professor> _sortingExpressionProvider;
    private readonly IProfessorRepository _ProfessorRepository;

    public GetAllProfessorsQueryHandler(
        IProfessorRepository ProfessorRepository,
        ISortingExpressionProvider<ProfessorViewModel, Professor> sortingExpressionProvider)
    {
        _ProfessorRepository = ProfessorRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ProfessorViewModel>> Handle(
        ProfessorsQuery request,
        CancellationToken cancellationToken)
    {
        var ProfessorsQuery = _ProfessorRepository
    .GetAllNoTracking()
    .IgnoreQueryFilters()
    .Include(x => x.User)
      .Include(x => x.ResearchGroup)
    .Where(x => request.IncludeDeleted || !x.Deleted);


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
          //  ProfessorsQuery = ProfessorsQuery.Where(Professor =>
            //    Professor.ResearchGroup.Contains(request.SearchTerm));
        }

        var totalCount = await ProfessorsQuery.CountAsync(cancellationToken);

        ProfessorsQuery = ProfessorsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Professors = await ProfessorsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(Professor => ProfessorViewModel.FromProfessor(Professor))
            .ToListAsync(cancellationToken);

        return new PagedResult<ProfessorViewModel>(
            totalCount, Professors, request.Query.Page, request.Query.PageSize);
    }
}