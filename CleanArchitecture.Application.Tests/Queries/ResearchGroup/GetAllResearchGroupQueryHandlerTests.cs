using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.ResearchGroups.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchGroups;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.ResearchGroups;

public sealed class GetAllResearchGroupsQueryHandlerTests
{
    private readonly GetAllResearchGroupsTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_ResearchGroup()
    {
        var ResearchGroup = _fixture.SetupResearchGroup();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ResearchGroupsQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        ResearchGroup.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_ResearchGroup()
    {
        _fixture.SetupResearchGroup(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ResearchGroupsQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}