using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Appointment;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.CreateAppointment;

public sealed class CreateAppointmentCommandHandlerTests
{
    private readonly CreateAppointmentCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Appointment()
    {
        var command = new CreateAppointmentCommand(
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            Guid.NewGuid(),
            DateTime.UtcNow, // Fecha de la cita (puedes usar DateTime.Now si lo prefieres)
            "Estado de la Appointment", // Estado de la cita
            "Asunto de la Appointment","new","EventId"); // Asunto de la cita

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<AppointmentCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.ProfessorId == command.ProfessorId && x.StudentId == command.StudentId
                && x.CalendarId  == command.CalendarId && x.DateTime == command.DateTime
                && x.ProfessorProgress == command.ProfessorProgress && x.StudentProgress == command.StudentProgress);
    }

    //[Fact]
    //public async Task Should_Not_Create_Appointment_Insufficient_Permissions()
    //{
    //    _fixture.SetupUser();

    //    var command = new CreateAppointmentCommand(
    //        Guid.NewGuid(),
    //        professorId: Guid.NewGuid(),
    //        studentId: Guid.NewGuid(),
    //        calendarId: Guid.NewGuid(),
    //        DateTime.UtcNow,
    //        "Test Appointment", "testcode","test","testeventId");

    //    await _fixture.CommandHandler.Handle(command, default);

    //    _fixture
    //        .VerifyNoCommit()
    //        .VerifyNoRaisedEvent<AppointmentCreatedEvent>()
    //        .VerifyAnyDomainNotification()
    //        .VerifyExistingNotification(
    //            ErrorCodes.InsufficientPermissions,
    //            $"No permission to create Appointment {command.AggregateId}");
    //}

    //[Fact]
    //public async Task Should_Not_Create_Appointment_Already_Exists()
    //{
    //    var command = new CreateAppointmentCommand(
    //       Ids.Seed.AppointmentId,
    //        professorId: Guid.NewGuid(),
    //        studentId: Guid.NewGuid(),
    //        calendarId: Guid.NewGuid(),
    //        DateTime.UtcNow,
    //        "Test Appointment", "testcode","test","test");

    //    _fixture.SetupExistingAppointment(command.AggregateId);

    //    await _fixture.CommandHandler.Handle(command, default);

    //    _fixture
    //        .VerifyNoCommit()
    //        .VerifyNoRaisedEvent<AppointmentCreatedEvent>()
    //        .VerifyAnyDomainNotification()
    //        .VerifyExistingNotification(
    //            DomainErrorCodes.Appointment.AlreadyExists,
    //            $"There is already a Appointment with Id {command.AggregateId}");
    //}




}