using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommandValidation : AbstractValidator<DeleteAdvisoryContractCommand>
{
    public DeleteAdvisoryContractCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyId)
            .WithMessage("AdvisoryContract id may not be empty");
    }
}