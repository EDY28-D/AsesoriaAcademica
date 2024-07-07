using System;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommand : CommandBase
{
    private static readonly DeleteResearchGroupCommandValidation s_validation = new();

    public DeleteResearchGroupCommand(Guid ResearchGroupId) : base(ResearchGroupId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}