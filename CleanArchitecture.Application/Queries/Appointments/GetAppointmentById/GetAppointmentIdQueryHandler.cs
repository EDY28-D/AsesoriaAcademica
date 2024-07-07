using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;

public sealed class GetAppointmentByIdQueryHandler :
    IRequestHandler<GetAppointmentByIdQuery, AppointmentViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IAppointmentRepository _AppointmentRepository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository AppointmentRepository, IMediatorHandler bus)
    {
        _AppointmentRepository = AppointmentRepository;
        _bus = bus;
    }

    public async Task<AppointmentViewModel?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var Appointment = await _AppointmentRepository.GetByIdAsync(request.AppointmentId);

        if (Appointment is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetAppointmentByIdQuery),
                    $"Appointment with id {request.AppointmentId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return AppointmentViewModel.FromAppointment(Appointment);
    }
}