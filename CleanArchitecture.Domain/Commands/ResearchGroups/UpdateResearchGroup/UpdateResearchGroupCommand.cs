using System;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;

public sealed class UpdateResearchGroupCommand : CommandBase
{
    private static readonly UpdateResearchGroupCommandValidation s_validation = new();

    public Guid Id { get; }
    public string Name { get; }

    public UpdateResearchGroupCommand(Guid ResearchGroupId, string name) : base(ResearchGroupId)
    {
        Name = name;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }


}