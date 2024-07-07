using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandValidation : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.AggregateId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Appointment.EmptyId)
                .WithMessage("Appointment id may not be empty");
        }
    }
}
