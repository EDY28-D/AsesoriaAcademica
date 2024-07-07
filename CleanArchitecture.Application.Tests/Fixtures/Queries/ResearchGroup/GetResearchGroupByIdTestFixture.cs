using System;
using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchGroups;

public sealed class GetResearchGroupByIdTestFixture : QueryHandlerBaseFixture
{
    public GetResearchGroupByIdQueryHandler QueryHandler { get; }
    private IResearchGroupRepository ResearchGroupRepository { get; }

    public GetResearchGroupByIdTestFixture()
    {
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        QueryHandler = new GetResearchGroupByIdQueryHandler(
            ResearchGroupRepository,
            Bus);
    }

    public ResearchGroup SetupResearchGroup(bool deleted = false)
    {
        var ResearchGroup = new ResearchGroup(Guid.NewGuid(), "ResearchGroup 1","sw123");

        if (deleted)
        {
            ResearchGroup.Delete();
        }
        else
        {
            ResearchGroupRepository.GetByIdAsync(Arg.Is<Guid>(y => y == ResearchGroup.Id)).Returns(ResearchGroup);
        }


        return ResearchGroup;
    }
}