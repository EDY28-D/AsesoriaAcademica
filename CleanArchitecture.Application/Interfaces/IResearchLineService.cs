using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;

namespace CleanArchitecture.Application.Interfaces;

public interface IResearchLineService
{
    public Task<Guid> CreateResearchLineAsync(CreateResearchLineViewModel ResearchLine);
    public Task UpdateResearchLineAsync(UpdateResearchLineViewModel ResearchLine);
    public Task DeleteResearchLineAsync(Guid ResearchLineId);
    public Task<ResearchLineViewModel?> GetResearchLineByIdAsync(Guid ResearchLineId);

    public Task<PagedResult<ResearchLineViewModel>> GetAllResearchLinesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}