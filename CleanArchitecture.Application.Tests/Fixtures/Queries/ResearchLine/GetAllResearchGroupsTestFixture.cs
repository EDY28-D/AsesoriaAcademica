using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.ResearchLines.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchLines;

public sealed class GetAllResearchLinesTestFixture : QueryHandlerBaseFixture
{
    public GetAllResearchLinesQueryHandler QueryHandler { get; }
    private IResearchLineRepository ResearchLineRepository { get; }

    public GetAllResearchLinesTestFixture()
    {
        ResearchLineRepository = Substitute.For<IResearchLineRepository>();
        var sortingProvider = new ResearchLineViewModelSortProvider();

        QueryHandler = new GetAllResearchLinesQueryHandler(ResearchLineRepository, sortingProvider);
    }

    public ResearchLine SetupResearchLine(bool deleted = false)
    {
        var ResearchLine = new ResearchLine(Guid.NewGuid(), "ResearchLine 1", Guid.NewGuid(), "sw123");

        if (deleted)
        {
            ResearchLine.Delete();
        }

        var ResearchLineList = new List<ResearchLine> { ResearchLine }.BuildMock();
        ResearchLineRepository.GetAllNoTracking().Returns(ResearchLineList);

        return ResearchLine;
    }
}