using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Professor;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;

public sealed class DeleteProfessorCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteProfessorCommand>
{
    private readonly IProfessorRepository _ProfessorRepository;
   // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteProfessorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProfessorRepository ProfessorRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProfessorRepository = ProfessorRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Professor {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        var Professor = await _ProfessorRepository.GetByIdAsync(request.AggregateId);

        if (Professor is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Professor with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }
/*
        var ProfessorUsers = _userRepository
            .GetAll()
            .Where(x => x.ProfessorId == request.AggregateId);

        _userRepository.RemoveRange(ProfessorUsers);*/

        _ProfessorRepository.Remove(Professor);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProfessorDeletedEvent(Professor.Id,Professor.UserId));
        }
    }
}