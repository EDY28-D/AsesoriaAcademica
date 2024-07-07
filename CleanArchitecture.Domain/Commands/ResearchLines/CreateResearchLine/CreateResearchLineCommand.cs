using System;

namespace CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;

public sealed class CreateResearchLineCommand : CommandBase
{
    private static readonly CreateResearchLineCommandValidation s_validation = new();

    public Guid ResearchLineId { get; }
    public Guid ResearchGroupId { get; }
    public string Name { get; }
    public string Code { get; }
   

    public CreateResearchLineCommand(Guid researchLineId, Guid researchGroupId,string name,string code) : base(researchLineId)
    {
        ResearchLineId = researchLineId;
        ResearchGroupId = researchGroupId;
        Name = name;
        Code = code;

    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}