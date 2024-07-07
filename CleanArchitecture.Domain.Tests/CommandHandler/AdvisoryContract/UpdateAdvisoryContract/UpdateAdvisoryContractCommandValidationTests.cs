using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandValidationTests :
    ValidationTestBase<UpdateAdvisoryContractCommand, UpdateAdvisoryContractCommandValidation>
{
    public UpdateAdvisoryContractCommandValidationTests() : base(new UpdateAdvisoryContractCommandValidation())
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
    public void Should_Be_Invalid_For_Empty_AdvisoryContract_Message()
    {
        var command = CreateTestCommand(message: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.AdvisoryContract.EmptyMessage,
            "Message may not be empty");
    }

    private static UpdateAdvisoryContractCommand CreateTestCommand(
        Guid? id = null,
        Guid? professorId = null,
        Guid? studentId = null,
        Guid? researchLineId = null,
        string? tesisTopic = null,
        string? message = null,
        string? status=null)
    {
        return new UpdateAdvisoryContractCommand(
            id ?? Guid.NewGuid(), professorId ?? Guid.NewGuid(),studentId ?? Guid.NewGuid(),researchLineId?? Guid.NewGuid(),tesisTopic??"testopic",message??"testmessage",
            status?? "Teststauts");
    }
}