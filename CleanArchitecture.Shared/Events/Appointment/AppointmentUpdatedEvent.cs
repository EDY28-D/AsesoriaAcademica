using System;
using System.Globalization;

namespace CleanArchitecture.Shared.Events.Appointment;

public sealed class AppointmentUpdatedEvent : DomainEvent
{
    public Guid ProfessorId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CalendarId { get; private set; }
    public DateTime DateTime { get; private set; }
    public string ProfessorProgress { get; private set; }
    public string StudentProgress { get; private set; }

    public AppointmentUpdatedEvent(Guid id, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(id)
    {
        ProfessorId = professorId;
        StudentId = studentId;
        CalendarId = calendarId;
        DateTime = dateTime;
        ProfessorProgress = professorProgress;
        StudentProgress = studentProgress;
    }
}