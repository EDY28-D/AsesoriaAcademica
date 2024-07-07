using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.AdvisoryContracts;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.AdvisoryContracts;

public sealed class GetAdvisoryContractByIdQueryHandlerTests
{
    private readonly GetAdvisoryContractByIdTestFixture _fixture = new();

    //[Fact]
    //public async Task Should_Get_Existing_AdvisoryContract()
    //{
    //    var AdvisoryContract = _fixture.SetupAdvisoryContract();

    //    var result = await _fixture.QueryHandler.Handle(
    //        new GetAdvisoryContractByIdQuery(AdvisoryContract.Id),
    //        default);

    //    _fixture.VerifyNoDomainNotification();

    //    AdvisoryContract.Should().BeEquivalentTo(result);
    //}

    [Fact]
    public async Task Should_Not_Get_Deleted_AdvisoryContract()
    {
        var AdvisoryContract = _fixture.SetupAdvisoryContract(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetAdvisoryContractByIdQuery(AdvisoryContract.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetAdvisoryContractByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"AdvisoryContract with id {AdvisoryContract.Id} could not be found");
        result.Should().BeNull();
    }
}