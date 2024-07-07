 using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommandHandlerTests
{
    private readonly DeleteAdvisoryContractCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_AdvisoryContract()
    {
        var AdvisoryContract = _fixture.SetupAdvisoryContract();

        var command = new DeleteAdvisoryContractCommand(AdvisoryContract.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<AdvisoryContractDeletedEvent>(x => x.AggregateId == AdvisoryContract.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_AdvisoryContract()
    {
        _fixture.SetupAdvisoryContract();

        var command = new DeleteAdvisoryContractCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AdvisoryContractDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no AdvisoryContract with Id {command.AggregateId}");
    }

   
}