using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using System;

namespace CleanArchitecture.IntegrationTests.Fixtures;
public sealed class AppointmentTestFixture : TestFixtureBase
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ProfessorId { get; } = Ids.Seed.ProfessorId;
    public Guid StudentId { get; } = Ids.Seed.StudentId;
    public Guid CalendarId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);
        context.ResearchGroups.Add(new ResearchGroup(Ids.Seed.ResearchGroupId, "test", "test"));
        context.ResearchLines.Add(new ResearchLine(Ids.Seed.ResearchLineId, "test", Ids.Seed.ResearchGroupId, "test"));
        context.Professors.Add(new Professor(Ids.Seed.ProfessorId, Ids.Seed.UserId, Ids.Seed.ResearchGroupId, false));
        context.Students.Add(new Student(Ids.Seed.StudentId, Ids.Seed.UserId, "test"));



        context.Appointments.Add(new Appointment(
            Id, ProfessorId, StudentId, CalendarId, DateTime.UtcNow,
            "Estado de la Appointment", 
            "Asunto de la Appointment","nuevo","eventId")); 


        context.SaveChanges();
    }
}
