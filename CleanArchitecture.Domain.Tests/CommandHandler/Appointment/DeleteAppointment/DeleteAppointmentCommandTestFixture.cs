using System;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.DeleteAppointment
{
    public sealed class DeleteAppointmentCommandTestFixture : CommandHandlerFixtureBase
    {
        public DeleteAppointmentCommandHandler CommandHandler { get; }

        private IAppointmentRepository AppointmentRepository { get; }
        private IUserRepository UserRepository { get; }

        public DeleteAppointmentCommandTestFixture()
        {
            AppointmentRepository = Substitute.For<IAppointmentRepository>();
            UserRepository = Substitute.For<IUserRepository>();

            CommandHandler = new DeleteAppointmentCommandHandler(
                Bus,
                UnitOfWork,
                NotificationHandler,
                AppointmentRepository,
                UserRepository,
                User);
        }

        public Entities.Appointment SetupAppointment()
        {
            var appointment = new Entities.Appointment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                "Test Professor Progress",
                "Test Student Progress","test","test");

            AppointmentRepository
                .GetByIdAsync(Arg.Is<Guid>(y => y == appointment.Id))
                .Returns(appointment);

            return appointment;
        }

        public void SetupUser()
        {
            User.GetUserRole().Returns(UserRole.User);
        }
    }
}
