 using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Appointment;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.DeleteAppointment;

public sealed class DeleteAppointmentCommandHandlerTests
{
    private readonly DeleteAppointmentCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_Appointment()
    {
        var Appointment = _fixture.SetupAppointment();

        var command = new DeleteAppointmentCommand(Appointment.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<AppointmentDeletedEvent>(x => x.AggregateId == Appointment.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_Appointment()
    {
        _fixture.SetupAppointment();

        var command = new DeleteAppointmentCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AppointmentDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no Appointment with Id {command.AggregateId}");
    }

   
}