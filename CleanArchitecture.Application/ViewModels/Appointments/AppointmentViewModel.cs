using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using static CleanArchitecture.Domain.Errors.DomainErrorCodes;
using Appointment = CleanArchitecture.Domain.Entities.Appointment;

namespace CleanArchitecture.Application.ViewModels.Appointments;

public sealed class AppointmentViewModel
{
    public Guid Id { get; set; }
    public Guid ProfessorId { get; set; }
    public Guid StudentId { get; set; }
    public Guid CalendarId { get; set; }
    public DateTime DateTime { get; set; }
    public string ProfessorProgress { get; set; }
    public string StudentProgress { get; set; }
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static AppointmentViewModel FromAppointment(Appointment Appointment)
    {
        return new AppointmentViewModel
        {
        Id = Appointment.Id,
        ProfessorId = Appointment.ProfessorId,
        StudentId = Appointment.StudentId,
        CalendarId = Appointment.CalendarId,
        DateTime = Appointment.DateTime,
        ProfessorProgress = Appointment.ProfessorProgress,
        StudentProgress = Appointment.StudentProgress,


        // Users = Appointment.Users.Select(UserViewModel.FromUser).ToList()
    };
    }
}