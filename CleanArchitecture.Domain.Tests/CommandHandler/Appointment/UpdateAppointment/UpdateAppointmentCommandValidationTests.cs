using System;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.UpdateAppointment;

public sealed class UpdateAppointmentCommandValidationTests :
    ValidationTestBase<UpdateAppointmentCommand, UpdateAppointmentCommandValidation>
{
    public UpdateAppointmentCommandValidationTests() : base(new UpdateAppointmentCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Appointment_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Appointment.EmptyId,
            "Appointment id may not be empty");
    }



    private static UpdateAppointmentCommand CreateTestCommand(
            Guid? id = null,
            Guid? professorId = null,
            Guid? studentId = null,
            Guid? calendarId = null,
            DateTime? startDate = null,
            string professorProgress = "Test Professor Progress",
            string studentProgress = "Test Student Progress")
    {
        return new UpdateAppointmentCommand(
                id ?? Guid.NewGuid(),
                professorId ?? Guid.NewGuid(),
                studentId ?? Guid.NewGuid(),
                calendarId ?? Guid.NewGuid(),
                startDate ?? DateTime.UtcNow,
                professorProgress,
                studentProgress);
    }
}