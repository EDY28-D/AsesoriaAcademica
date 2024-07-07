using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using MediatR;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateAdvisoryContractCommand>
{
    private readonly IAdvisoryContractRepository _AdvisoryContractRepository;
   // private readonly IUserRepository _UserRepository;

    private readonly IUser _user;
    private readonly IProfessorRepository _professorRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IResearchLineRepository _researchLineRepository;

    public CreateAdvisoryContractCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAdvisoryContractRepository AdvisoryContractRepository,IProfessorRepository ProfessorRepository,IStudentRepository studentRepository,
        IResearchLineRepository researchLineRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AdvisoryContractRepository = AdvisoryContractRepository;
        _professorRepository = ProfessorRepository;
        _studentRepository = studentRepository;
        _researchLineRepository = researchLineRepository;
        _user = user;
        
    }

    public async Task Handle(CreateAdvisoryContractCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create AdvisoryContract {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _AdvisoryContractRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a AdvisoryContract with Id {request.AggregateId}",
                    DomainErrorCodes.AdvisoryContract.AlreadyExists));

            return;
        }

        var AdvisoryContract = new AdvisoryContract(
            request.AdvisoryContractId,request.ProfessorId,request.StudentId,request.ResearchLineId,request.ThesisTopic,request.Message,request.Status);
        _AdvisoryContractRepository.Add(AdvisoryContract);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AdvisoryContractCreatedEvent(request.AdvisoryContractId, request.ProfessorId, request.StudentId, request.ResearchLineId));
        }
    }
}