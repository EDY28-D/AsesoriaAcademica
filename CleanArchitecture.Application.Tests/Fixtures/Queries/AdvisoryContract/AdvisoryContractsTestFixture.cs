using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.AdvisoryContracts;

public sealed class GetAllAdvisoryContractsTestFixture : QueryHandlerBaseFixture
{
    public GetAllAdvisoryContractsQueryHandler QueryHandler { get; }
    private IAdvisoryContractRepository AdvisoryContractRepository { get; }

    public GetAllAdvisoryContractsTestFixture()
    {
        AdvisoryContractRepository = Substitute.For<IAdvisoryContractRepository>();
        var sortingProvider = new AdvisoryContractViewModelSortProvider();

        QueryHandler = new GetAllAdvisoryContractsQueryHandler(AdvisoryContractRepository, sortingProvider);
    }

    public AdvisoryContract SetupAdvisoryContract(bool deleted = false)
    {
        var AdvisoryContract = new AdvisoryContract(
            Ids.Seed.AdvisoryContractId, Ids.Seed.ProfessorId, Guid.NewGuid(), Guid.NewGuid(),
            "testTesisTopic", "testMessage", "testStatus");

        if (deleted)
        {
            AdvisoryContract.Delete();
        }

        var AdvisoryContractList = new List<AdvisoryContract> { AdvisoryContract }.BuildMock();
        AdvisoryContractRepository.GetAllNoTracking().Returns(AdvisoryContractList);

        return AdvisoryContract;
    }
}