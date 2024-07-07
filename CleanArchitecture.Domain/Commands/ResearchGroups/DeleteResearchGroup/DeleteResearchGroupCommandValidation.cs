using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommandValidation : AbstractValidator<DeleteResearchGroupCommand>
{
    public DeleteResearchGroupCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyId)
            .WithMessage("ResearchGroup id may not be empty");
    }
}