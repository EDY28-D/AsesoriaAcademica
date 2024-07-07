using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateAdvisoryContractCommandHandler CommandHandler { get; }

    private IAdvisoryContractRepository AdvisoryContractRepository { get; }

    public UpdateAdvisoryContractCommandTestFixture()
    {
        AdvisoryContractRepository = Substitute.For<IAdvisoryContractRepository>();

        CommandHandler = new UpdateAdvisoryContractCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            AdvisoryContractRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingAdvisoryContract(Guid id)
    {
        AdvisoryContractRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == id))
            .Returns(new Entities.AdvisoryContract(id ,Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),"test","testcode","test"));
    }
}