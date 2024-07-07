using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Students.DeleteStudent;

public sealed class DeleteStudentCommandValidation : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Student.EmptyId)
            .WithMessage("Student id may not be empty");
    }
}