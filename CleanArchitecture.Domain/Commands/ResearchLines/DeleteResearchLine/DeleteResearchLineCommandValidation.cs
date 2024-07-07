using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;

public sealed class DeleteResearchLineCommandValidation : AbstractValidator<DeleteResearchLineCommand>
{
    public DeleteResearchLineCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchLine.EmptyId)
            .WithMessage("ResearchLine id may not be empty");
    }
}