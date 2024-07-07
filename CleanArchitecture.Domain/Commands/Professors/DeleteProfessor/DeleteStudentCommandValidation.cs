using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;

public sealed class DeleteProfessorCommandValidation : AbstractValidator<DeleteProfessorCommand>
{
    public DeleteProfessorCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Professor.EmptyId)
            .WithMessage("Professor id may not be empty");
    }
}