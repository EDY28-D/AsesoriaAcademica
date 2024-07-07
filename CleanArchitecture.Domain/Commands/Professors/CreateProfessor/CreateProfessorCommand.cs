using System;

namespace CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
//feature Proffesor coordinator
public sealed class CreateProfessorCommand : CommandBase
{
    private static readonly CreateProfessorCommandValidation s_validation = new();

    public Guid UserId { get; set; }
    public Guid ProfessorId { get; set; }
    public Guid ResearchGroupId{ get; set; }
    public bool IsCoordinator { get; set; }
    public CreateProfessorCommand(Guid professorId, Guid userId,Guid researchgroupId ,bool isCoordinator) : base(professorId)
    {
        UserId = userId;
        ProfessorId = professorId;
        ResearchGroupId = researchgroupId;
        IsCoordinator = isCoordinator;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}