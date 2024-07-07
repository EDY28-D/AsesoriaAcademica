using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandHandlerTests
{
    private readonly CreateAdvisoryContractCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_AdvisoryContract()
    {
        var command = new CreateAdvisoryContractCommand(
            Guid.NewGuid(),Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),"test",
            "Test AdvisoryContract","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<AdvisoryContractCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.ProfessorId == command.ProfessorId);
    }

    [Fact]
    public async Task Should_Not_Create_AdvisoryContract_Insufficient_Permissions()
    {
        _fixture.SetupUser();

        var command = new CreateAdvisoryContractCommand(
            Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),"test",
            "Test AdvisoryContract","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AdvisoryContractCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to create AdvisoryContract {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_AdvisoryContract_Already_Exists()
    {
        var command = new CreateAdvisoryContractCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),"test",
            "Test AdvisoryContract","testcode");

        _fixture.SetupExistingAdvisoryContract(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AdvisoryContractCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.AdvisoryContract.AlreadyExists,
                $"There is already a AdvisoryContract with Id {command.AggregateId}");
    }
}