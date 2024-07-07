using System;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.DeleteAppointment;

public sealed class DeleteAppointmentCommandValidationTests :
    ValidationTestBase<DeleteAppointmentCommand, DeleteAppointmentCommandValidation>
{
    public DeleteAppointmentCommandValidationTests() : base(new DeleteAppointmentCommandValidation())
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

    private static DeleteAppointmentCommand CreateTestCommand(Guid? AppointmentId = null)
    {
        return new DeleteAppointmentCommand(AppointmentId ?? Guid.NewGuid());
    }
}