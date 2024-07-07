 using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.ResearchLine;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.DeleteResearchLine;

public sealed class DeleteResearchLineCommandHandlerTests
{
    private readonly DeleteResearchLineCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_ResearchLine()
    {
        var ResearchLine = _fixture.SetupResearchLine();

        var command = new DeleteResearchLineCommand(ResearchLine.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ResearchLineDeletedEvent>(x => x.AggregateId == ResearchLine.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_ResearchLine()
    {
        _fixture.SetupResearchLine();

        var command = new DeleteResearchLineCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ResearchLineDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no ResearchLine with Id {command.AggregateId}");
    }

   
}