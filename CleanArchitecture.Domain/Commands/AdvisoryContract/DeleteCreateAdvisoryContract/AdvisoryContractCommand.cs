using System;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommand : CommandBase
{
    private static readonly DeleteAdvisoryContractCommandValidation s_validation = new();

    public DeleteAdvisoryContractCommand(Guid AdvisoryContractId) : base(AdvisoryContractId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}