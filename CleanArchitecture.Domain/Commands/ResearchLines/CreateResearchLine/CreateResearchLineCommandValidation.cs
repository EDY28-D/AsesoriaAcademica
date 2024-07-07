using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;

public sealed class CreateResearchLineCommandValidation : AbstractValidator<CreateResearchLineCommand>
{
    public CreateResearchLineCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchLine.EmptyId)
            .WithMessage("ResearchLine id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchLine.EmptyName)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.ResearchLine.Name)
            .WithErrorCode(DomainErrorCodes.ResearchLine.NameExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.ResearchLine.Name} characters");
    }
}