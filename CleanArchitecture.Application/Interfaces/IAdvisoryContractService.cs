using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;

namespace CleanArchitecture.Application.Interfaces;

public interface IAdvisoryContractService
{
    public Task<Guid> CreateAdvisoryContractAsync(CreateAdvisoryContractViewModel AdvisoryContract);
    public Task UpdateAdvisoryContractAsync(UpdateAdvisoryContractViewModel AdvisoryContract);
    public Task DeleteAdvisoryContractAsync(Guid AdvisoryContractId);
    public Task<AdvisoryContractViewModel?> GetAdvisoryContractByIdAsync(Guid AdvisoryContractId);

    public Task<PagedResult<AdvisoryContractViewModel>> GetAllAdvisoryContractsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}