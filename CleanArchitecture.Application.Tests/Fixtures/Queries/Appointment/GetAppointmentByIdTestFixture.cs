using System;
using CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;
using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Application.Queries.Tenants.GetTenantById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Appointments;

public sealed class GetAppointmentByIdTestFixture : QueryHandlerBaseFixture
{
    public GetAppointmentByIdQueryHandler QueryHandler { get; }
    private IAppointmentRepository AppointmentRepository { get; }

    public GetAppointmentByIdTestFixture()
    {
        AppointmentRepository = Substitute.For<IAppointmentRepository>();

        QueryHandler = new GetAppointmentByIdQueryHandler(
            AppointmentRepository,
            Bus);
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
        else
        {
            AppointmentRepository.GetByIdAsync(Arg.Is<Guid>(y => y == Appointment.Id)).Returns(Appointment);
        }


        return Appointment;
    }
}