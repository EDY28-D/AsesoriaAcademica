using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.DeleteAdvisoryContract;

public sealed class DeleteAdvisoryContractCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteAdvisoryContractCommandHandler CommandHandler { get; }

    private IAdvisoryContractRepository AdvisoryContractRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteAdvisoryContractCommandTestFixture()
    {
        AdvisoryContractRepository = Substitute.For<IAdvisoryContractRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteAdvisoryContractCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            AdvisoryContractRepository,
            UserRepository,
            User);
    }

    public Entities.AdvisoryContract SetupAdvisoryContract()
    {
        var AdvisoryContract = new Entities.AdvisoryContract(Guid.NewGuid(),Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(), "Test AdvisoryContract","testcode","piendiente");

        AdvisoryContractRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == AdvisoryContract.Id))
            .Returns(AdvisoryContract);

        return AdvisoryContract;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}