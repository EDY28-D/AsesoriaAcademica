using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;
using MediatR;

namespace CleanArchitecture.Application.Queries.Professors.GetAll;

public sealed record ProfessorsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ProfessorViewModel>>;