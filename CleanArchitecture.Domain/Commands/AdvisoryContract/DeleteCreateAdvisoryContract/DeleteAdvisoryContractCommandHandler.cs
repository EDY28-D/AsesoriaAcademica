using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using MediatR;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteAdvisoryContractCommand>
{
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;
   // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteAdvisoryContractCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAdvisoryContractRepository AdvisoryContractRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteAdvisoryContractCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
/*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete AdvisoryContract {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

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
/*
        var AdvisoryContractUsers = _userRepository
            .GetAll()
            .Where(x => x.AdvisoryContractId == request.AggregateId);

        _userRepository.RemoveRange(AdvisoryContractUsers);*/

        _AdvisoryContractRepository.Remove(AdvisoryContract);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AdvisoryContractDeletedEvent(AdvisoryContract.Id,AdvisoryContract.ProfessorId,AdvisoryContract.StudentId,AdvisoryContract.ResearchLineId));
        }
    }
}