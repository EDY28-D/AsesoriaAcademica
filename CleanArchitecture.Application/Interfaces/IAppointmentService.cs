using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;

namespace CleanArchitecture.Application.Interfaces;

public interface IAppointmentService
{
    public Task<Guid> CreateAppointmentAsync(CreateAppointmentViewModel Appointment);
    public Task UpdateAppointmentAsync(UpdateAppointmentViewModel Appointment);
    public Task DeleteAppointmentAsync(Guid AppointmentId);
    public Task<AppointmentViewModel?> GetAppointmentByIdAsync(Guid AppointmentId);

    public Task<PagedResult<AppointmentViewModel>> GetAllAppointmentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}