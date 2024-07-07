using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Student;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Students.UpdateStudent;

public sealed class UpdateStudentCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _StudentRepository;
    private readonly IUser _user;

    public UpdateStudentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IStudentRepository StudentRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _StudentRepository = StudentRepository;
        _user = user;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update Student {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var Student = await _StudentRepository.GetByIdAsync(request.AggregateId);

        if (Student is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Student with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        Student.SetCode(request.Code);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new StudentUpdatedEvent(
                Student.Id,Student.UserId));
        }
    }
}