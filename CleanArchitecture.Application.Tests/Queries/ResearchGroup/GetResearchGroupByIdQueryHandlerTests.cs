using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchGroups;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.ResearchGroups;

public sealed class GetResearchGroupByIdQueryHandlerTests
{
    private readonly GetResearchGroupByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_ResearchGroup()
    {
        var ResearchGroup = _fixture.SetupResearchGroup();

        var result = await _fixture.QueryHandler.Handle(
            new GetResearchGroupByIdQuery(ResearchGroup.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        ResearchGroup.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_ResearchGroup()
    {
        var ResearchGroup = _fixture.SetupResearchGroup(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetResearchGroupByIdQuery(ResearchGroup.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetResearchGroupByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"ResearchGroup with id {ResearchGroup.Id} could not be found");
        result.Should().BeNull();
    }
}