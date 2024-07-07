using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Student;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Students.DeleteStudent;

public sealed class DeleteStudentCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _StudentRepository;
   // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteStudentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IStudentRepository StudentRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _StudentRepository = StudentRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
/*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete Student {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

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
/*
        var StudentUsers = _userRepository
            .GetAll()
            .Where(x => x.StudentId == request.AggregateId);

        _userRepository.RemoveRange(StudentUsers);*/

        _StudentRepository.Remove(Student);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new StudentDeletedEvent(Student.Id,Student.UserId));
        }
    }
}