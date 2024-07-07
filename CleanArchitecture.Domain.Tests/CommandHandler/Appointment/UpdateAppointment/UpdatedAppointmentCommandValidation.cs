//using CleanArchitecture.Domain.Errors;
//using FluentValidation;

//namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment
//{
//    public sealed class UpdateAppointmentCommandValidation : AbstractValidator<UpdateAppointmentCommand>
//    {
//        public UpdateAppointmentCommandValidation()
//        {
//            RuleFor(cmd => cmd.AggregateId)
//                .NotEmpty()
//                .WithErrorCode(DomainErrorCodes.Appointment.EmptyId)
//                .WithMessage("Appointment id may not be empty");

//            RuleFor(cmd => cmd.ProfessorId)
//                .NotEmpty()
//                .WithErrorCode(DomainErrorCodes.Appointment.EmptyProfessorId)
//                .WithMessage("Professor id may not be empty");

//            RuleFor(cmd => cmd.StudentId)
//                .NotEmpty()
//                .WithErrorCode(DomainErrorCodes.Appointment.EmptyStudentId)
//                .WithMessage("Student id may not be empty");

//            RuleFor(cmd => cmd.CalendarId)
//                .NotEmpty()
//                .WithErrorCode(DomainErrorCodes.Appointment.EmptyCalendarId)
//                .WithMessage("Calendar id may not be empty");

//            RuleFor(cmd => cmd.DateTime)
//                .NotEmpty()
//                .WithMessage("DateTime may not be empty");

//            RuleFor(cmd => cmd.ProfessorProgress)
//                .NotEmpty()
//                .WithMessage("ProfessorProgress may not be empty");

//            RuleFor(cmd => cmd.StudentProgress)
//                .NotEmpty()
//                .WithMessage("StudentProgress may not be empty");
//        }
//    }
//}
