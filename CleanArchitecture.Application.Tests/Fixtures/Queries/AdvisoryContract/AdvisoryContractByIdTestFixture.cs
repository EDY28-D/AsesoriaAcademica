using System;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.AdvisoryContracts;

public sealed class GetAdvisoryContractByIdTestFixture : QueryHandlerBaseFixture
{
    public GetAdvisoryContractByIdQueryHandler QueryHandler { get; }
    private IAdvisoryContractRepository AdvisoryContractRepository { get; }

    public GetAdvisoryContractByIdTestFixture()
    {
        AdvisoryContractRepository = Substitute.For<IAdvisoryContractRepository>();

        QueryHandler = new GetAdvisoryContractByIdQueryHandler(
            AdvisoryContractRepository,
            Bus);
    }

    public AdvisoryContract SetupAdvisoryContract(bool deleted = false)
    {
        var AdvisoryContract = new AdvisoryContract(
            Ids.Seed.AdvisoryContractId, Ids.Seed.ProfessorId ,Ids.Seed.StudentId, Guid.NewGuid(),
            "testTesisTopic", "testMessage", "testStatus");

        if (deleted)
        {
            AdvisoryContract.Delete();
        }
        else
        {
            AdvisoryContractRepository.GetByIdAsync(Arg.Is<Guid>(y => y == AdvisoryContract.Id)).Returns(AdvisoryContract);
        }


        return AdvisoryContract;
    }
}