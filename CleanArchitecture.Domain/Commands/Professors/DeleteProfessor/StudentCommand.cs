using System;

namespace CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;

public sealed class DeleteProfessorCommand : CommandBase
{
    private static readonly DeleteProfessorCommandValidation s_validation = new();

    public DeleteProfessorCommand(Guid ProfessorId) : base(ProfessorId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}