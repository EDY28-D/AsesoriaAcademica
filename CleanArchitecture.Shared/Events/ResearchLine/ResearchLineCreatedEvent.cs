using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineCreatedEvent : DomainEvent
{
    public Guid ResearchGroupId { get; set; }

    public ResearchLineCreatedEvent(Guid ResearchLineId, Guid researchGroupId) : base(ResearchLineId)
    {
        ResearchGroupId = researchGroupId;
    }
}