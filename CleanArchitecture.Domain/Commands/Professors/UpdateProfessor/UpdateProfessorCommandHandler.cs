using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Professor;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;

public sealed class UpdateProfessorCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateProfessorCommand>
{
    private readonly IProfessorRepository _ProfessorRepository;
    private readonly IUser _user;

    public UpdateProfessorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProfessorRepository ProfessorRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProfessorRepository = ProfessorRepository;
        _user = user;
    }

    public async Task Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update Professor {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

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

        Professor.SetCoordinator(request.IsCoordinator);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProfessorUpdatedEvent(
                Professor.Id,Professor.UserId));
        }
    }
}