using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Student;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class StudentEventHandler :
    INotificationHandler<StudentCreatedEvent>,
    INotificationHandler<StudentDeletedEvent>,
    INotificationHandler<StudentUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public StudentEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(StudentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(StudentDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Student>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(StudentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Student>(notification.AggregateId),
            cancellationToken);
    }
}