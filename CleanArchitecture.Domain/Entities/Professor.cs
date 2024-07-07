using System;

namespace CleanArchitecture.Domain.Entities
{
    public class Professor : Entity
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public Guid ResearchGroupId { get; private set; }
        public virtual ResearchGroup ResearchGroup { get; private set; } = null!;
        public bool IsCoordinator { get; private set; }

        public Professor(Guid id, Guid userId, Guid researchGroupId, bool isCoordinator) : base(id)
        {
            UserId = userId;
            ResearchGroupId = researchGroupId;
            IsCoordinator = isCoordinator;
        }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }

        public void SetResearchGroup(Guid researchGroupId)
        {
            ResearchGroupId = researchGroupId;
        }

        public void SetCoordinator(bool isCoordinator)
        {
            IsCoordinator = isCoordinator;
        }
    }
}
