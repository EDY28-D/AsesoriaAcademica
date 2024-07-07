using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommandValidationTests :
    ValidationTestBase<DeleteAdvisoryContractCommand, DeleteAdvisoryContractCommandValidation>
{
    public DeleteAdvisoryContractCommandValidationTests() : base(new DeleteAdvisoryContractCommandValidation())
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

    private static DeleteAdvisoryContractCommand CreateTestCommand(Guid? AdvisoryContractId = null)
    {
        return new DeleteAdvisoryContractCommand(AdvisoryContractId ?? Guid.NewGuid());
    }
}