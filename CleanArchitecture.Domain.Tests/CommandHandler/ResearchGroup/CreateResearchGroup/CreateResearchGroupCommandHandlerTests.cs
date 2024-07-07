using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchGroup;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.CreateResearchGroup;

public sealed class CreateResearchGroupCommandHandlerTests
{
    private readonly CreateResearchGroupCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_ResearchGroup()
    {
        var command = new CreateResearchGroupCommand(
            Guid.NewGuid(),
            "Test ResearchGroup","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ResearchGroupCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.Name == command.Name);
    }

    [Fact]
    public async Task Should_Not_Create_ResearchGroup_Insufficient_Permissions()
    {
        _fixture.SetupUser();

        var command = new CreateResearchGroupCommand(
            Guid.NewGuid(),
            "Test ResearchGroup","testcode");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchGroupCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to create ResearchGroup {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_ResearchGroup_Already_Exists()
    {
        var command = new CreateResearchGroupCommand(
            Guid.NewGuid(),
            "Test ResearchGroup","testcode");

        _fixture.SetupExistingResearchGroup(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchGroupCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.ResearchGroup.AlreadyExists,
                $"There is already a ResearchGroup with Id {command.AggregateId}");
    }
}