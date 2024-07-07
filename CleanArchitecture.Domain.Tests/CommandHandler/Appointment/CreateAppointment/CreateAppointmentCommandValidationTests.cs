using System;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Errors;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.CreateAppointment
{
    public sealed class CreateAppointmentCommandValidationTests :
        ValidationTestBase<CreateAppointmentCommand, CreateAppointmentCommandValidation>
    {
        public CreateAppointmentCommandValidationTests() : base(new CreateAppointmentCommandValidation())
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
            var command = CreateTestCommand(id: Guid.Empty); // Pasar el id vacío

            ShouldHaveSingleError(
                command,
                DomainErrorCodes.Appointment.EmptyId,
                "Appointment id may not be empty");
        }

        private static CreateAppointmentCommand CreateTestCommand(
            Guid? id = null,
            Guid? professorId = null,
            Guid? studentId = null,
            Guid? calendarId = null,
            DateTime? startDate = null,
            string professorProgress = "Test Professor Progress",
            string studentProgress = "Test Student Progress",
            string status="new",
            string googleEventId="eventid")
        {
            return new CreateAppointmentCommand(
                id ?? Guid.NewGuid(),
                professorId ?? Guid.NewGuid(),
                studentId ?? Guid.NewGuid(),
                calendarId ?? Guid.NewGuid(),
                startDate ?? DateTime.UtcNow,
                professorProgress,
                studentProgress,status,googleEventId);
        }
    }
}
