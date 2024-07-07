using System;
using CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.CreateResearchGroup;

public sealed class CreateResearchGroupCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateResearchGroupCommandHandler CommandHandler { get; }

    private IResearchGroupRepository ResearchGroupRepository { get; }

    public CreateResearchGroupCommandTestFixture()
    {
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        CommandHandler = new CreateResearchGroupCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ResearchGroupRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingResearchGroup(Guid id)
    {
        ResearchGroupRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}