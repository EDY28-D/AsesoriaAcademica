using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchLine;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.CreateResearchLine;

public sealed class CreateResearchLineCommandHandlerTests
{
    private readonly CreateResearchLineCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_ResearchLine()
    {
        var command = new CreateResearchLineCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "Test ResearchLine","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ResearchLineCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.ResearchGroupId == command.ResearchGroupId);
    }

    [Fact]
    public async Task Should_Not_Create_ResearchLine_Insufficient_Permissions()
    {
        _fixture.SetupUser();

        var command = new CreateResearchLineCommand(
            Guid.NewGuid(), Guid.NewGuid(),
            "Test ResearchLine","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchLineCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to create ResearchLine {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_ResearchLine_Already_Exists()
    {
        var command = new CreateResearchLineCommand(
            Guid.NewGuid(), Guid.NewGuid(),
            "Test ResearchLine","testcode");

        _fixture.SetupExistingResearchLine(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchLineCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.ResearchLine.AlreadyExists,
                $"There is already a ResearchLine with Id {command.AggregateId}");
    }
}