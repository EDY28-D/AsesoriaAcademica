using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Appointments;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Appointments;
public sealed class GetAppointmentByIdQueryHandlerTests
{
    private readonly GetAppointmentByIdTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Appointment()
    {
        var Appointment = _fixture.SetupAppointment();

        var result = await _fixture.QueryHandler.Handle(
            new GetAppointmentByIdQuery(Appointment.Id),
            default);

        _fixture.VerifyNoDomainNotification();

        Appointment.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Appointment()
    {
        var Appointment = _fixture.SetupAppointment(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetAppointmentByIdQuery(Appointment.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetAppointmentByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Appointment with id {Appointment.Id} could not be found");
        result.Should().BeNull();
    }
}