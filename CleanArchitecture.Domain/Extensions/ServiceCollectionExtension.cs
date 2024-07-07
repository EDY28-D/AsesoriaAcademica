using CleanArchitecture.Domain.Commands.Tenants.CreateTenant;
using CleanArchitecture.Domain.Commands.Tenants.DeleteTenant;
using CleanArchitecture.Domain.Commands.Tenants.UpdateTenant;
using CleanArchitecture.Domain.Commands.ResearchGroups.CreateResearchGroup;
using CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;
using CleanArchitecture.Domain.Commands.ResearchGroups.UpdateResearchGroup;
using CleanArchitecture.Domain.Commands.ResearchLines.CreateResearchLine;
using CleanArchitecture.Domain.Commands.ResearchLines.DeleteResearchLine;
using CleanArchitecture.Domain.Commands.ResearchLines.UpdateResearchLine;
using CleanArchitecture.Domain.Commands.Students.CreateStudent;
using CleanArchitecture.Domain.Commands.Students.DeleteStudent;
using CleanArchitecture.Domain.Commands.Students.UpdateStudent;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;
using CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;
using CleanArchitecture.Domain.Commands.Users.ChangePassword;
using CleanArchitecture.Domain.Commands.Users.CreateUser;
using CleanArchitecture.Domain.Commands.Users.DeleteUser;
using CleanArchitecture.Domain.Commands.Users.LoginUser;
using CleanArchitecture.Domain.Commands.Users.UpdateUser;
using CleanArchitecture.Domain.EventHandler;
using CleanArchitecture.Domain.EventHandler.Fanout;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Shared.Events.Tenant;
using CleanArchitecture.Shared.Events.ResearchGroup;
using CleanArchitecture.Shared.Events.ResearchLine;
using CleanArchitecture.Shared.Events.Student;
using CleanArchitecture.Shared.Events.Professor;
using CleanArchitecture.Shared.Events.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Shared.Events.Appointment;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
using CleanArchitecture.Shared.Events.AdvisoryContract;


namespace CleanArchitecture.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
        services.AddScoped<IRequestHandler<ChangePasswordCommand>, ChangePasswordCommandHandler>();
        services.AddScoped<IRequestHandler<LoginUserCommand, string>, LoginUserCommandHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<CreateTenantCommand>, CreateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTenantCommand>, UpdateTenantCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTenantCommand>, DeleteTenantCommandHandler>();
         // ResearchGroup
        services.AddScoped<IRequestHandler<CreateResearchGroupCommand>, CreateResearchGroupCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateResearchGroupCommand>, UpdateResearchGroupCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteResearchGroupCommand>, DeleteResearchGroupCommandHandler>();
        // Appointment
        services.AddScoped<IRequestHandler<CreateAppointmentCommand>, CreateAppointmentCommandHandler>();
      
        services.AddScoped<IRequestHandler<UpdateAppointmentCommand>, UpdateAppointmentCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteAppointmentCommand>, DeleteAppointmentCommandHandler>();

        //  ResearchLine  
        services.AddScoped<IRequestHandler<CreateResearchLineCommand>, CreateResearchLineCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateResearchLineCommand>, UpdateResearchLineCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteResearchLineCommand>, DeleteResearchLineCommandHandler>();
        //  Student  
        services.AddScoped<IRequestHandler<CreateStudentCommand>, CreateStudentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStudentCommand>, UpdateStudentCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteStudentCommand>, DeleteStudentCommandHandler>();
        //  Professor  
        services.AddScoped<IRequestHandler<CreateProfessorCommand>, CreateProfessorCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProfessorCommand>, UpdateProfessorCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteProfessorCommand>, DeleteProfessorCommandHandler>();
        //  AdvisoryContract 
        services.AddScoped<IRequestHandler<CreateAdvisoryContractCommand>, CreateAdvisoryContractCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateAdvisoryContractCommand>, UpdateAdvisoryContractCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteAdvisoryContractCommand>, DeleteAdvisoryContractCommandHandler>();
        return services;
    }

    public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
    {
        // Fanout
        services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

        // User
        services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<UserDeletedEvent>, UserEventHandler>();
        services.AddScoped<INotificationHandler<PasswordChangedEvent>, UserEventHandler>();

        // Tenant
        services.AddScoped<INotificationHandler<TenantCreatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantUpdatedEvent>, TenantEventHandler>();
        services.AddScoped<INotificationHandler<TenantDeletedEvent>, TenantEventHandler>();
        // ResearchGroup
        services.AddScoped<INotificationHandler<ResearchGroupCreatedEvent>, ResearchGroupEventHandler>();
        services.AddScoped<INotificationHandler<ResearchGroupUpdatedEvent>, ResearchGroupEventHandler>();
        services.AddScoped<INotificationHandler<ResearchGroupDeletedEvent>, ResearchGroupEventHandler>();
        //Appointment
        services.AddScoped<INotificationHandler<AppointmentCreatedEvent>, AppointmentEventHandler>();
        services.AddScoped<INotificationHandler<AppointmentUpdatedEvent>, AppointmentEventHandler>();
        services.AddScoped<INotificationHandler<AppointmentDeletedEvent>, AppointmentEventHandler>();

        // ResearchLine
        services.AddScoped<INotificationHandler<ResearchLineCreatedEvent>, ResearchLineEventHandler>();
        services.AddScoped<INotificationHandler<ResearchLineUpdatedEvent>, ResearchLineEventHandler>();
        services.AddScoped<INotificationHandler<ResearchLineDeletedEvent>, ResearchLineEventHandler>();
        //  Student  
        services.AddScoped<INotificationHandler<StudentCreatedEvent>, StudentEventHandler>();
        services.AddScoped<INotificationHandler<StudentUpdatedEvent>, StudentEventHandler>();
        services.AddScoped<INotificationHandler<StudentDeletedEvent>, StudentEventHandler>();
        //  Professor  
        services.AddScoped<INotificationHandler<ProfessorCreatedEvent>, ProfessorEventHandler>();
        services.AddScoped<INotificationHandler<ProfessorUpdatedEvent>, ProfessorEventHandler>();
        services.AddScoped<INotificationHandler<ProfessorDeletedEvent>, ProfessorEventHandler>();

        //  AdvisoryContract  
        services.AddScoped<INotificationHandler<AdvisoryContractCreatedEvent>, AdvisoryContractEventHandler>();
        services.AddScoped<INotificationHandler<AdvisoryContractUpdatedEvent>,AdvisoryContractEventHandler>();
        services.AddScoped<INotificationHandler<AdvisoryContractDeletedEvent>,AdvisoryContractEventHandler>();
        return services;
    }

    public static IServiceCollection AddApiUser(this IServiceCollection services)
    {
        // User
        services.AddScoped<IUser, ApiUser>();

        return services;
    }
}