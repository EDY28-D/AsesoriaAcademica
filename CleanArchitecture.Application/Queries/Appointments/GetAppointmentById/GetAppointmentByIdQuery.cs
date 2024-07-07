using System;
using CleanArchitecture.Application.ViewModels.Appointments;
using MediatR;

namespace CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;

public sealed record GetAppointmentByIdQuery(Guid AppointmentId) : IRequest<AppointmentViewModel?>;