using System;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.CreateAppointment
{
    public sealed class CreateAppointmentCommandTestFixture : CommandHandlerFixtureBase
    {
        public CreateAppointmentCommandHandler CommandHandler { get; }

        private IAppointmentRepository AppointmentRepository { get; }
        private IUserRepository UserRepository { get; }
        

        public CreateAppointmentCommandTestFixture()
        {
            AppointmentRepository = Substitute.For<IAppointmentRepository>();
            UserRepository = Substitute.For<IUserRepository>();
            

            CommandHandler = new CreateAppointmentCommandHandler(
                Bus,
                UnitOfWork,
                NotificationHandler,
                AppointmentRepository,
                UserRepository,
                User);
        }

        public void SetupUser()
        {
            User.GetUserRole().Returns(UserRole.User);
        }

        public void SetupExistingAppointment(Guid id)
        {
            AppointmentRepository
                .ExistsAsync(Arg.Is<Guid>(x => x == id))
                .Returns(true);
        }
    }
}
