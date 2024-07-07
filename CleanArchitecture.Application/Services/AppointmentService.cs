using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Appointments.GetAll;
using CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Entities;
using System.Net.Mail;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using static CleanArchitecture.Domain.Errors.DomainErrorCodes;
using Appointment = CleanArchitecture.Domain.Entities.Appointment;

namespace CleanArchitecture.Application.Services;

public sealed class AppointmentService : IAppointmentService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;
    private readonly INotificationService _notificationService;
    private readonly string _recipientEmail = "junior.matias@unas.edu.pe";

    public AppointmentService(IMediatorHandler bus, IDistributedCache distributedCache, INotificationService notificationService)
    {
        _bus = bus;
        _distributedCache = distributedCache;
        _notificationService = notificationService;
    }

    public async Task<Guid> CreateAppointmentAsync(CreateAppointmentViewModel appointment)
    {
        var appointmentId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateAppointmentCommand(
            appointmentId, appointment.professorId, appointment.studentId, appointment.calendarId, appointment.dateTime, appointment.professorProgress, appointment.studentProgress ,appointment.status, appointment.googleEventId));

        await _notificationService.SendAppointmentCreatedNotificationAsync(appointmentId, appointment.dateTime, _recipientEmail);

        return appointmentId;
    }

    public async Task UpdateAppointmentAsync(UpdateAppointmentViewModel appointment)
    {
        await _bus.SendCommandAsync(new UpdateAppointmentCommand(
            appointment.appointmentId, appointment.professorId, appointment.studentId, appointment.calendarId, appointment.dateTime, appointment.professorProgress, appointment.studentProgress));

        await _notificationService.SendAppointmentUpdatedNotificationAsync(appointment.appointmentId, appointment.dateTime, _recipientEmail);
    }

    public async Task DeleteAppointmentAsync(Guid appointmentId)
    {
        await _bus.SendCommandAsync(new DeleteAppointmentCommand(appointmentId));
        await _notificationService.SendAppointmentDeletedNotificationAsync(appointmentId, _recipientEmail);
    }

    public async Task<AppointmentViewModel?> GetAppointmentByIdAsync(Guid AppointmentId)
    {
        var cachedAppointment = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Appointment>(AppointmentId),
            async () => await _bus.QueryAsync(new GetAppointmentByIdQuery(AppointmentId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedAppointment;
    }

    public async Task<PagedResult<AppointmentViewModel>> GetAllAppointmentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new AppointmentsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}
