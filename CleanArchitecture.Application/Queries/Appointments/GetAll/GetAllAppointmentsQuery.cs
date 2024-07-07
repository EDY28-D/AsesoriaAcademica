using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using MediatR;

namespace CleanArchitecture.Application.Queries.Appointments.GetAll;

public sealed record AppointmentsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<AppointmentViewModel>>;