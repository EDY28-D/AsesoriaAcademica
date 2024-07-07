using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineUpdatedEvent : DomainEvent
{
    public Guid ResearchGroupId { get; set; }

    public ResearchLineUpdatedEvent(Guid ResearchLineId, Guid researchGroupId) : base(ResearchLineId)
    {
        ResearchGroupId = researchGroupId;
    }
}