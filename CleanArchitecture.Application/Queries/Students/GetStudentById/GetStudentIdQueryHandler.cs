using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Students.GetStudentById;

public sealed class GetStudentByIdQueryHandler :
    IRequestHandler<GetStudentByIdQuery, StudentViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IStudentRepository _StudentRepository;

    public GetStudentByIdQueryHandler(IStudentRepository StudentRepository, IMediatorHandler bus)
    {
        _StudentRepository = StudentRepository;
        _bus = bus;
    }

    public async Task<StudentViewModel?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var Student = await _StudentRepository.GetByIdAsync(request.StudentId);

        if (Student is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetStudentByIdQuery),
                    $"Student with id {request.StudentId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return StudentViewModel.FromStudent(Student);
    }
}