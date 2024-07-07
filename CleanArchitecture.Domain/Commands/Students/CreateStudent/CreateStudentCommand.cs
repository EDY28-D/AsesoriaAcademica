using System;

namespace CleanArchitecture.Domain.Commands.Students.CreateStudent;

public sealed class CreateStudentCommand : CommandBase
{
    private static readonly CreateStudentCommandValidation s_validation = new();

    public string Code { get; set; }
    public Guid UserId { get; set; }
    public Guid StudentId { get; set; } 
    public CreateStudentCommand(Guid studentId, Guid userId,string code) : base(studentId)
    {
        UserId = userId;
        Code = code;
        StudentId = studentId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}