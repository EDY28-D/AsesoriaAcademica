using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.ResearchLines.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchLines;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.ResearchLines;

public sealed class GetAllResearchLinesQueryHandlerTests
{
    private readonly GetAllResearchLinesTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_ResearchLine()
    {
        var ResearchLine = _fixture.SetupResearchLine();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ResearchLinesQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        ResearchLine.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_ResearchLine()
    {
        _fixture.SetupResearchLine(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ResearchLinesQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}