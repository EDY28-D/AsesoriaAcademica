using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Students.UpdateStudent;

public sealed class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Student.EmptyId)
            .WithMessage("Student id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Student.EmptyCode)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.Student.Code)
            .WithErrorCode(DomainErrorCodes.Student.CodeExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.Student.Code} characters");
    }
}