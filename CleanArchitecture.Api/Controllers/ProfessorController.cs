using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Professors;
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
public sealed class ProfessorController : ApiController
{
    private readonly IProfessorService _ProfessorService;

    public ProfessorController(
        INotificationHandler<DomainNotification> notifications,
        IProfessorService ProfessorService) : base(notifications)
    {
        _ProfessorService = ProfessorService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Professors")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ProfessorViewModel>>))]
    public async Task<IActionResult> GetAllProfessorsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ProfessorViewModelSortProvider, ProfessorViewModel, Professor>]
        SortQuery? sortQuery = null)
    {
        var Professors = await _ProfessorService.GetAllProfessorsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Professors);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Professor by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ProfessorViewModel>))]
    public async Task<IActionResult> GetProfessorByIdAsync([FromRoute] Guid id)
    {
        var Professor = await _ProfessorService.GetProfessorByIdAsync(id);
        return Response(Professor);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Professor")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateProfessorAsync([FromBody] CreateProfessorViewModel Professor)
    {
        var ProfessorId = await _ProfessorService.CreateProfessorAsync(Professor);
        return Response(ProfessorId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Professor")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateProfessorViewModel>))]
    public async Task<IActionResult> UpdateProfessorAsync([FromBody] UpdateProfessorViewModel Professor)
    {
        await _ProfessorService.UpdateProfessorAsync(Professor);
        return Response(Professor);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Professor")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteProfessorAsync([FromRoute] Guid id)
    {
        await _ProfessorService.DeleteProfessorAsync(id);
        return Response(id);
    }
}