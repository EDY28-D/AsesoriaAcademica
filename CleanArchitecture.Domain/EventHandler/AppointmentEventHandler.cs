using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class AppointmentEventHandler :
    INotificationHandler<AppointmentCreatedEvent>,
    INotificationHandler<AppointmentDeletedEvent>,
    INotificationHandler<AppointmentUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public AppointmentEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(AppointmentDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Appointment>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(AppointmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Appointment>(notification.AggregateId),
            cancellationToken);
    }
}