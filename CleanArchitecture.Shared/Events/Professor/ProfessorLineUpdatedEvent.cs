using System;

namespace CleanArchitecture.Shared.Events.Professor;

public sealed class ProfessorUpdatedEvent : DomainEvent
{
    public Guid UserId { get; set; }

    public ProfessorUpdatedEvent(Guid ProfessorId, Guid userId) : base(ProfessorId)
    {
        UserId=userId;
    }
}