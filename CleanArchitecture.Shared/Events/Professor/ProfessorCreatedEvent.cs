using System;

namespace CleanArchitecture.Shared.Events.Professor;

public sealed class ProfessorCreatedEvent : DomainEvent
{
    public bool IsCoordinator {  get; set; }
    public Guid UserId { get; set; }
    public Guid ResearchGroupId { get; set; }

    public ProfessorCreatedEvent(Guid ProfessorId,Guid userId,Guid researchGroupId,bool isCoordinator) : base(ProfessorId)
    {
        IsCoordinator = isCoordinator;
        UserId=userId;
        ResearchGroupId= researchGroupId;
    }
}