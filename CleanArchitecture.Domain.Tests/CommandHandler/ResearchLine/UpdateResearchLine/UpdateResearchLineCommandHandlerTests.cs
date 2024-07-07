using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchLine;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.UpdateResearchLine;

public sealed class UpdateResearchLineCommandHandlerTests
{
    private readonly UpdateResearchLineCommandTestFixture _fixture = new();

   /** [Fact]
    public async Task Should_Update_ResearchLine()
    {
        var command = new UpdateResearchLineCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "ResearchLine Name");

        _fixture.SetupExistingResearchLine(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyCommit()
            .VerifyNoDomainNotification()
            .VerifyRaisedEvent<ResearchLineUpdatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.ResearchGroupId == command.ResearchGroupId);
    }**/

    [Fact]
    public async Task Should_Not_Update_ResearchLine_Insufficient_Permissions()
    {
        var command = new UpdateResearchLineCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "ResearchLine Name");

        _fixture.SetupUser();

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchLineUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to update ResearchLine {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Update_ResearchLine_Not_Existing()
    {
        var command = new UpdateResearchLineCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "ResearchLine Name");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchLineUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no ResearchLine with Id {command.AggregateId}");
    }
}