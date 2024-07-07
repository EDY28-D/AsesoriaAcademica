using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using MediatR;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateAdvisoryContractCommand>
{
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;
    private readonly IUser _user;

    public UpdateAdvisoryContractCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAdvisoryContractRepository AdvisoryContractRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
        _user = user;
    }

    public async Task Handle(UpdateAdvisoryContractCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update AdvisoryContract {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var AdvisoryContract = await _AdvisoryContractRepository.GetByIdAsync(request.AggregateId);

        if (AdvisoryContract is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no AdvisoryContract with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        AdvisoryContract.SetStatus(request.Status);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AdvisoryContractUpdatedEvent(
                AdvisoryContract.Id,AdvisoryContract.ProfessorId,AdvisoryContract.StudentId,AdvisoryContract.ResearchLineId));
        }
    }
}