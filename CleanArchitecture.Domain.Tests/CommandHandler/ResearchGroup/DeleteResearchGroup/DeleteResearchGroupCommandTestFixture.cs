using System;
using CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.DeleteResearchGroup;

public sealed class DeleteResearchGroupCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteResearchGroupCommandHandler CommandHandler { get; }

    private IResearchGroupRepository ResearchGroupRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteResearchGroupCommandTestFixture()
    {
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteResearchGroupCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ResearchGroupRepository,
            UserRepository,
            User);
    }

    public Entities.ResearchGroup SetupResearchGroup()
    {
        var ResearchGroup = new Entities.ResearchGroup(Guid.NewGuid(), "TestResearchGroup","testcode");

        ResearchGroupRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == ResearchGroup.Id))
            .Returns(ResearchGroup);

        return ResearchGroup;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}