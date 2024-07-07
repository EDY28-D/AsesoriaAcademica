using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class AppointmentControllerTests : IClassFixture<AppointmentTestFixture>
{
    private readonly AppointmentTestFixture _fixture;

    public AppointmentControllerTests(AppointmentTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_Appointment_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<AppointmentViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.Id);
    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_Appointments()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/Appointment?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<AppointmentViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.Id)
            .Should().NotBeNull();
    }

    [Fact]
    [Priority(10)]
    //public async Task Should_Create_Appointment()
    //{
    //    var request = new CreateAppointmentViewModel(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "Nuevo Estado", "Nuevo Asunto","nuevo","evento1");

    //    var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/Appointment", request);

    //    response.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var message = await response.Content.ReadAsJsonAsync<Guid>();
    //    var appointmentId = message?.Data;

    //    // Check if Appointment exists
    //    var appointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{appointmentId}");

    //    appointmentResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var appointmentMessage = await appointmentResponse.Content.ReadAsJsonAsync<AppointmentViewModel>();

    //    appointmentMessage?.Data.Should().NotBeNull();

    //    appointmentMessage!.Data!.Id.Should().Be(appointmentId!.Value);
    //    appointmentMessage.Data.ProfessorId.Should().Be(request.professorId);
    //    appointmentMessage.Data.StudentId.Should().Be(request.studentId);
    //    appointmentMessage.Data.CalendarId.Should().Be(request.calendarId);
    //    appointmentMessage.Data.DateTime.Should().Be(request.dateTime);
    //    appointmentMessage.Data.ProfessorProgress.Should().Be(request.professorProgress);
    //    appointmentMessage.Data.StudentProgress.Should().Be(request.studentProgress);

    //}

    //[Fact]
    //[Priority(15)]
    //public async Task Should_Update_Appointment()
    //{
    //    var request = new UpdateAppointmentViewModel(_fixture.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, "Actualizado Estado", "Actualizado Asunto");

    //    var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/Appointment", request);

    //    response.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var message = await response.Content.ReadAsJsonAsync<UpdateAppointmentViewModel>();

    //    message?.Data.Should().NotBeNull();
    //    message!.Data.Should().BeEquivalentTo(request);

    //    // Check if Appointment is updated
    //    var appointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

    //    appointmentResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var appointmentMessage = await appointmentResponse.Content.ReadAsJsonAsync<AppointmentViewModel>();

    //    appointmentMessage?.Data.Should().NotBeNull();
    //    appointmentMessage!.Data!.Id.Should().Be(_fixture.Id);
    //}

    //[Fact]
    //[Priority(20)]
    public async Task Should_Delete_Appointment()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/Appointment/{_fixture.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if Appointment is deleted
        var appointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

        appointmentResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [Priority(25)]
    public async Task Should_Not_Get_Non_Existing_Appointment()
    {
        var nonExistingId = Guid.NewGuid();
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{nonExistingId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
