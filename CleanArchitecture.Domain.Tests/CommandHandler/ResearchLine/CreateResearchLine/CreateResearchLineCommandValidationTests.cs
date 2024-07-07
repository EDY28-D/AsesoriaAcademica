using System;
using CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.CreateResearchLine;

public sealed class CreateResearchLineCommandValidationTests :
    ValidationTestBase<CreateResearchLineCommand, CreateResearchLineCommandValidation>
{
    public CreateResearchLineCommandValidationTests() : base(new CreateResearchLineCommandValidation())
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

    private static CreateResearchLineCommand CreateTestCommand(
        Guid? id = null,
        string? name = null)
    {
        return new CreateResearchLineCommand(
            id ?? Guid.NewGuid(), id ?? Guid.NewGuid(),
            name ?? "Test ResearchLine","testcode");
    }
}