using System;

namespace CleanArchitecture.Shared.Events.Professor;

public sealed class ProfessorDeletedEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public ProfessorDeletedEvent(Guid ProfessorId,Guid userId) : base(ProfessorId)
    {
        UserId = userId;
    }
}