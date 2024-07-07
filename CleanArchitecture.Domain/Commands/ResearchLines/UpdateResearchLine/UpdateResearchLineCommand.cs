using System;

namespace CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;

public sealed class UpdateResearchLineCommand : CommandBase
{
    private static readonly UpdateResearchLineCommandValidation s_validation = new();

    public string Name { get; }
    public Guid ResearchGroupId { get; }
    public UpdateResearchLineCommand(Guid ResearchLineId,Guid researchGroupId, string name) : base(ResearchLineId)
    {
        Name = name;
        ResearchGroupId = researchGroupId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}