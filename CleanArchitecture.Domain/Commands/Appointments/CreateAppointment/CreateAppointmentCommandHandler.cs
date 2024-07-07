using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateAppointmentCommand>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUser _user;

    public CreateAppointmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAppointmentRepository appointmentRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _appointmentRepository = appointmentRepository;
        _user = user;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        try
        {
            //var userRole = _user.GetUserRole();
            //if (userRole != UserRole.Admin)
            //{
            //    await NotifyAsync(
            //        new DomainNotification(
            //            request.MessageType,
            //            $"No permission to create Appointment {request.AggregateId}",
            //            ErrorCodes.InsufficientPermissions));

            //    return;
            //}
        }
        catch (ArgumentException ex)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"Failed to parse user role: {ex.Message}",
                    ErrorCodes.InvalidUserRole));
            return;
        }

        if (await _appointmentRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already an Appointment with Id {request.AggregateId}",
                    DomainErrorCodes.Appointment.AlreadyExists));

            return;
        }

        var appointment = new Appointment(
            request.AggregateId, request.ProfessorId, request.StudentId, request.CalendarId, request.DateTime, request.ProfessorProgress, request.StudentProgress,request.Status,request.GoogleEventId);
        _appointmentRepository.Add(appointment);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AppointmentCreatedEvent(
               appointment.Id, appointment.ProfessorId, appointment.StudentId, appointment.CalendarId, appointment.DateTime, appointment.ProfessorProgress, appointment.StudentProgress));
        }
    }
}
