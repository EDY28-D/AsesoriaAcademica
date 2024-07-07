using System;
using CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.ResearchGroup.UpdateResearchGroup;

public sealed class UpdateResearchGroupCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateResearchGroupCommandHandler CommandHandler { get; }

    private IResearchGroupRepository ResearchGroupRepository { get; }

    public UpdateResearchGroupCommandTestFixture()
    {
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        CommandHandler = new UpdateResearchGroupCommandHandler(
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
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.ResearchGroup(id, "Test ResearchGroup","testcode"));
    }
}