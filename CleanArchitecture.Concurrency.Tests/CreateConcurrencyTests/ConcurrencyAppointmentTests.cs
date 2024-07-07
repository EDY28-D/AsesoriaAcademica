using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Shared.Events.Appointment;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Tests
{
    public class ConcurrencyAppointmentTests
    {
        private Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUser> _userMock;
        private Mock<IMediatorHandler> _mediatorHandlerMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private DomainNotificationHandler _notificationHandler;
        private CreateAppointmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userMock = new Mock<IUser>();
            _mediatorHandlerMock = new Mock<IMediatorHandler>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _notificationHandler = new DomainNotificationHandler();

            _handler = new CreateAppointmentCommandHandler(
                _mediatorHandlerMock.Object,
                _unitOfWorkMock.Object,
                _notificationHandler,
                _appointmentRepositoryMock.Object,
                _userRepositoryMock.Object,
                _userMock.Object);

            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(true);
        }

        [Test]
        public async Task TestCreateAppointmentConcurrently()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                var command = new CreateAppointmentCommand(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    $"Professor Progress {i}",
                    $"Student Progress {i}","new","eventId");

                tasks.Add(Task.Run(() => _handler.Handle(command, CancellationToken.None)));
            }

            await Task.WhenAll(tasks);

            _appointmentRepositoryMock.Verify(r => r.Add(It.IsAny<CleanArchitecture.Domain.Entities.Appointment>()), Times.Exactly(5));
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Exactly(5));
            _mediatorHandlerMock.Verify(m => m.RaiseEventAsync(It.IsAny<AppointmentCreatedEvent>()), Times.Exactly(5));
        }
    }
}
