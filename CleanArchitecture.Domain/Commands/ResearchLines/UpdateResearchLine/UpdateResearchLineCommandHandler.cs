using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchLine;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;

public sealed class UpdateResearchLineCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateResearchLineCommand>
{
    private readonly IResearchLineRepository _ResearchLineRepository;
    private readonly IUser _user;

    public UpdateResearchLineCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchLineRepository ResearchLineRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchLineRepository = ResearchLineRepository;
        _user = user;
    }

    public async Task Handle(UpdateResearchLineCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update ResearchLine {request.AggregateId}",
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

        ResearchLine.SetName(request.Name);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchLineUpdatedEvent(
                ResearchLine.Id,
                ResearchLine.ResearchGroupId));
        }
    }
}