using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchGroup;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteResearchGroupCommand>
{
    private readonly IResearchGroupRepository _ResearchGroupRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteResearchGroupCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchGroupRepository ResearchGroupRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchGroupRepository = ResearchGroupRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteResearchGroupCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete ResearchGroup {request.AggregateId}",
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
/*
        var ResearchGroupUsers = _userRepository
            .GetAll()
            .Where(x => x.ResearchGroupId == request.AggregateId);

        _userRepository.RemoveRange(ResearchGroupUsers);*/

        _ResearchGroupRepository.Remove(ResearchGroup);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchGroupDeletedEvent(ResearchGroup.Id));
        }
    }
}