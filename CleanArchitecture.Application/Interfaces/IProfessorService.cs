using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;

namespace CleanArchitecture.Application.Interfaces;

public interface IProfessorService
{
    public Task<Guid> CreateProfessorAsync(CreateProfessorViewModel Professor);
    public Task UpdateProfessorAsync(UpdateProfessorViewModel Professor);
    public Task DeleteProfessorAsync(Guid ProfessorId);
    public Task<ProfessorViewModel?> GetProfessorByIdAsync(Guid ProfessorId);

    public Task<PagedResult<ProfessorViewModel>> GetAllProfessorsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}