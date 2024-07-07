using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchLine;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;

public sealed class DeleteResearchLineCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteResearchLineCommand>
{
    private readonly IResearchLineRepository _ResearchLineRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteResearchLineCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchLineRepository ResearchLineRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchLineRepository = ResearchLineRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteResearchLineCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete ResearchLine {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var ResearchLine = await _ResearchLineRepository.GetByIdAsync(request.AggregateId);

        if (ResearchLine is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no ResearchLine with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }
/*
        var ResearchLineUsers = _userRepository
            .GetAll()
            .Where(x => x.ResearchLineId == request.AggregateId);

        _userRepository.RemoveRange(ResearchLineUsers);*/

        _ResearchLineRepository.Remove(ResearchLine);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchLineDeletedEvent(ResearchLine.Id,ResearchLine.ResearchGroupId));
        }
    }
}