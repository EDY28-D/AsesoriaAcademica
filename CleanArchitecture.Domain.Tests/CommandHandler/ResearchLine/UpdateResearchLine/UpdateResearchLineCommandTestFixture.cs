using System;
using CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.UpdateResearchLine;

public sealed class UpdateResearchLineCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateResearchLineCommandHandler CommandHandler { get; }

    private IResearchLineRepository ResearchLineRepository { get; }

    public UpdateResearchLineCommandTestFixture()
    {
        ResearchLineRepository = Substitute.For<IResearchLineRepository>();

        CommandHandler = new UpdateResearchLineCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ResearchLineRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingResearchLine(Guid id)
    {
        ResearchLineRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.ResearchLine(id, "Test ResearchLine",Guid.NewGuid(),"testcode"));
    }
}