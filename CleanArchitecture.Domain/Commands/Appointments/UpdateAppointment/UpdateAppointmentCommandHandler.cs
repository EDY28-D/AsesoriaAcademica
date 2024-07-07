using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;

public sealed class UpdateAppointmentCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateAppointmentCommand>
{
    private readonly IAppointmentRepository _AppointmentRepository;
    private readonly IUser _user;

    public UpdateAppointmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAppointmentRepository AppointmentRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AppointmentRepository = AppointmentRepository;
        _user = user;
    }

    public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update Appointment {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

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


        Appointment.SetDateTime(request.DateTime);
        Appointment.SetProfessorProgress(request.ProfessorProgress);
        Appointment.SetStudentProgress(request.StudentProgress);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AppointmentUpdatedEvent(
                Appointment.Id, Appointment.ProfessorId, Appointment.StudentId  , Appointment.CalendarId , Appointment.DateTime,Appointment.ProfessorProgress , Appointment.StudentProgress));
        }
    }
}