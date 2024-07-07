using System;

namespace CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;

public sealed class DeleteResearchLineCommand : CommandBase
{
    private static readonly DeleteResearchLineCommandValidation s_validation = new();

    public DeleteResearchLineCommand(Guid ResearchLineId) : base(ResearchLineId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}