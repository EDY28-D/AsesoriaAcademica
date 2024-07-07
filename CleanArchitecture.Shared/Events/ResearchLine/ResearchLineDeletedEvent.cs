using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineDeletedEvent : DomainEvent
{
    public Guid ResearchGroupId { get; set; }

    public ResearchLineDeletedEvent(Guid ResearchLineId, Guid researchGroupId) : base(ResearchLineId)
    {
        ResearchGroupId = researchGroupId;
    }
}