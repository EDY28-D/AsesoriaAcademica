using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;
/// <summary>
/// By Perez
/// </summary>
public sealed class AdvisoryContractService : IAdvisoryContractService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public AdvisoryContractService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateAdvisoryContractAsync(CreateAdvisoryContractViewModel AdvisoryContract)
    {
        var AdvisoryContractId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateAdvisoryContractCommand(
            AdvisoryContractId,AdvisoryContract.professorId,AdvisoryContract.studentId,AdvisoryContract.researchLineId,AdvisoryContract.thesisTopic,AdvisoryContract.message,AdvisoryContract.status));

        return AdvisoryContractId;
    }

    public async Task UpdateAdvisoryContractAsync(UpdateAdvisoryContractViewModel AdvisoryContract)
    {
        await _bus.SendCommandAsync(new UpdateAdvisoryContractCommand(AdvisoryContract.Id,AdvisoryContract.professorId, AdvisoryContract.studentId, AdvisoryContract.researchLineId, AdvisoryContract.thesisTopic, AdvisoryContract.message, AdvisoryContract.status));
    }

    public async Task DeleteAdvisoryContractAsync(Guid AdvisoryContractId)
    {
        await _bus.SendCommandAsync(new DeleteAdvisoryContractCommand(AdvisoryContractId));
    }

    public async Task<AdvisoryContractViewModel?> GetAdvisoryContractByIdAsync(Guid AdvisoryContractId)
    {
        var cachedAdvisoryContract = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<AdvisoryContract>(AdvisoryContractId),
            async () => await _bus.QueryAsync(new GetAdvisoryContractByIdQuery(AdvisoryContractId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedAdvisoryContract;
    }

    public async Task<PagedResult<AdvisoryContractViewModel>> GetAllAdvisoryContractsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new AdvisoryContractsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}
