using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandValidation : AbstractValidator<CreateAdvisoryContractCommand>
{
    public CreateAdvisoryContractCommandValidation()
    {
        AddRuleForId();
        AddRuleForThesisTopic();
        AddRuleForMessage();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyId)
            .WithMessage("AdvisoryContract id may not be empty");
    }

    private void AddRuleForThesisTopic()
    {
        RuleFor(cmd => cmd.ThesisTopic)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyThesisTopic)
            .WithMessage("Code may not be empty")
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.ThesisTopicExceedsMaxLength)
            .WithMessage($"Code may not be longer than {MaxLengths.AdvisoryContract.Status} characters");
    }
    private void AddRuleForMessage()
    {
        RuleFor(cmd => cmd.Message)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyMessage)
            .WithMessage("Message may not be empty");
    }
}