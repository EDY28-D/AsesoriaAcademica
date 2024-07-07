using RabbitMQ.Client;
using System;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment
{
    public sealed class CreateAppointmentCommand : CommandBase
    {
        private static readonly CreateAppointmentCommandValidation s_validation = new();

        public Guid ProfessorId { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CalendarId { get; set; }
        public DateTime DateTime { get; set; }
        public string ProfessorProgress { get; set; }
        public string StudentProgress { get; set; }
        public string Status { get; set; }
        public string GoogleEventId { get; set; }

        public CreateAppointmentCommand(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress,string status,string googleEventId) : base(appointmentId)
        {
            AppointmentId = appointmentId;
            ProfessorId = professorId;
            StudentId = studentId;
            CalendarId = calendarId;
            DateTime = dateTime;
            ProfessorProgress = professorProgress;
            StudentProgress = studentProgress;
            Status = status;
            GoogleEventId = googleEventId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
