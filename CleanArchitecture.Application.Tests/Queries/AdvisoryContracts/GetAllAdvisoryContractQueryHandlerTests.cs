using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.AdvisoryContracts;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.AdvisoryContracts;

public sealed class GetAllAdvisoryContractsQueryHandlerTests
{
    private readonly GetAllAdvisoryContractsTestFixture _fixture = new();

    //[Fact] neceistamos sembrar datos de las entidades realcionadas
    //public async Task Should_Get_Existing_AdvisoryContract()
    //{
    //    var AdvisoryContract = _fixture.SetupAdvisoryContract();

    //    var query = new PageQuery
    //    {
    //        PageSize = 10,
    //        Page = 1
    //    };

    //    var result = await _fixture.QueryHandler.Handle(
    //        new AdvisoryContractsQuery(query, false),
    //        default);

    //    _fixture.VerifyNoDomainNotification();

    //    result.PageSize.Should().Be(query.PageSize);
    //    result.Page.Should().Be(query.Page);
    //    result.Count.Should().Be(1);

    //    AdvisoryContract.Should().BeEquivalentTo(result.Items.First());
    //}

    [Fact]
    public async Task Should_Not_Get_Deleted_AdvisoryContract()
    {
        _fixture.SetupAdvisoryContract(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new AdvisoryContractsQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}