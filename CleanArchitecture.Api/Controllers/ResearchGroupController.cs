using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.ResearchGroups;
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
public sealed class ResearchGroupController : ApiController
{
    private readonly IResearchGroupService _ResearchGroupService;

    public ResearchGroupController(
        INotificationHandler<DomainNotification> notifications,
        IResearchGroupService ResearchGroupService) : base(notifications)
    {
        _ResearchGroupService = ResearchGroupService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all ResearchGroups")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ResearchGroupViewModel>>))]
    public async Task<IActionResult> GetAllResearchGroupsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ResearchGroupViewModelSortProvider, ResearchGroupViewModel, ResearchGroup>]
        SortQuery? sortQuery = null)
    {
        var ResearchGroups = await _ResearchGroupService.GetAllResearchGroupsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(ResearchGroups);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a ResearchGroup by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ResearchGroupViewModel>))]
    public async Task<IActionResult> GetResearchGroupByIdAsync([FromRoute] Guid id)
    {
        var ResearchGroup = await _ResearchGroupService.GetResearchGroupByIdAsync(id);
        return Response(ResearchGroup);
    }

    [HttpPost]
    [SwaggerOperation("Create a new ResearchGroup")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateResearchGroupAsync([FromBody] CreateResearchGroupViewModel ResearchGroup)
    {
        var ResearchGroupId = await _ResearchGroupService.CreateResearchGroupAsync(ResearchGroup);
        return Response(ResearchGroupId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing ResearchGroup")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateResearchGroupViewModel>))]
    public async Task<IActionResult> UpdateResearchGroupAsync([FromBody] UpdateResearchGroupViewModel ResearchGroup)
    {
        await _ResearchGroupService.UpdateResearchGroupAsync(ResearchGroup);
        return Response(ResearchGroup);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing ResearchGroup")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteResearchGroupAsync([FromRoute] Guid id)
    {
        await _ResearchGroupService.DeleteResearchGroupAsync(id);
        return Response(id);
    }
}