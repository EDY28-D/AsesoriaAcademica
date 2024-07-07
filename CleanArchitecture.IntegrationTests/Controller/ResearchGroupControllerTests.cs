using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class ResearchGroupControllerTests : IClassFixture<ResearchGroupTestFixture>
{
    private readonly ResearchGroupTestFixture _fixture;

    public ResearchGroupControllerTests(ResearchGroupTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_ResearchGroup_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{_fixture.CreatedResearchGroupId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<ResearchGroupViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.CreatedResearchGroupId);
        message.Data.Name.Should().Be("Test ResearchGroup");


    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_ResearchGroups()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/ResearchGroup?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<ResearchGroupViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.CreatedResearchGroupId)
            .Should().NotBeNull();

    }
/*
    [Fact]
    [Priority(10)]
    public async Task Should_Create_ResearchGroup()
    {
        var request = new CreateResearchGroupViewModel("Test ResearchGroup 2","sw123");

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/ResearchGroup", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var ResearchGroupId = message?.Data;

        // Check if ResearchGroup exists
        var ResearchGroupResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{ResearchGroupId}");

        ResearchGroupResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var ResearchGroupMessage = await ResearchGroupResponse.Content.ReadAsJsonAsync<ResearchGroupViewModel>();

        ResearchGroupMessage?.Data.Should().NotBeNull();

        ResearchGroupMessage!.Data!.Id.Should().Be(ResearchGroupId!.Value);
        ResearchGroupMessage.Data.Name.Should().Be(request.Name);
    }*/




[Fact]
[Priority(10)]
   public void Should_Create_ResearchGroup_Using_ThreadPool()
   {
    const int numberOfThreads = 5;
    ManualResetEvent[] doneEvents = new ManualResetEvent[numberOfThreads];

    for (int i = 0; i < numberOfThreads; i++)
    {
        doneEvents[i] = new ManualResetEvent(false);
        ThreadPool.QueueUserWorkItem(new WaitCallback(async (state) =>
        {
            var index = (int)state;
            try
            {
                await Should_Create_ResearchGroup();
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error en el hilo {index}: {ex.Message}");
            }
            finally
            {
                doneEvents[index].Set();
            }
        }), i);
    }

    WaitHandle.WaitAll(doneEvents);
}

public async Task Should_Create_ResearchGroup()
{
    var request = new CreateResearchGroupViewModel("Test ResearchGroup 2", "sw123");

    var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/ResearchGroup", request);

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var message = await response.Content.ReadAsJsonAsync<Guid>();
    var ResearchGroupId = message?.Data;

    // Check if ResearchGroup exists
    var ResearchGroupResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{ResearchGroupId}");

    ResearchGroupResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var ResearchGroupMessage = await ResearchGroupResponse.Content.ReadAsJsonAsync<ResearchGroupViewModel>();

    ResearchGroupMessage?.Data.Should().NotBeNull();

    ResearchGroupMessage!.Data!.Id.Should().Be(ResearchGroupId!.Value);
    ResearchGroupMessage.Data.Name.Should().Be(request.Name);
}


[Fact]
    [Priority(15)]
    public async Task Should_Update_ResearchGroup()
    {
        var request = new UpdateResearchGroupViewModel(_fixture.CreatedResearchGroupId, "Test ResearchGroup 3","sw123");

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/ResearchGroup", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateResearchGroupViewModel>();

        message?.Data.Should().NotBeNull();
        message!.Data.Should().BeEquivalentTo(request);

        // Check if ResearchGroup is updated
        var ResearchGroupResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{_fixture.CreatedResearchGroupId}");

        ResearchGroupResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var ResearchGroupMessage = await response.Content.ReadAsJsonAsync<ResearchGroupViewModel>();

        ResearchGroupMessage?.Data.Should().NotBeNull();

        ResearchGroupMessage!.Data!.Id.Should().Be(_fixture.CreatedResearchGroupId);
        ResearchGroupMessage.Data.Name.Should().Be(request.Name);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_ResearchGroup()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/ResearchGroup/{_fixture.CreatedResearchGroupId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if ResearchGroup is deleted
        var ResearchGroupResponse = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{_fixture.CreatedResearchGroupId}");

        ResearchGroupResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    /// <summary>
    /// Caso negativo: Intentar obtener un grupo de investigación que no existe
    /// </summary>
    /// <returns></returns>
    [Fact]
    [Priority(25)]
    public async Task Should_Not_Get_Non_Existing_ResearchGroup()
    {
        var nonExistingId = Guid.NewGuid();
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/ResearchGroup/{nonExistingId}");

    }
 } 