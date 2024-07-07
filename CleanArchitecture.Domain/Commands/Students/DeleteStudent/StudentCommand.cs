using System;

namespace CleanArchitecture.Domain.Commands.Students.DeleteStudent;

public sealed class DeleteStudentCommand : CommandBase
{
    private static readonly DeleteStudentCommandValidation s_validation = new();

    public DeleteStudentCommand(Guid StudentId) : base(StudentId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}