using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;

namespace CleanArchitecture.Application.Interfaces;

public interface IResearchGroupService
{
    public Task<Guid> CreateResearchGroupAsync(CreateResearchGroupViewModel ResearchGroup);
    public Task UpdateResearchGroupAsync(UpdateResearchGroupViewModel ResearchGroup);
    public Task DeleteResearchGroupAsync(Guid ResearchGroupId);
    public Task<ResearchGroupViewModel?> GetResearchGroupByIdAsync(Guid ResearchGroupId);

    public Task<PagedResult<ResearchGroupViewModel>> GetAllResearchGroupsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}