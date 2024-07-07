using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;

public sealed class GetResearchGroupByIdQueryHandler :
    IRequestHandler<GetResearchGroupByIdQuery, ResearchGroupViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IResearchGroupRepository _ResearchGroupRepository;

    public GetResearchGroupByIdQueryHandler(IResearchGroupRepository ResearchGroupRepository, IMediatorHandler bus)
    {
        _ResearchGroupRepository = ResearchGroupRepository;
        _bus = bus;
    }

    public async Task<ResearchGroupViewModel?> Handle(GetResearchGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var ResearchGroup = await _ResearchGroupRepository.GetByIdAsync(request.ResearchGroupId);

        if (ResearchGroup is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetResearchGroupByIdQuery),
                    $"ResearchGroup with id {request.ResearchGroupId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ResearchGroupViewModel.FromResearchGroup(ResearchGroup);
    }
}