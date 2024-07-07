using System;
using CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchLine.DeleteResearchLine;

public sealed class DeleteResearchLineCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteResearchLineCommandHandler CommandHandler { get; }

    private IResearchLineRepository ResearchLineRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteResearchLineCommandTestFixture()
    {
        ResearchLineRepository = Substitute.For<IResearchLineRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteResearchLineCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ResearchLineRepository,
            UserRepository,
            User);
    }

    public Entities.ResearchLine SetupResearchLine()
    {
        var ResearchLine = new Entities.ResearchLine(Guid.NewGuid(),"Test REsearchLIne",Guid.NewGuid(),"testcode");

        ResearchLineRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == ResearchLine.Id))
            .Returns(ResearchLine);

        return ResearchLine;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}