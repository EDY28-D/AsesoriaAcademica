using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Appointments.GetAll;

public sealed class GetAllAppointmentsQueryHandler :
    IRequestHandler<AppointmentsQuery, PagedResult<AppointmentViewModel>>
{
    private readonly ISortingExpressionProvider<AppointmentViewModel, Appointment> _sortingExpressionProvider;
    private readonly IAppointmentRepository _AppointmentRepository;

    public GetAllAppointmentsQueryHandler(
        IAppointmentRepository AppointmentRepository,
        ISortingExpressionProvider<AppointmentViewModel, Appointment> sortingExpressionProvider)
    {
        _AppointmentRepository = AppointmentRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<AppointmentViewModel>> Handle(
        AppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var appointmentsQuery = _AppointmentRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.Professor) // Incluye la entidad relacionada Professor
            .Include(x => x.Student) // Incluye la entidad relacionada Student
            
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            //appointmentsQuery = appointmentsQuery.Where(appointment =>
            //    appointment.Subject.Contains(request.SearchTerm)); // Suponiendo que Subject es un campo en Appointment
        }

        var totalCount = await appointmentsQuery.CountAsync(cancellationToken);

        appointmentsQuery = appointmentsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var appointments = await appointmentsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(appointment => AppointmentViewModel.FromAppointment(appointment))
            .ToListAsync(cancellationToken);

        return new PagedResult<AppointmentViewModel>(
            totalCount, appointments, request.Query.Page, request.Query.PageSize);
    }

}