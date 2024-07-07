using System;

namespace CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;

public sealed class DeleteAppointmentCommand : CommandBase
{
    private static readonly DeleteAppointmentCommandValidation s_validation = new();

    public DeleteAppointmentCommand(Guid AppointmentId) : base(AppointmentId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}