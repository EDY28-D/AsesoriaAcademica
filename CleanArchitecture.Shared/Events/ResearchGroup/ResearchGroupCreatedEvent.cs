using System;

namespace CleanArchitecture.Shared.Events.ResearchGroup;

public sealed class ResearchGroupCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ResearchGroupCreatedEvent(Guid ResearchGroupId, string name) : base(ResearchGroupId)
    {
        Name = name;
    }
}