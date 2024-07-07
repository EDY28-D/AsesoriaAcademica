using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandValidation : AbstractValidator<UpdateAdvisoryContractCommand>
{
    public UpdateAdvisoryContractCommandValidation()
    {
        AddRuleForId();
        AddRuleForMessage();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyId)
            .WithMessage("AdvisoryContract id may not be empty");
    }

    private void AddRuleForMessage()
    {
        RuleFor(cmd => cmd.Message)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyMessage)
            .WithMessage("Message may not be empty")
            .MaximumLength(MaxLengths.AdvisoryContract.Status)
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.ThesisTopicExceedsMaxLength)
            .WithMessage($"Message may not be longer than {MaxLengths.AdvisoryContract.Status} characters");
    }
}