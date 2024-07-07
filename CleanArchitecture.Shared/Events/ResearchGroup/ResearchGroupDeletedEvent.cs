using System;

namespace CleanArchitecture.Shared.Events.ResearchGroup;

public sealed class ResearchGroupDeletedEvent : DomainEvent
{
    public ResearchGroupDeletedEvent(Guid ResearchGroupId) : base(ResearchGroupId)
    {
    }
}