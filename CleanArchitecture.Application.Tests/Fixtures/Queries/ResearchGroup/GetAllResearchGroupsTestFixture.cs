using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.ResearchGroups.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchGroups;

public sealed class GetAllResearchGroupsTestFixture : QueryHandlerBaseFixture
{
    public GetAllResearchGroupsQueryHandler QueryHandler { get; }
    private IResearchGroupRepository ResearchGroupRepository { get; }

    public GetAllResearchGroupsTestFixture()
    {
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();
        var sortingProvider = new ResearchGroupViewModelSortProvider();

        QueryHandler = new GetAllResearchGroupsQueryHandler(ResearchGroupRepository, sortingProvider);
    }

    public ResearchGroup SetupResearchGroup(bool deleted = false)
    {
        var ResearchGroup = new ResearchGroup(Guid.NewGuid(), "ResearchGroup 1", "sw123");

        if (deleted)
        {
            ResearchGroup.Delete();
        }

        var ResearchGroupList = new List<ResearchGroup> { ResearchGroup }.BuildMock();
        ResearchGroupRepository.GetAllNoTracking().Returns(ResearchGroupList);

        return ResearchGroup;
    }
}