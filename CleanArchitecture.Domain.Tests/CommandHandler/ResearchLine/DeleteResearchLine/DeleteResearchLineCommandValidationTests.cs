using System;
using CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.DeleteResearchLine;

public sealed class DeleteResearchLineCommandValidationTests :
    ValidationTestBase<DeleteResearchLineCommand, DeleteResearchLineCommandValidation>
{
    public DeleteResearchLineCommandValidationTests() : base(new DeleteResearchLineCommandValidation())
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

    private static DeleteResearchLineCommand CreateTestCommand(Guid? ResearchLineId = null)
    {
        return new DeleteResearchLineCommand(ResearchLineId ?? Guid.NewGuid());
    }
}