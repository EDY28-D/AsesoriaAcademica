using System;
using CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchLines;

public sealed class GetResearchLineByIdTestFixture : QueryHandlerBaseFixture
{
    public GetResearchLineByIdQueryHandler QueryHandler { get; }
    private IResearchLineRepository ResearchLineRepository { get; }

    public GetResearchLineByIdTestFixture()
    {
        ResearchLineRepository = Substitute.For<IResearchLineRepository>();

        QueryHandler = new GetResearchLineByIdQueryHandler(
            ResearchLineRepository,
            Bus);
    }

    public ResearchLine SetupResearchLine(bool deleted = false)
    {
        var ResearchLine = new ResearchLine(Guid.NewGuid(), "ResearchLine 1", Guid.NewGuid(),"sw123");

        if (deleted)
        {
            ResearchLine.Delete();
        }
        else
        {
            ResearchLineRepository.GetByIdAsync(Arg.Is<Guid>(y => y == ResearchLine.Id)).Returns(ResearchLine);
        }


        return ResearchLine;
    }
}