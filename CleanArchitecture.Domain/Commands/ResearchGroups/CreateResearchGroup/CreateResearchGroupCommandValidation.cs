using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;

public sealed class CreateResearchGroupCommandValidation : AbstractValidator<CreateResearchGroupCommand>
{
    public CreateResearchGroupCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyId)
            .WithMessage("ResearchGroup id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyName)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.ResearchGroup.Name)
            .WithErrorCode(DomainErrorCodes.ResearchGroup.NameExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.ResearchGroup.Name} characters");
    }
}