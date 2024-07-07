using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;

public sealed class GetAdvisoryContractByIdQueryHandler :
    IRequestHandler<GetAdvisoryContractByIdQuery, AdvisoryContractViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;

    public GetAdvisoryContractByIdQueryHandler(IAdvisoryContractRepository AdvisoryContractRepository, IMediatorHandler bus)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
        _bus = bus;
    }

    public async Task<AdvisoryContractViewModel?> Handle(GetAdvisoryContractByIdQuery request, CancellationToken cancellationToken)
    {
        var AdvisoryContract = await _AdvisoryContractRepository.GetByIdAsync(request.AdvisoryContractId);

        if (AdvisoryContract is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetAdvisoryContractByIdQuery),
                    $"AdvisoryContract with id {request.AdvisoryContractId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return AdvisoryContractViewModel.FromAdvisoryContract(AdvisoryContract);
    }
}