using System;

namespace CleanArchitecture.Shared.Events.Student;

public sealed class StudentUpdatedEvent : DomainEvent
{
    public Guid UserId { get; set; }

    public StudentUpdatedEvent(Guid StudentId, Guid userId) : base(StudentId)
    {
        UserId=userId;
    }
}