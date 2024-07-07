using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchGroup;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.UpdateResearchGroup;

public sealed class UpdateResearchGroupCommandHandlerTests
{
    private readonly UpdateResearchGroupCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Update_ResearchGroup()
    {
        var command = new UpdateResearchGroupCommand(
            Guid.NewGuid(),
            "ResearchGroup Name");

        _fixture.SetupExistingResearchGroup(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyCommit()
            .VerifyNoDomainNotification()
            .VerifyRaisedEvent<ResearchGroupUpdatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.Name == command.Name);
    }

    [Fact]
    public async Task Should_Not_Update_ResearchGroup_Insufficient_Permissions()
    {
        var command = new UpdateResearchGroupCommand(
            Guid.NewGuid(),
            "ResearchGroup Name");

        _fixture.SetupUser();

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchGroupUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to update ResearchGroup {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Update_ResearchGroup_Not_Existing()
    {
        var command = new UpdateResearchGroupCommand(
            Guid.NewGuid(),
            "ResearchGroup Name");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchGroupUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no ResearchGroup with Id {command.AggregateId}");
    }
}