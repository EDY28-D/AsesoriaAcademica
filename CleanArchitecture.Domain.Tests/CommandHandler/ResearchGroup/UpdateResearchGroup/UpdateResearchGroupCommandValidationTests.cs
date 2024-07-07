using System;
using CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.UpdateResearchGroup;

public sealed class UpdateResearchGroupCommandValidationTests :
    ValidationTestBase<UpdateResearchGroupCommand, UpdateResearchGroupCommandValidation>
{
    public UpdateResearchGroupCommandValidationTests() : base(new UpdateResearchGroupCommandValidation())
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

    [Fact]
    public void Should_Be_Invalid_For_Empty_ResearchGroup_Name()
    {
        var command = CreateTestCommand(name: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.ResearchGroup.EmptyName,
            "Name may not be empty");
    }

    private static UpdateResearchGroupCommand CreateTestCommand(
        Guid? id = null,
        string? name = null)
    {
        return new UpdateResearchGroupCommand(
            id ?? Guid.NewGuid(),
            name ?? "Test ResearchGroup");
    }
}