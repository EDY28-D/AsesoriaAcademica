using System;
using CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommandValidationTests :
    ValidationTestBase<DeleteResearchGroupCommand, DeleteResearchGroupCommandValidation>
{
    public DeleteResearchGroupCommandValidationTests() : base(new DeleteResearchGroupCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_ResearchGroup_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.ResearchGroup.EmptyId,
            "ResearchGroup id may not be empty");
    }

    private static DeleteResearchGroupCommand CreateTestCommand(Guid? ResearchGroupId = null)
    {
        return new DeleteResearchGroupCommand(ResearchGroupId ?? Guid.NewGuid());
    }
}