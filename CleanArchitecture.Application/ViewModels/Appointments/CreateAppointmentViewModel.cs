using System;

namespace CleanArchitecture.Application.ViewModels.Appointments;

public sealed record CreateAppointmentViewModel(Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress,string status,string googleEventId);
