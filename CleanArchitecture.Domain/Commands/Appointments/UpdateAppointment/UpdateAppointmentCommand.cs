using System;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment
{
    public sealed class UpdateAppointmentCommand : CommandBase, IRequest<Unit>
    {
        private static readonly UpdateAppointmentCommandValidation s_validation = new();

        public Guid ProfessorId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CalendarId { get; set; }
        public DateTime DateTime { get; set; }
        public string ProfessorProgress { get; set; }
        public string StudentProgress { get; set; }

        public UpdateAppointmentCommand(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(appointmentId)
        {
            ProfessorId = professorId;
            StudentId = studentId;
            CalendarId = calendarId;
            DateTime = dateTime;
            ProfessorProgress = professorProgress;
            StudentProgress = studentProgress;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
