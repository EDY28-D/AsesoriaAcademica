 using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchGroup;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommandHandlerTests
{
    private readonly DeleteResearchGroupCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_ResearchGroup()
    {
        var ResearchGroup = _fixture.SetupResearchGroup();

        var command = new DeleteResearchGroupCommand(ResearchGroup.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ResearchGroupDeletedEvent>(x => x.AggregateId == ResearchGroup.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_ResearchGroup()
    {
        _fixture.SetupResearchGroup();

        var command = new DeleteResearchGroupCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchGroupDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no ResearchGroup with Id {command.AggregateId}");
    }

   
}