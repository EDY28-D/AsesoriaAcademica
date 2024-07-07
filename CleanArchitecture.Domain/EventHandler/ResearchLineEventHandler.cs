using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.ResearchLine;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class ResearchLineEventHandler :
    INotificationHandler<ResearchLineCreatedEvent>,
    INotificationHandler<ResearchLineDeletedEvent>,
    INotificationHandler<ResearchLineUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ResearchLineEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(ResearchLineCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ResearchLineDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchLine>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(ResearchLineUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchLine>(notification.AggregateId),
            cancellationToken);
    }
}