using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Appointments.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Appointments;

public sealed class GetAllAppointmentsTestFixture : QueryHandlerBaseFixture
{
    public GetAllAppointmentsQueryHandler QueryHandler { get; }
    private IAppointmentRepository AppointmentRepository { get; }

    public GetAllAppointmentsTestFixture()
    {
        AppointmentRepository = Substitute.For<IAppointmentRepository>();
        var sortingProvider = new AppointmentViewModelSortProvider();

        QueryHandler = new GetAllAppointmentsQueryHandler(AppointmentRepository, sortingProvider);
    }

    public Appointment SetupAppointment(bool deleted = false)
    {

        var AppointmentId = Guid.Empty;

        var Appointment = new Appointment(AppointmentId, // Id de la Appointment
            Guid.NewGuid(), // Id del estudiante
            Guid.NewGuid(), // Id del asesor
            Guid.NewGuid(),

            DateTime.UtcNow, // Fecha de la Appointment (puedes usar DateTime.Now si lo prefieres)
            "Estado de la Appointment", // Estado de la Appointment
            "Asunto de la Appointment","test","test"); // Asunto de la Appointment

        if (deleted)
        {
            Appointment.Delete();
        }


        var AppointmentList = new List<Appointment> { Appointment }.BuildMock();
        AppointmentRepository.GetAllNoTracking().Returns(AppointmentList);

        return Appointment;
    }
}