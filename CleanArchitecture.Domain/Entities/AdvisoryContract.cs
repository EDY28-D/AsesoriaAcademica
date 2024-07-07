using System;
//Modelo advisory contract
namespace CleanArchitecture.Domain.Entities
{
    public class AdvisoryContract : Entity
    {
        public Guid ProfessorId { get; private set; }
        public virtual Professor Professor { get; private set; } = null!;
        public Guid StudentId { get; private set; }
        public virtual Student Student { get; private set; } = null!;
        public Guid ResearchLineId { get; private set; }
        public virtual ResearchLine ResearchLine { get; private set; } = null!;
        public string ThesisTopic { get; private set; }
        public string Message { get; private set; }
        public string Status { get; private set; }

        public AdvisoryContract(Guid id, Guid professorId, Guid studentId, Guid researchLineId, string thesisTopic, string message, string status) : base(id)
        {
            ProfessorId = professorId;
            StudentId = studentId;
            ResearchLineId = researchLineId;
            ThesisTopic = thesisTopic;
            Message = message;
            Status = status;
        }

        public void SetThesisTopic(string thesisTopic)
        {
            ThesisTopic = thesisTopic;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetStatus(string status)
        {
            Status = status;
        }
    }
}
