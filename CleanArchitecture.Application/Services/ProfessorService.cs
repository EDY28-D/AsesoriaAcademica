using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Queries.Professors.GetProfessorById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;
using CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class ProfessorService : IProfessorService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public ProfessorService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateProfessorAsync(CreateProfessorViewModel Professor)
    {
        var ProfessorId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateProfessorCommand(
            ProfessorId,Professor.UserId,Professor.ResearchGroupId,Professor.IsCoordinator));

        return ProfessorId;
    }

    public async Task UpdateProfessorAsync(UpdateProfessorViewModel Professor)
    {
        await _bus.SendCommandAsync(new UpdateProfessorCommand(
            Professor.Id,Professor.UserId,Professor.ResearchGroupId,Professor.IsCoordinator));
    }

    public async Task DeleteProfessorAsync(Guid ProfessorId)
    {
        await _bus.SendCommandAsync(new DeleteProfessorCommand(ProfessorId));
    }

    public async Task<ProfessorViewModel?> GetProfessorByIdAsync(Guid ProfessorId)
    {
        var cachedProfessor = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Professor>(ProfessorId),
            async () => await _bus.QueryAsync(new GetProfessorByIdQuery(ProfessorId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedProfessor;
    }

    public async Task<PagedResult<ProfessorViewModel>> GetAllProfessorsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new ProfessorsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}