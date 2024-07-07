using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Appointments.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Appointments;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Appointments;

public sealed class GetAllAppointmentsQueryHandlerTests
{
    private readonly GetAllAppointmentsTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Appointment()
    {
        var Appointment = _fixture.SetupAppointment();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new AppointmentsQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        Appointment.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Appointment()
    {
        _fixture.SetupAppointment(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new AppointmentsQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}