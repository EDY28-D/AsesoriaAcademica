using System;

namespace CleanArchitecture.Shared.Events.Student;

public sealed class StudentDeletedEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public StudentDeletedEvent(Guid StudentId,Guid userId) : base(StudentId)
    {
        UserId = userId;
    }
}