using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Students.GetAll;

public sealed class GetAllStudentsQueryHandler :
    IRequestHandler<StudentsQuery, PagedResult<StudentViewModel>>
{
    private readonly ISortingExpressionProvider<StudentViewModel, Student> _sortingExpressionProvider;
    private readonly IStudentRepository _StudentRepository;

    public GetAllStudentsQueryHandler(
        IStudentRepository StudentRepository,
        ISortingExpressionProvider<StudentViewModel, Student> sortingExpressionProvider)
    {
        _StudentRepository = StudentRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<StudentViewModel>> Handle(
        StudentsQuery request,
        CancellationToken cancellationToken)
    {
        var StudentsQuery = _StudentRepository
    .GetAllNoTracking()
    .IgnoreQueryFilters()
    .Include(x => x.User) // Include the related entity without the Where clause
    .Where(x => request.IncludeDeleted || !x.Deleted);


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            StudentsQuery = StudentsQuery.Where(Student =>
                Student.Code.Contains(request.SearchTerm));
        }

        var totalCount = await StudentsQuery.CountAsync(cancellationToken);

        StudentsQuery = StudentsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Students = await StudentsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(Student => StudentViewModel.FromStudent(Student))
            .ToListAsync(cancellationToken);

        return new PagedResult<StudentViewModel>(
            totalCount, Students, request.Query.Page, request.Query.PageSize);
    }
}

