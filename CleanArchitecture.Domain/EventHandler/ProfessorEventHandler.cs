using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Professor;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class ProfessorEventHandler :
    INotificationHandler<ProfessorCreatedEvent>,
    INotificationHandler<ProfessorDeletedEvent>,
    INotificationHandler<ProfessorUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ProfessorEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(ProfessorCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ProfessorDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Professor>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(ProfessorUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Professor>(notification.AggregateId),
            cancellationToken);
    }
}