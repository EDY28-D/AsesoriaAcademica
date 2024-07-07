using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Professor;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Professors.CreateProfessor;

public sealed class CreateProfessorCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateProfessorCommand>
{
    private readonly IProfessorRepository _ProfessorRepository;
    private readonly IUserRepository _UserRepository;
    private readonly IUser _user;

    public CreateProfessorCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProfessorRepository ProfessorRepository,
        IUserRepository UserRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProfessorRepository = ProfessorRepository;
        _user = user;
        _UserRepository = UserRepository;
    }

    public async Task Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create Professor {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        if (await _ProfessorRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Professor with Id {request.AggregateId}",
                    DomainErrorCodes.Professor.AlreadyExists));

            return;
        }

        var Professor = new Professor(
            request.ProfessorId,request.UserId,request.ResearchGroupId,request.IsCoordinator);
        _ProfessorRepository.Add(Professor);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProfessorCreatedEvent(
                Professor.Id,Professor.UserId,Professor.ResearchGroupId,Professor.IsCoordinator));
        }
    }
}