using System;

namespace CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;

public sealed class UpdateProfessorCommand : CommandBase
{
    private static readonly UpdateProfessorCommandValidation s_validation = new();

    public bool IsCoordinator { get; }
    public Guid UserId { get; } 
    public Guid ResearchGroupId { get; }
    public UpdateProfessorCommand(Guid ProfessorId,Guid userId,Guid researchGroupID,bool isCoordinator) : base(ProfessorId)
    {
        UserId = userId;
        ResearchGroupId = researchGroupID;
        IsCoordinator = isCoordinator;
       
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}