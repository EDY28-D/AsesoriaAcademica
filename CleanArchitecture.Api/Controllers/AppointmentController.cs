using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
//[Authorize]
[Route("/api/v1/[controller]")]
public sealed class AppointmentController : ApiController
{
    private readonly IAppointmentService _AppointmentService;

    public AppointmentController(
        INotificationHandler<DomainNotification> notifications,
        IAppointmentService AppointmentService) : base(notifications)
    {
        _AppointmentService = AppointmentService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Appointments")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<AppointmentViewModel>>))]
    public async Task<IActionResult> GetAllAppointmentsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<AppointmentViewModelSortProvider, AppointmentViewModel, Appointment>]
        SortQuery? sortQuery = null)
    {
        var Appointments = await _AppointmentService.GetAllAppointmentsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Appointments);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Appointment by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<AppointmentViewModel>))]
    public async Task<IActionResult> GetAppointmentByIdAsync([FromRoute] Guid id)
    {
        var Appointment = await _AppointmentService.GetAppointmentByIdAsync(id);
        return Response(Appointment);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Appointment")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentViewModel Appointment)
    {
        var AppointmentId = await _AppointmentService.CreateAppointmentAsync(Appointment);
        return Response(AppointmentId);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update an existing Appointment")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateAppointmentViewModel>))]
    public async Task<IActionResult> UpdateAppointmentAsync([FromRoute] Guid id, [FromBody] UpdateAppointmentViewModel appointment)
    {
        if (id != appointment.appointmentId)
        {
            return BadRequest(new { Message = "The ID in the route does not match the ID in the body." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //var result = await _AppointmentService.UpdateAppointmentAsync1(appointment);
        //if (!result)
        //{
        //    return NotFound(new { Message = "Appointment not found." });
        //}

        return Response(appointment);
    }



    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Appointment")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteAppointmentAsync([FromRoute] Guid id)
    {
        await _AppointmentService.DeleteAppointmentAsync(id);
        return Response(id);
    }
}