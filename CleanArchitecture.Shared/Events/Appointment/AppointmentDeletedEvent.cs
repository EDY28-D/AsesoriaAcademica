using System;

namespace CleanArchitecture.Shared.Events.Appointment;

public sealed class AppointmentDeletedEvent : DomainEvent
{
    public AppointmentDeletedEvent(Guid appointmentId) : base(appointmentId)
    {
    }
}