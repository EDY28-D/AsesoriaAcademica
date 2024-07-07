using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Tenants.GetAll;
using CleanArchitecture.Application.Queries.Tenants.GetTenantById;
using CleanArchitecture.Application.Queries.ResearchGroups.GetAll;
using CleanArchitecture.Application.Queries.ResearchGroups.GetResearchGroupById;
using CleanArchitecture.Application.Queries.ResearchLines.GetAll;
using CleanArchitecture.Application.Queries.ResearchLines.GetResearchLineById;
using CleanArchitecture.Application.Queries.Students.GetAll;
using CleanArchitecture.Application.Queries.Students.GetStudentById;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Queries.Professors.GetProfessorById;
using CleanArchitecture.Application.Queries.Users.GetAll;
using CleanArchitecture.Application.Queries.Users.GetUserById;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Tenants;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
using CleanArchitecture.Application.ViewModels.ResearchLines;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Queries.Appointments.GetAll;
using CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;

namespace CleanArchitecture.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IResearchGroupService, ResearchGroupService>();
        services.AddScoped<IResearchLineService, ResearchLineService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IAdvisoryContractService,AdvisoryContractService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IMyService, MyService>();
        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>, GetAllUsersQueryHandler>();

        // Tenant
        services.AddScoped<IRequestHandler<GetTenantByIdQuery, TenantViewModel?>, GetTenantByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<TenantsQuery, PagedResult<TenantViewModel>>, GetAllTenantsQueryHandler>();
        // ResearchGroup
        services.AddScoped<IRequestHandler<GetResearchGroupByIdQuery, ResearchGroupViewModel?>, GetResearchGroupByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<ResearchGroupsQuery, PagedResult<ResearchGroupViewModel>>, GetAllResearchGroupsQueryHandler>();
          // ResearchLine
        services.AddScoped<IRequestHandler<GetResearchLineByIdQuery, ResearchLineViewModel?>, GetResearchLineByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<ResearchLinesQuery, PagedResult<ResearchLineViewModel>>, GetAllResearchLinesQueryHandler>();
        // Student
        services.AddScoped<IRequestHandler<GetStudentByIdQuery, StudentViewModel?>, GetStudentByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<StudentsQuery, PagedResult<StudentViewModel>>, GetAllStudentsQueryHandler>();
        // Professor
        services.AddScoped<IRequestHandler<GetProfessorByIdQuery, ProfessorViewModel?>, GetProfessorByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<ProfessorsQuery, PagedResult<ProfessorViewModel>>, GetAllProfessorsQueryHandler>();
        // AdvisoryContract
        services.AddScoped<IRequestHandler<GetAdvisoryContractByIdQuery, AdvisoryContractViewModel?>, GetAdvisoryContractByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<AdvisoryContractsQuery, PagedResult<AdvisoryContractViewModel>>, GetAllAdvisoryContractsQueryHandler>();
        // Appointment
        services.AddScoped<IRequestHandler<GetAppointmentByIdQuery, AppointmentViewModel?>, GetAppointmentByIdQueryHandler>();
        services
            .AddScoped<IRequestHandler<AppointmentsQuery, PagedResult<AppointmentViewModel>>, GetAllAppointmentsQueryHandler>();
        return services;
    }

    public static IServiceCollection AddSortProviders(this IServiceCollection services)
    {
        services.AddScoped<ISortingExpressionProvider<TenantViewModel, Tenant>, TenantViewModelSortProvider>();
         services.AddScoped<ISortingExpressionProvider<ResearchGroupViewModel, ResearchGroup>, ResearchGroupViewModelSortProvider>();
         services.AddScoped<ISortingExpressionProvider<ResearchLineViewModel, ResearchLine>, ResearchLineViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<StudentViewModel, Student>, StudentViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<ProfessorViewModel, Professor>, ProfessorViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<AdvisoryContractViewModel,AdvisoryContract>, AdvisoryContractViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<AppointmentViewModel, Appointment>, AppointmentViewModelSortProvider>();

        return services;
    }
}