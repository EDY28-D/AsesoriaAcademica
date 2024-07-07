using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.Professors.GetProfessorById;

public sealed class GetProfessorByIdQueryHandler :
    IRequestHandler<GetProfessorByIdQuery, ProfessorViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IProfessorRepository _ProfessorRepository;

    public GetProfessorByIdQueryHandler(IProfessorRepository ProfessorRepository, IMediatorHandler bus)
    {
        _ProfessorRepository = ProfessorRepository;
        _bus = bus;
    }

    public async Task<ProfessorViewModel?> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
    {
        var Professor = await _ProfessorRepository.GetByIdAsync(request.ProfessorId);

        if (Professor is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetProfessorByIdQuery),
                    $"Professor with id {request.ProfessorId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ProfessorViewModel.FromProfessor(Professor);
    }
}