using CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.ResearchLines;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.ResearchLines;

public sealed class GetResearchLineByIdQueryHandlerTests
{
    private readonly GetResearchLineByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_ResearchLine()
    {
        var ResearchLine = _fixture.SetupResearchLine();

        var result = await _fixture.QueryHandler.Handle(
            new GetResearchLineByIdQuery(ResearchLine.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        ResearchLine.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_ResearchLine()
    {
        var ResearchLine = _fixture.SetupResearchLine(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetResearchLineByIdQuery(ResearchLine.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetResearchLineByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"ResearchLine with id {ResearchLine.Id} could not be found");
        result.Should().BeNull();
    }
}