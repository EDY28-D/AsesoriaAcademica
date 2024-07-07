using System;
using CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.UpdateResearchLine;

public sealed class UpdateResearchLineCommandValidationTests :
    ValidationTestBase<UpdateResearchLineCommand, UpdateResearchLineCommandValidation>
{
    public UpdateResearchLineCommandValidationTests() : base(new UpdateResearchLineCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_ResearchLine_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.ResearchLine.EmptyId,
            "ResearchLine id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_ResearchLine_Name()
    {
        var command = CreateTestCommand(name: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.ResearchLine.EmptyName,
            "Name may not be empty");
    }

    private static UpdateResearchLineCommand CreateTestCommand(
        Guid? id = null,
        string? name = null)
    {
        return new UpdateResearchLineCommand(
            id ?? Guid.NewGuid(), id ?? Guid.NewGuid(),
            name ?? "Test ResearchLine");
    }
}