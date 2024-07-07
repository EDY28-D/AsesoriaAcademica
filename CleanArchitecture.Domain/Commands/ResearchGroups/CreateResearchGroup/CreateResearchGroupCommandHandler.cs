using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchGroup;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;

public sealed class CreateResearchGroupCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateResearchGroupCommand>
{
    private readonly IResearchGroupRepository _ResearchGroupRepository;
    private readonly IUser _user;

    public CreateResearchGroupCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchGroupRepository ResearchGroupRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchGroupRepository = ResearchGroupRepository;
        _user = user;
    }

    public async Task Handle(CreateResearchGroupCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _ResearchGroupRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a ResearchGroup with Id {request.AggregateId}",
                    DomainErrorCodes.ResearchGroup.AlreadyExists));

            return;
        }

        var ResearchGroup = new ResearchGroup(
            request.AggregateId,
            request.Name,request.Code);

        _ResearchGroupRepository.Add(ResearchGroup);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchGroupCreatedEvent(
                ResearchGroup.Id,
                ResearchGroup.Name));
        }
    }
}