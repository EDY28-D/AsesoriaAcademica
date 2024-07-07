using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;

namespace CleanArchitecture.Application.Interfaces;

public interface IStudentService
{
    public Task<Guid> CreateStudentAsync(CreateStudentViewModel Student);
    public Task UpdateStudentAsync(UpdateStudentViewModel Student);
    public Task DeleteStudentAsync(Guid StudentId);
    public Task<StudentViewModel?> GetStudentByIdAsync(Guid StudentId);

    public Task<PagedResult<StudentViewModel>> GetAllStudentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}