using System;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;

public sealed class CreateResearchGroupCommand : CommandBase
{
    private static readonly CreateResearchGroupCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }

    public CreateResearchGroupCommand(Guid ResearchGroupId, string name,string code) : base(ResearchGroupId)
    {
        Name = name;
        Code = code;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}