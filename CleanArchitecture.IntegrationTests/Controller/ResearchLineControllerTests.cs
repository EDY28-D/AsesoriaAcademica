using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class ResearchLineControllerTests : IClassFixture<ResearchLineTestFixture>
{
    private readonly ResearchLineTestFixture _fixture;

    public ResearchLineControllerTests(ResearchLineTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_ResearchLine_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchLine/{_fixture.CreatedResearchLineId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<ResearchLineViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.CreatedResearchLineId);
        message.Data.Name.Should().Be("Test ResearchLine");


    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_ResearchLines()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/ResearchLine?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<ResearchLineViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.CreatedResearchLineId)
            .Should().NotBeNull();

    }

    [Fact]
    [Priority(10)]
    public async Task Should_Create_ResearchLine()
    {
        var request = new CreateResearchLineViewModel(Ids.Seed.ResearchGroupId, "Test ResearchLine 2","sw123");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/ResearchLine", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var ResearchLineId = message?.Data;

        // Check if ResearchLine exists
        var ResearchLineResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchLine/{ResearchLineId}");

        ResearchLineResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var ResearchLineMessage = await ResearchLineResponse.Content.ReadAsJsonAsync<ResearchLineViewModel>();

        ResearchLineMessage?.Data.Should().NotBeNull();

        ResearchLineMessage!.Data!.Id.Should().Be(ResearchLineId!.Value);
        ResearchLineMessage.Data.Name.Should().Be(request.Name);
    }

    [Fact]
    [Priority(15)]
    public async Task Should_Update_ResearchLine()
    {
        var request = new UpdateResearchLineViewModel(_fixture.CreatedResearchLineId, Ids.Seed.ResearchGroupId, "Test ResearchLine 3","sw123");

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/ResearchLine", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateResearchLineViewModel>();

        message?.Data.Should().NotBeNull();
        message!.Data.Should().BeEquivalentTo(request);

        // Check if ResearchLine is updated
        var ResearchLineResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchLine/{_fixture.CreatedResearchLineId}");

        ResearchLineResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var ResearchLineMessage = await response.Content.ReadAsJsonAsync<ResearchLineViewModel>();

        ResearchLineMessage?.Data.Should().NotBeNull();

        ResearchLineMessage!.Data!.Id.Should().Be(_fixture.CreatedResearchLineId);
        ResearchLineMessage.Data.Name.Should().Be(request.Name);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_ResearchLine()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/ResearchLine/{_fixture.CreatedResearchLineId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if ResearchLine is deleted
        var ResearchLineResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchLine/{_fixture.CreatedResearchLineId}");

        ResearchLineResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    /// <summary>
    /// Caso negativo: Intentar obtener un grupo de investigación que no existe
    /// </summary>
    /// <returns></returns>
    [Fact]
    [Priority(25)]
    public async Task Should_Not_Get_Non_Existing_ResearchLine()
    {
        var nonExistingId = Guid.NewGuid();
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchLine/{nonExistingId}");

    }
 }