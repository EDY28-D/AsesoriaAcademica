using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class AdvisoryContractControllerTests : IClassFixture<AdvisoryContractTestFixture>
{
    private readonly AdvisoryContractTestFixture _fixture;

    public AdvisoryContractControllerTests(AdvisoryContractTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_AdvisoryContract_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/AdvisoryContract/{_fixture.CreatedAdvisoryContractId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<AdvisoryContractViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.AdvisoryContractId.Should().Be(_fixture.CreatedAdvisoryContractId);
        message.Data.Message.Should().Be("test");


    }

    //[Fact]
    //[Priority(5)]
    //public async Task Should_Get_All_AdvisoryContracts()
    //{
    //    var response = await _fixture.ServerClient.GetAsync(
    //        "api/v1/AdvisoryContract?searchTerm=Test&pageSize=5&page=1");

    //    response.StatusCode.Should().Be(HttpStatusCode.OK);

    //    //var message = await response.Content.ReadAsJsonAsync<PagedResult<AdvisoryContractViewModel>>();

    //    //message?.Data!.Items.Should().NotBeEmpty();
    //    //message!.Data!.Items.Should().HaveCount(1);
    //    //message.Data!.Items
    //    //    .FirstOrDefault(x => x.AdvisoryContractId == _fixture.CreatedAdvisoryContractId)
    //    //    .Should().NotBeNull();

    //}

    [Fact]
    [Priority(10)]
    public async Task Should_Create_AdvisoryContract()
    {
        var request = new CreateAdvisoryContractViewModel(Ids.Seed.ProfessorId,Ids.Seed.StudentId,Ids.Seed.ResearchLineId,"test","test","test");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/AdvisoryContract", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var AdvisoryContractId = message?.Data;

        // Check if AdvisoryContract exists
        var AdvisoryContractResponse = await _fixture.ServerClient.GetAsync($"/api/v1/AdvisoryContract/{AdvisoryContractId}");
        Console.WriteLine(AdvisoryContractResponse);

        AdvisoryContractResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var AdvisoryContractMessage = await AdvisoryContractResponse.Content.ReadAsJsonAsync<AdvisoryContractViewModel>();

        AdvisoryContractMessage?.Data.Should().NotBeNull();

        AdvisoryContractMessage!.Data!.AdvisoryContractId.Should().Be(AdvisoryContractId!.Value);
    }

    [Fact]
    [Priority(15)]
    public async Task Should_Update_AdvisoryContract()
    {
        var request = new UpdateAdvisoryContractViewModel(_fixture.CreatedAdvisoryContractId, Ids.Seed.ProfessorId, Ids.Seed.StudentId, Ids.Seed.ResearchLineId,"test","test","test");

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/AdvisoryContract", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateAdvisoryContractViewModel>();

        message?.Data.Should().NotBeNull();
        message!.Data.Should().BeEquivalentTo(request);

        // Check if AdvisoryContract is updated
        var AdvisoryContractResponse = await _fixture.ServerClient.GetAsync($"/api/v1/AdvisoryContract/{_fixture.CreatedAdvisoryContractId}");

        AdvisoryContractResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var AdvisoryContractMessage = await response.Content.ReadAsJsonAsync<AdvisoryContractViewModel>();

        AdvisoryContractMessage?.Data.Should().NotBeNull();
        AdvisoryContractMessage!.Data!.ProfessorId.Should().Be(Ids.Seed.ProfessorId);
        AdvisoryContractMessage.Data.Message.Should().Be(request.message);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_AdvisoryContract()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/AdvisoryContract/{_fixture.CreatedAdvisoryContractId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if AdvisoryContract is deleted
        var AdvisoryContractResponse = await _fixture.ServerClient.GetAsync($"/api/v1/AdvisoryContract/{_fixture.CreatedAdvisoryContractId}");

        AdvisoryContractResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    /// <summary>
    /// Caso negativo
    /// </summary>
    /// <returns></returns>
    [Fact]
    [Priority(25)]
    public async Task Should_Not_Get_Non_Existing_AdvisoryContract()
    {
        var nonExistingId = Guid.NewGuid();
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/AdvisoryContract/{nonExistingId}");

    }
 }