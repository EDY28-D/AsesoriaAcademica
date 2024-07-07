using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.ResearchLines.GetAll;
using CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;
using CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;
using CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class ResearchLineService : IResearchLineService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ResearchLineService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateResearchLineAsync(CreateResearchLineViewModel ResearchLine)
    {
        var ResearchLineId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateResearchLineCommand(
            ResearchLineId,ResearchLine.ResearchGroupId,ResearchLine.Name,ResearchLine.Code));

        return ResearchLineId;
    }

    public async Task UpdateResearchLineAsync(UpdateResearchLineViewModel ResearchLine)
    {
        await _bus.SendCommandAsync(new UpdateResearchLineCommand(
            ResearchLine.Id,ResearchLine.ResearchGroupId,
            ResearchLine.Code));
    }

    public async Task DeleteResearchLineAsync(Guid ResearchLineId)
    {
        await _bus.SendCommandAsync(new DeleteResearchLineCommand(ResearchLineId));
    }

    public async Task<ResearchLineViewModel?> GetResearchLineByIdAsync(Guid ResearchLineId)
    {
        var cachedResearchLine = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchLine>(ResearchLineId),
            async () => await _bus.QueryAsync(new GetResearchLineByIdQuery(ResearchLineId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedResearchLine;
    }

    public async Task<PagedResult<ResearchLineViewModel>> GetAllResearchLinesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new ResearchLinesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}