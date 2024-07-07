using System;

namespace CleanArchitecture.Shared.Events.AdvisoryContract;

public sealed class AdvisoryContractUpdatedEvent : DomainEvent
{

    public Guid ProfessorId { get; private set; }

    public Guid StudentId { get; private set; }

    public Guid ResearchLineId { get; private set; }

    public AdvisoryContractUpdatedEvent(Guid AdvisoryContractId, Guid professorId, Guid studentId, Guid researchLineId) : base(AdvisoryContractId)
    {
        ProfessorId = professorId;
        StudentId = studentId;
        ResearchLineId = researchLineId;
    }
}