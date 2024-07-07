using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;

public sealed class GetResearchLineByIdQueryHandler :
    IRequestHandler<GetResearchLineByIdQuery, ResearchLineViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IResearchLineRepository _ResearchLineRepository;

    public GetResearchLineByIdQueryHandler(IResearchLineRepository ResearchLineRepository, IMediatorHandler bus)
    {
        _ResearchLineRepository = ResearchLineRepository;
        _bus = bus;
    }

    public async Task<ResearchLineViewModel?> Handle(GetResearchLineByIdQuery request, CancellationToken cancellationToken)
    {
        var ResearchLine = await _ResearchLineRepository.GetByIdAsync(request.ResearchLineId);

        if (ResearchLine is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetResearchLineByIdQuery),
                    $"ResearchLine with id {request.ResearchLineId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ResearchLineViewModel.FromResearchLine(ResearchLine);
    }
}