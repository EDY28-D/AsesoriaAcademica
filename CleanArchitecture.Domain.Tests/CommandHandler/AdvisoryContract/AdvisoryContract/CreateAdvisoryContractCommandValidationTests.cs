using System;
using System.Xml.Linq;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandValidationTests :
    ValidationTestBase<CreateAdvisoryContractCommand, CreateAdvisoryContractCommandValidation>
{
    public CreateAdvisoryContractCommandValidationTests() : base(new CreateAdvisoryContractCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AdvisoryContract_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.AdvisoryContract.EmptyId,
            "AdvisoryContract id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AdvisoryContract_message()
    {
        var command = CreateTestCommand(message: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.AdvisoryContract.EmptyMessage,
            "Message may not be empty");
    }


    private static CreateAdvisoryContractCommand CreateTestCommand(
        Guid? id = null,
         Guid? professorId = null,
         Guid? studentId = null,
         Guid? researchLineId = null,
        string?message = null,
        string?tesisTopic=null,
        string?status=null)
    {
        return new CreateAdvisoryContractCommand(
            id ?? Guid.NewGuid(),
            professorId??Guid.NewGuid(),
            studentId ?? Guid.NewGuid(),
            researchLineId ?? Guid.NewGuid(),
            tesisTopic ?? "tesisTopic",
            message ?? "message",   
            status??"sta");
    }
}

      