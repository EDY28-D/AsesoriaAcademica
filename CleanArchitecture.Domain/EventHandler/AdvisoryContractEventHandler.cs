using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class AdvisoryContractEventHandler :
    INotificationHandler<AdvisoryContractCreatedEvent>,
    INotificationHandler<AdvisoryContractDeletedEvent>,
    INotificationHandler<AdvisoryContractUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public AdvisoryContractEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(AdvisoryContractCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(AdvisoryContractDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<AdvisoryContract>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(AdvisoryContractUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<AdvisoryContract>(notification.AggregateId),
            cancellationToken);
    }
}