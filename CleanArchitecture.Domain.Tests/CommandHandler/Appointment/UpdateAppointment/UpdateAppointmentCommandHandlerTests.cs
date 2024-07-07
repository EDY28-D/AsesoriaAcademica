using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Appointment;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandHandlerTests
    {
        private readonly UpdateAppointmentCommandTestFixture _fixture = new();

        //[Fact]
        //public async Task Should_Update_Appointment()
        //{
        //    var command = new UpdateAppointmentCommand(
        //        Guid.NewGuid(),
        //        Guid.NewGuid(),
        //        Guid.NewGuid(),
        //        Guid.NewGuid(),
        //        DateTime.UtcNow,
        //        "Estado de la Appointment",
        //        "Asunto de la Appointment");
        //    _fixture.SetupUserAsUser();
            
           

        //    await _fixture.CommandHandler.Handle(command, default);

        //    _fixture
        //        .VerifyNoDomainNotification()
        //        .VerifyCommit()
        //        .VerifyRaisedEvent<AppointmentUpdatedEvent>(x =>
        //            x.AggregateId == command.AggregateId &&
        //            x.ProfessorId == command.ProfessorId &&
        //            x.StudentId == command.StudentId &&
        //            x.CalendarId == command.CalendarId &&
        //            x.DateTime == command.DateTime &&
        //            x.ProfessorProgress == command.ProfessorProgress &&
        //            x.StudentProgress == command.StudentProgress);
        //}

    //    [Fact]
    //    public async Task Should_Not_Update_Appointment_Insufficient_Permissions()
    //    {
    //        var command = new UpdateAppointmentCommand(
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            DateTime.UtcNow,
    //            "Estado de la Appointment",
    //            "Asunto de la Appointment");

    //        _fixture.SetupUserAsUser();

    //        await _fixture.CommandHandler.Handle(command, default);

    //        _fixture
    //            .VerifyNoCommit()
    //            .VerifyNoRaisedEvent<AppointmentUpdatedEvent>()
    //            .VerifyAnyDomainNotification()
    //            .VerifyExistingNotification(
    //                DomainErrorCodes.Appointment.InsufficientPermissions,
    //                $"No permission to update Appointment {command.AggregateId}");
    //    }

    //    [Fact]
    //    public async Task Should_Not_Update_Appointment_Not_Existing()
    //    {
    //        var command = new UpdateAppointmentCommand(
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            Guid.NewGuid(),
    //            DateTime.UtcNow,
    //            "Estado de la Appointment",
    //            "Asunto de la Appointment");

    //        await _fixture.CommandHandler.Handle(command, default);

    //        _fixture
    //            .VerifyNoCommit()
    //            .VerifyNoRaisedEvent<AppointmentUpdatedEvent>()
    //            .VerifyAnyDomainNotification()
    //            .VerifyExistingNotification(
    //                DomainErrorCodes.Appointment.NotFound,
    //                $"There is no Appointment with Id {command.AggregateId}");
    //    }
   }
}
