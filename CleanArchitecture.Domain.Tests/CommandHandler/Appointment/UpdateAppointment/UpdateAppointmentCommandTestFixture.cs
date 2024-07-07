using System;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandTestFixture : CommandHandlerFixtureBase
    {
        public UpdateAppointmentCommandHandler CommandHandler { get; }

        private IAppointmentRepository AppointmentRepository { get; }
        private IUserRepository UserRepository { get; }

        public UpdateAppointmentCommandTestFixture()
        {
            AppointmentRepository = Substitute.For<IAppointmentRepository>();
            UserRepository = Substitute.For<IUserRepository>();

            CommandHandler = new UpdateAppointmentCommandHandler(
                
                Bus,
                UnitOfWork,
                NotificationHandler,
                AppointmentRepository,
                
                User);
        }

        public void SetupExistingAppointment(Guid id)
        {
            var appointment = new Entities.Appointment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                "Existing Professor Progress",
                "Existing Student Progress","test","test");

            AppointmentRepository
                .GetByIdAsync(Arg.Is<Guid>(x => x == id))
                .Returns(appointment);
        }

        public void SetupUserAsAdmin()
        {
            User.GetUserRole().Returns(UserRole.Admin);
        }

        public void SetupUserAsUser()
        {
            User.GetUserRole().Returns(UserRole.User);
        }
    }
}
