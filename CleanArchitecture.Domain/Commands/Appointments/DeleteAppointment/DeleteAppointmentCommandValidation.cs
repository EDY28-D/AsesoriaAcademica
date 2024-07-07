using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;

public sealed class DeleteAppointmentCommandValidation : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidation()
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