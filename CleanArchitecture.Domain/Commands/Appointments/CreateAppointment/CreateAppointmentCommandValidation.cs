using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandValidation : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidation()
    {
        AddRuleForId();
        
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyId)
            .WithMessage("ResearchGroup id may not be empty");
    }


}