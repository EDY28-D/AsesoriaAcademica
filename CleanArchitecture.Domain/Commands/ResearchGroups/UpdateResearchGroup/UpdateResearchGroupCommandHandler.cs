using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchGroup;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;

public sealed class UpdateResearchGroupCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateResearchGroupCommand>
{
    private readonly IResearchGroupRepository _ResearchGroupRepository;
    private readonly IUser _user;

    public UpdateResearchGroupCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchGroupRepository ResearchGroupRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchGroupRepository = ResearchGroupRepository;
        _user = user;
    }

    public async Task Handle(UpdateResearchGroupCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var ResearchGroup = await _ResearchGroupRepository.GetByIdAsync(request.AggregateId);

        if (ResearchGroup is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no ResearchGroup with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        ResearchGroup.SetName(request.Name);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchGroupUpdatedEvent(
                ResearchGroup.Id,
                ResearchGroup.Name));
        }
    }
}