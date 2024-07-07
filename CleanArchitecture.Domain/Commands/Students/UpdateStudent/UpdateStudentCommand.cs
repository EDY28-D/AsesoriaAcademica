using System;

namespace CleanArchitecture.Domain.Commands.Students.UpdateStudent;

public sealed class UpdateStudentCommand : CommandBase
{
    private static readonly UpdateStudentCommandValidation s_validation = new();

    public string Code { get; }
    public Guid UserId { get; } 
    public UpdateStudentCommand(Guid StudentId,Guid userId,string code) : base(StudentId)
    {
        Code = code;
        UserId = userId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}