using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Student;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Students.CreateStudent;

public sealed class CreateStudentCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateStudentCommand>
{
    private readonly IStudentRepository _StudentRepository;
    private readonly IUserRepository _UserRepository;
    private readonly IUser _user;

    public CreateStudentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IStudentRepository StudentRepository,
        IUserRepository UserRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _StudentRepository = StudentRepository;
        _user = user;
        _UserRepository = UserRepository;
    }

    public async Task Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

       /* if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to create Student {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        if (await _StudentRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Student with Id {request.AggregateId}",
                    DomainErrorCodes.Student.AlreadyExists));

            return;
        }

        var Student = new Student(
            request.StudentId,request.UserId,
            request.Code);
        _StudentRepository.Add(Student);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new StudentCreatedEvent(
                Student.Id,Student.UserId,
                Student.Code));
        }
    }
}