using System;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommand : CommandBase
{
    private static readonly UpdateAdvisoryContractCommandValidation s_validation = new();

    public Guid AdvisoryContractId { get; set; }
    public Guid ProfessorId { get; private set; }

    public Guid StudentId { get; private set; }

    public Guid ResearchLineId { get; private set; }
    public string ThesisTopic { get; private set; }
    public string Message { get; private set; }
    public string Status { get; private set; }

    public UpdateAdvisoryContractCommand(Guid id, Guid professorId, Guid studentId, Guid researchLineId, string thesisTopic, string message, string status) : base(id)
    {
        AdvisoryContractId = id;
        ProfessorId = professorId;
        StudentId = studentId;
        ResearchLineId = researchLineId;
        ThesisTopic = thesisTopic;
        Message = message;
        Status = status;
    }


    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}