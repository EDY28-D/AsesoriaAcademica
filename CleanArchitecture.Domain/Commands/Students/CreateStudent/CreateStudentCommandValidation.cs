using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Students.CreateStudent;

public sealed class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidation()
    {
        AddRuleForId();
        AddRuleForCode();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Student.EmptyId)
            .WithMessage("Student id may not be empty");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Student.EmptyCode)
            .WithMessage("Code may not be empty")
            .WithErrorCode(DomainErrorCodes.Student.CodeExceedsMaxLength)
            .WithMessage($"Code may not be longer than {MaxLengths.Student.Code} characters");
    }
}