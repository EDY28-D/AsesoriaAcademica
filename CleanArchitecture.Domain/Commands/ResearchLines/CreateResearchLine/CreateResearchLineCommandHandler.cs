using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.ResearchLine;
using MediatR;

namespace CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;

public sealed class CreateResearchLineCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateResearchLineCommand>
{
    private readonly IResearchLineRepository _ResearchLineRepository;
    private readonly IUser _user;
    private readonly IResearchGroupRepository _researchGroupRepository;

    public CreateResearchLineCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IResearchLineRepository ResearchLineRepository,
        IResearchGroupRepository researchGroupRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ResearchLineRepository = ResearchLineRepository;
        _user = user;
        _researchGroupRepository = researchGroupRepository;
    }

    public async Task Handle(CreateResearchLineCommand request, CancellationToken cancellationToken)
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
                  $"No permission to create ResearchLine {request.AggregateId}",
                   ErrorCodes.InsufficientPermissions));

           return;
        }

        if (await _ResearchLineRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a ResearchLine with Id {request.AggregateId}",
                    DomainErrorCodes.ResearchLine.AlreadyExists));

            return;
        }

        var ResearchLine = new ResearchLine(
            request.ResearchLineId,
            request.Name,request.ResearchGroupId,request.Code);

        _ResearchLineRepository.Add(ResearchLine);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ResearchLineCreatedEvent(
                ResearchLine.Id,
                ResearchLine.ResearchGroupId));
        }
    }
}