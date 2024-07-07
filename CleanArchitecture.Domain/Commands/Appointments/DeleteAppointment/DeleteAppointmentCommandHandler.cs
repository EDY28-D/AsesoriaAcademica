using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;

public sealed class DeleteAppointmentCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteAppointmentCommand>
{
    private readonly IAppointmentRepository _AppointmentRepository;
    // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteAppointmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAppointmentRepository AppointmentRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AppointmentRepository = AppointmentRepository;
        //  _userRepository = userRepository;
        // _user = user;
    }

    public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
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
                            $"No permission to delete Appointment {request.AggregateId}",
                            ErrorCodes.InsufficientPermissions));

                    return;
                }*/

        var Appointment = await _AppointmentRepository.GetByIdAsync(request.AggregateId);

        if (Appointment is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Appointment with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }
        /*
                var AppointmentUsers = _userRepository
                    .GetAll()
                    .Where(x => x.AppointmentId == request.AggregateId);

                _userRepository.RemoveRange(AppointmentUsers);*/

        _AppointmentRepository.Remove(Appointment);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AppointmentDeletedEvent(Appointment.Id));
        }
    }
}