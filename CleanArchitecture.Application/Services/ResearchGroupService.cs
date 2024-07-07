using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.ResearchGroups.GetAll;
using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;
using CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;
using CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Domain.Commands;
namespace CleanArchitecture.Application.Services;

public sealed class ResearchGroupService : IResearchGroupService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ResearchGroupService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateResearchGroupAsync(CreateResearchGroupViewModel ResearchGroup)
    {
        var ResearchGroupId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateResearchGroupCommand(
            ResearchGroupId,
            ResearchGroup.Name,ResearchGroup.Code));

        return ResearchGroupId;
    }

    public async Task UpdateResearchGroupAsync(UpdateResearchGroupViewModel ResearchGroup)
    {
        await _bus.SendCommandAsync(new UpdateResearchGroupCommand(
            ResearchGroup.Id,
            ResearchGroup.Name));
    }

    public async Task DeleteResearchGroupAsync(Guid ResearchGroupId)
    {
        await _bus.SendCommandAsync(new DeleteResearchGroupCommand(ResearchGroupId));
    }

    public async Task<ResearchGroupViewModel?> GetResearchGroupByIdAsync(Guid ResearchGroupId)
    {
        var cachedResearchGroup = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchGroup>(ResearchGroupId),
            async () => await _bus.QueryAsync(new GetResearchGroupByIdQuery(ResearchGroupId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedResearchGroup;
    }

    public async Task<PagedResult<ResearchGroupViewModel>> GetAllResearchGroupsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new ResearchGroupsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}