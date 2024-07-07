using System;
using CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.CreateResearchLine;

public sealed class CreateResearchLineCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateResearchLineCommandHandler CommandHandler { get; }

    private IResearchLineRepository ResearchLineRepository { get; }
    private IResearchGroupRepository ResearchGroupRepository { get; }
    public CreateResearchLineCommandTestFixture()
    {
        ResearchLineRepository = Substitute.For<IResearchLineRepository>();
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        CommandHandler = new CreateResearchLineCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ResearchLineRepository,ResearchGroupRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingResearchLine(Guid id)
    {
        ResearchLineRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}