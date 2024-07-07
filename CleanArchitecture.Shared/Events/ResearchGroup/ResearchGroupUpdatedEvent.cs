using System;

namespace CleanArchitecture.Shared.Events.ResearchGroup;

public sealed class ResearchGroupUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ResearchGroupUpdatedEvent(Guid ResearchGroupId, string name) : base(ResearchGroupId)
    {
        Name = name;
    }
}