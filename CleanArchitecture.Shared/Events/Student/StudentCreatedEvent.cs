using System;

namespace CleanArchitecture.Shared.Events.Student;

public sealed class StudentCreatedEvent : DomainEvent
{
    public string Code { get; set; }
    public Guid UserId { get; set; }

    public StudentCreatedEvent(Guid StudentId,Guid userId,String code) : base(StudentId)
    {
        Code = code;
        UserId=userId;
    }
}